using UnityEngine;
using Zenject;
using Upgrades.Data;

namespace Upgrades.Logic
{
    /// <summary>
    /// This class applies an upgrade depending on upgrade type
    /// </summary>
    public class UpgradeHandler : MonoBehaviour, IUpgradeHandler
    {
        private IRotationHandler rotationHandler;
        private IScoreController scoreController;

        [Inject]
        private void Construct(IRotationHandler rotationHandler, IScoreController scoreController)
        {
            this.rotationHandler = rotationHandler;
            this.scoreController = scoreController;
        }

        public void AddUpgrade(UpgradeConfig upgradeConfig)
        {
            if (upgradeConfig is ClickUpgradeConfig clickUpgradeConfig)
            {
                ApplyUpgrade(clickUpgradeConfig);
                return;
            }

            if (upgradeConfig is RotatableUpgradeConfig rotatableUpgradeConfig)
            {
                ApplyUpgrade(rotatableUpgradeConfig);
            }

            ApplyAdditionalScorePerSecond(upgradeConfig);
        }

        private void ApplyUpgrade(ClickUpgradeConfig clickUpgradeConfig)
        {
            scoreController.IncreaseScorePerMouseClickAmount(clickUpgradeConfig.Damage);
        }

        private void ApplyUpgrade(RotatableUpgradeConfig rotatableUpgradeConfig)
        {
            rotationHandler.AddRotatable(rotatableUpgradeConfig);
        }

        private void ApplyAdditionalScorePerSecond(UpgradeConfig upgradeConfig)
        {
            scoreController.IncreaseScorePerSecondAmount(upgradeConfig.Damage);
        }
    }
}
