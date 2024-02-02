using UnityEngine;
using Zenject;
using Upgrades.Buttons;
using Upgrades.Data;


public class UpgradeButtonFactory : IFactory<GameObject, Transform, UpgradeConfig, int, UpgradeButton>
{
    private DiContainer container;

    [Inject]
    private void Construct(DiContainer container)
    {
        this.container = container;
    }

    public UpgradeButton Create(GameObject prefab, Transform parent, UpgradeConfig config, int upgradeCount)
    {
        UpgradeButton upgradeButton = container.InstantiatePrefabForComponent<UpgradeButton>(prefab, parent);

        upgradeButton.Initialize(config, upgradeCount);

        return upgradeButton;
    }
}
