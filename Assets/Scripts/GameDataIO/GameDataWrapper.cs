using System.Collections.Generic;
using UnityEngine;
using Upgrades.Buttons;
using Zenject;

public class GameDataWrapper : MonoBehaviour
{
    //private IScoreManager scoreManager;
    private IScoreInfoProvider scoreInfo;
    private UpgradeButtonsStorage upgradeButtonsStorage;

    [Inject]
    private void Construct(IScoreInfoProvider scoreInfo, UpgradeButtonsStorage upgradeButtonsStorage)
    {
        this.upgradeButtonsStorage = upgradeButtonsStorage;
        this.scoreInfo = scoreInfo;
    }

    public GameData CollectAndWrapGameData()
    {
        List<UpgradeIdCountPair> upgradeToCountMap = new(upgradeButtonsStorage.UpgradeButtonsList.Count);

        // Create a dict of kvp, where key -> upgrade db ID and value -> upgrade count
        foreach (UpgradeButton upgradeButton in upgradeButtonsStorage.UpgradeButtonsList)
        {
            UpgradeIdCountPair pair = new(upgradeButton.UpgradeConfig.DatabaseID, upgradeButton.UpgradeCount);
            upgradeToCountMap.Add(pair);
        }

        return new(scoreInfo.Score, upgradeToCountMap);
    }
}
