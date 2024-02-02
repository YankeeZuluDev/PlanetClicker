using UnityEngine;

namespace Upgrades.Data
{
    /// <summary>
    /// A data class for upgrade config
    /// </summary>

    [CreateAssetMenu(fileName = "Upgrade Config")]
    public class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private int databaseID;
        [SerializeField] private string upgradeName;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private int damage;
        [SerializeField] private string description;
        [SerializeField] private int initialUpgradePrice;

        public int DatabaseID => databaseID;
        public string UpgradeName => upgradeName;
        public Sprite IconSprite => iconSprite;
        public int Damage => damage;
        public string Description => description;
        public int InitialUpgradePrice => initialUpgradePrice;
    }
}
