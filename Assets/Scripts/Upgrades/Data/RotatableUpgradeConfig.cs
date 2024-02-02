using UnityEngine;

namespace Upgrades.Data
{
    /// <summary>
    /// A data class for upgrade, that can be rotated around the planet
    /// </summary>

    [CreateAssetMenu(fileName = "Rotatable Upgrade Config")]
    public class RotatableUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private GameObject rotatablePrefab;
        [SerializeField] private Sprite rotatableImage;

        public GameObject RotatablePrefab => rotatablePrefab;

        public Sprite RotatableImage => rotatableImage;
    }
}
