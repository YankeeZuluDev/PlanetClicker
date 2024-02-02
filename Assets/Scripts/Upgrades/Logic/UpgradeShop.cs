using UnityEngine;
using Upgrades.Data;
using Zenject;

namespace Upgrades.Logic
{
    /// <summary>
    /// This class serves as upgrade shop
    /// </summary>
    public class UpgradeShop : MonoBehaviour, IUpgradeShop
    {
        private IScoreInfoProvider scoreInfo;
        private IScoreController scoreController;
        private IUpgradeHandler upgradeHandler;

        [Inject]
        private void Construct(IScoreInfoProvider scoreInfo, IScoreController scoreController, IUpgradeHandler upgradeHandler)
        {
            this.scoreInfo = scoreInfo;
            this.upgradeHandler = upgradeHandler;
            this.scoreController = scoreController;
        }

        public bool BuyUpgrade(long price, UpgradeConfig upgradeConfig)
        {
            long diff = scoreInfo.Score - price;

            if (diff < 0) return false; // return false if player doesn`t have enough score to buy an upgrade

            scoreController.RemoveScore(price);
            upgradeHandler.AddUpgrade(upgradeConfig);

            return true; // return true if player bought an upgrade successfully
        }
    }
}
