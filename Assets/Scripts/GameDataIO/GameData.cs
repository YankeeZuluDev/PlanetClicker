using System.Collections.Generic;

/// <summary>
/// A data class, capturing a snapshot of the game state
/// </summary>
public class GameData
{
    public long Score;
    public List<UpgradeIdCountPair> UpgradeToCountMap;

    public GameData(long score, List<UpgradeIdCountPair> upgradeToCountMap)
    {
        Score = score;
        UpgradeToCountMap = upgradeToCountMap;
    }
}

[System.Serializable]
public struct UpgradeIdCountPair
{
    public int ID;
    public int Count;

    public UpgradeIdCountPair(int id, int count)
    {
        ID = id;
        Count = count;
    }
}
