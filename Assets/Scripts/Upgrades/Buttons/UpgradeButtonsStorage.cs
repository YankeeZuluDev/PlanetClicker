using System.Collections.Generic;
using UnityEngine;
using Upgrades.Data;

namespace Upgrades.Buttons
{
    public class UpgradeButtonsStorage : MonoBehaviour
    {
        public List<UpgradeConfig> UpgradeConfigsList;
        public List<UpgradeButton> UpgradeButtonsList = new();

        public void AddUpgradeButton(UpgradeButton upgradeButton)
        {
            UpgradeButtonsList.Add(upgradeButton);
        }

        #region Editor time checks

        private void OnValidate()
        {
            // Check for duplicate database IDs
            if (UpgradeConfigsList.HasDuplicateIDs())
            {
                Debug.LogWarning("Upgrade configs list has duplicate database IDs");
            }

            if (UpgradeConfigsList.Count == 0) return;

            if (UpgradeConfigsList.IsSortedAscending()) return;

            // Sort in ascending order by price
            UpgradeConfigsList.Sort((a, b) => a.InitialUpgradePrice.CompareTo(b.InitialUpgradePrice));
        }

        #endregion
    }
}
