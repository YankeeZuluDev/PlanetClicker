using System.Collections.Generic;
using UnityEngine;
using Upgrades.Data;

public static class Extensions
{
    private static string[] numberFormatChars = new string[6] { "", "K", "M", "B", "T", "Q" }; // kilo, million, billion etc.

    public static bool IsSortedAscending(this List<UpgradeConfig> list)
    {
        if (list.Count < 2) return true;

        for (int i = 0; i < list.Count - 1; i++)
        {
            if (list[i].InitialUpgradePrice > list[i + 1].InitialUpgradePrice)
                return false;
        }

        return true;
    }

    public static bool HasDuplicateIDs(this List<UpgradeConfig> list)
    {
        if (list.Count < 2) return false;

        HashSet<int> uniqueIDs = new(list.Count);

        foreach (UpgradeConfig config in list)
        {
            if (!uniqueIDs.Add(config.DatabaseID))
                return true;
        }

        return false;
    }

    public static string FormatHumanizeNumber(this long num, string format)
    {
        if (num < 1000)
            return num.ToString();

        int idx = 0;

        double dNum = num;

        while (dNum >= 1000 && idx < numberFormatChars.Length - 1)
        {
            dNum /= 1000.0;
            idx++;
        }

        return $"{dNum.ToString(format)}{numberFormatChars[idx]}";
    }
}
