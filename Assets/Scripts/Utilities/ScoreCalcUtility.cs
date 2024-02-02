using System.Collections.Generic;
using UnityEngine;
using Upgrades.Data;

public class ScoreCalcUtility
{
    private Dictionary<int, int> upgradeIdToDamageDict;
    private int clickUpgradeID;
    private int clickUpgradeCount;

    private Dictionary<int, int> CreateUpgradeIdToDamageDict(List<UpgradeConfig> upgradeConfigsList)
    {
        Dictionary<int, int> dict = new(upgradeConfigsList.Count);

        foreach (UpgradeConfig upgradeConfig in upgradeConfigsList)
        {
            // Remember id of click upgrade
            if (upgradeConfig is ClickUpgradeConfig)
            {
                clickUpgradeID = upgradeConfig.DatabaseID;
            }

            dict.Add(upgradeConfig.DatabaseID, upgradeConfig.Damage);
        }

        return dict;
    }

    public long CalculateScorePerSecond(List<UpgradeConfig> upgradeConfigsList, GameData gameData)
    {
        long scorePerSecond = 0;
        upgradeIdToDamageDict = CreateUpgradeIdToDamageDict(upgradeConfigsList);

        // Calculate scorePerSecond
        foreach (UpgradeIdCountPair pair in gameData.UpgradeToCountMap)
        {
            // Skip if this is click upgrade and remember clickUpgradeCount.
            // Click upgrade damage should not be considered when calculating scorePerSecond
            if (pair.ID == clickUpgradeID)
            {
                clickUpgradeCount = pair.Count;
                continue;
            }

            scorePerSecond += upgradeIdToDamageDict[pair.ID] * pair.Count;
        }

        return scorePerSecond;
    }

    public long CalculateScorePerMouseClick(long initialScorePerClick)
    {
        return initialScorePerClick + (clickUpgradeCount * upgradeIdToDamageDict[clickUpgradeID]);
    }
}
