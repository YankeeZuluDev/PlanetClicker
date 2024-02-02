using System.Collections.Generic;
using UnityEngine;
using Upgrades.Data;
using Upgrades.Buttons;
using Zenject;

namespace Upgrades.Logic
{
    /// <summary>
    /// This class is responsible for storing upgrade configs, spawning and initializing upgrade buttons
    /// </summary>
    public class UpgradeButtonsInitializer : MonoBehaviour, IGameDataInitializable
    {
        [SerializeField] private GameObject upgradeButtonPrefab;

        private Dictionary<int, int> upgradeIDToCountMap;
        private UpgradeButtonFactory upgradeButtonFactory;
        private UpgradeButtonsStateController buttonsStateController;
        private UpgradeButtonsStorage upgradeButtonsStorage;
        private SaveLoadDataManager saveLoadDataManager;

        #region Subscription & initialization

        [Inject]
        private void Construct(UpgradeButtonFactory upgradeButtonFactory, UpgradeButtonsStateController buttonsStateController, UpgradeButtonsStorage upgradeButtonsStorage, SaveLoadDataManager saveLoadDataManager)
        {
            this.upgradeButtonFactory = upgradeButtonFactory;
            this.buttonsStateController = buttonsStateController;
            this.upgradeButtonsStorage = upgradeButtonsStorage;
            this.saveLoadDataManager = saveLoadDataManager;
        }

        private void Awake()
        {
            saveLoadDataManager.SubscribeForDataInitialization(this);
            SaveLoadDataManager.OnGameDataInitializedEvent += SpawnAndInitializeUpgradeButtons;
        }

        private void OnDestroy()
        {
            SaveLoadDataManager.OnGameDataInitializedEvent -= SpawnAndInitializeUpgradeButtons;
        }

        private void SpawnAndInitializeUpgradeButtons()
        {
            foreach (UpgradeConfig upgradeConfig in upgradeButtonsStorage.UpgradeConfigsList)
            {
                int upgradeCount = 0;
                if (upgradeIDToCountMap.ContainsKey(upgradeConfig.DatabaseID))
                {
                    upgradeCount = upgradeIDToCountMap[upgradeConfig.DatabaseID];
                }

                // Spawn and initialzie button
                UpgradeButton upgradeButton = upgradeButtonFactory.Create(upgradeButtonPrefab, transform, upgradeConfig, upgradeCount);

                // Subscribe button to state controller
                buttonsStateController.AddUpgradeButton(upgradeButton);

                // Add buttons to button storage to save them to firebase db later
                upgradeButtonsStorage.AddUpgradeButton(upgradeButton);
            }

            // Update the state of all buttons, after they are spawned and initialized
            buttonsStateController.UpdateButtonsState();
        }

        public void InitialzieFromGameData(GameData data)
        {
            // Convert from list to dictionary for O(1) lookups when spawning and initializing
            upgradeIDToCountMap = new Dictionary<int, int>(data.UpgradeToCountMap.Count);

            foreach (UpgradeIdCountPair pair in data.UpgradeToCountMap)
            {
                upgradeIDToCountMap.Add(pair.ID, pair.Count);
            }
        }

        #endregion
    }
}
