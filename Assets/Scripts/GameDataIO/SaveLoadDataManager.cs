using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for saving, loading and intializing game data from Firebase db
/// </summary>
public class SaveLoadDataManager : MonoBehaviour
{
    private List<IGameDataInitializable> initializablesList = new();
    private GameDataWrapper gameDataWrapper;
    private DatabaseReference db;
    private string userID;

    public delegate void OnGameDataInitialized();
    public static event OnGameDataInitialized OnGameDataInitializedEvent;

    [Inject]
    private void Construct(GameDataWrapper gameDataWrapper)
    {
        this.gameDataWrapper = gameDataWrapper;
    }

    private void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;

        StartCoroutine(LoadAndInitializeGameData());
    }

    private IEnumerator LoadAndInitializeGameData()
    {
        yield return InitializeDB();

        var dataTask = db.Child("users").Child(userID).GetValueAsync();

        yield return new WaitUntil(() => dataTask.IsCompleted);

        DataSnapshot gameDataSnapshot = dataTask.Result;

        if (!gameDataSnapshot.Exists)
        {
            // if no data for user ID, then load default data from db
            Debug.Log("data snapshot does not exist for current user, loading default data...");

            // load default data
            var defaultDataTask = db.Child("users").Child("default").GetValueAsync();

            yield return new WaitUntil(() => defaultDataTask.IsCompleted);

            gameDataSnapshot = defaultDataTask.Result;
        }

        string gameDataJson = gameDataSnapshot.GetRawJsonValue();

        GameData gameData = JsonUtility.FromJson<GameData>(gameDataJson);

        if (gameData == null)
        {
            Debug.LogWarning("game data is null");
            yield break;
        }

        InitializeGameData(gameData);
    }

    private IEnumerator InitializeDB()
    {
        // Check and fix dependencies
        var checkTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(() => checkTask.IsCompleted);

        var checkStatus = checkTask.Result;

        if (checkStatus != DependencyStatus.Available)
        {
            Debug.LogError("Could not resolve dependencies: " + checkStatus);
            yield break;
        }

        // Initialize db
        Debug.Log("Setting up unity firebase...");

        var database = FirebaseDatabase.DefaultInstance;
        if (Application.isEditor)
        {
            database.SetPersistenceEnabled(false);
        }

        db = database.RootReference;
    }

    private void SaveGameData() 
    {
        GameData gameData = gameDataWrapper.CollectAndWrapGameData();

        string json = JsonUtility.ToJson(gameData);

        db.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    private void InitializeGameData(GameData gameData)
    {
        // Initialzie initializables with given GameData

        foreach (var initializable in initializablesList)
        {
            initializable.InitialzieFromGameData(gameData);
        }

        OnGameDataInitializedEvent();
    }

    public void SubscribeForDataInitialization(IGameDataInitializable initializable)
    {
        initializablesList.Add(initializable);
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
