using System.Collections;
using UnityEngine;
using Upgrades.Logic;
using Upgrades.Buttons;
using Zenject;
using Score.Model;
using Score.View;

namespace Score.Controller
{
    /// <summary>
    /// This class acts as an intermediary between ScoreView and ScoreModel
    /// </summary>
    public class ScoreController : MonoBehaviour, IGameDataInitializable, IScoreController
    {
        [SerializeField] private long initialScorePerClick;

        private ScoreModel model;
        private ScoreView view;

        private UpgradeButtonsStateController buttonsStateController;
        private UpgradeButtonsStorage upgradeButtonsStorage;
        private SaveLoadDataManager saveLoadDataManager;
        private ScoreCalcUtility calcUtility;

        #region Initialization & subscription

        [Inject]
        private void Construct(UpgradeButtonsStorage upgradeButtonsStorage, SaveLoadDataManager saveLoadDataManager, UpgradeButtonsStateController buttonsStateController, ScoreModel model, ScoreView view)
        {
            this.model = model;
            this.view = view;

            this.upgradeButtonsStorage = upgradeButtonsStorage;
            this.saveLoadDataManager = saveLoadDataManager;
            this.buttonsStateController = buttonsStateController;
        }

        private void Awake()
        {
            calcUtility = new ScoreCalcUtility();

            model.OnScoreChanged += OnScoreChanged;
            model.OnScorePerSecondAmountIncreased += OnScorePerSecondAmountIncreased;
            model.OnScorePerClickAdded += OnScorePerClickAdded;
            SaveLoadDataManager.OnGameDataInitializedEvent += StartAddingScoreEverySecond;
            Planet.OnPlanetButtonClickedEvent += OnPlanetButtonClicked;

            saveLoadDataManager.SubscribeForDataInitialization(this);
        }

        public void InitialzieFromGameData(GameData gameData)
        {
            long scorePerSecond = calcUtility.CalculateScorePerSecond(upgradeButtonsStorage.UpgradeConfigsList, gameData);
            long scorePerMouseClick = calcUtility.CalculateScorePerMouseClick(initialScorePerClick);

            model.Initialize(gameData.Score, scorePerMouseClick, scorePerSecond);
        }

        #endregion

        #region Add score every second coroutine

        private IEnumerator AddScoreEverySecond()
        {
            while (model.AllowAddingScoreEverySecond)
            {
                // Skip adding score if score per second == 0
                if (model.ScorePerSecond == 0)
                {
                    yield return null;
                    continue;
                }

                yield return new WaitForSeconds(1f);
                model.AddScore(model.ScorePerSecond);

                view.SpawnAndAnimateScorePerSecondText(model.ScorePerSecond);
            }
        }

        public void StartAddingScoreEverySecond()
        {
            if (model.AllowAddingScoreEverySecond) return;

            model.AllowAddingScoreEverySecond = true;
            StartCoroutine(AddScoreEverySecond());
        }

        public void StopAddingScoreEverySecond()
        {
            if (!model.AllowAddingScoreEverySecond) return;

            model.AllowAddingScoreEverySecond = false;
            StopAllCoroutines();
        }

        #endregion

        #region Handle callbacks

        private void OnScoreChanged()
        {
            view.UpdateScoreText(model.Score);

            buttonsStateController.UpdateButtonsState();
        }

        private void OnScorePerSecondAmountIncreased()
        {
            view.UpdateScorePerSecondText(model.ScorePerSecond);
        }

        private void OnScorePerClickAdded()
        {
            view.SpawnAndAnimateClickScoreText(model.ScorePerMouseClick);
        }

        private void OnPlanetButtonClicked()
        {
            model.AddScorePerClick();
        }

        #endregion

        public void IncreaseScorePerMouseClickAmount(long amount)
        {
            model.IncreaseScorePerMouseClickAmount(amount);
        }

        public void IncreaseScorePerSecondAmount(long amount)
        {
            model.IncreaseScorePerSecondAmount(amount);
        }

        public void RemoveScore(long amount)
        {
            model.RemoveScore(amount);
        }

        private void OnDestroy()
        {
            model.OnScoreChanged -= OnScoreChanged;
            model.OnScorePerSecondAmountIncreased -= OnScorePerSecondAmountIncreased;
            model.OnScorePerClickAdded -= OnScorePerClickAdded;
            SaveLoadDataManager.OnGameDataInitializedEvent -= StartAddingScoreEverySecond;
            Planet.OnPlanetButtonClickedEvent -= OnPlanetButtonClicked;
        }
    }
}
