using UnityEngine;
using Zenject;
using Upgrades.Data;

public class RotatableFactory : IFactory<RotatableUpgradeConfig, RectTransform, IRotatable>
{
    private DiContainer container;

    [Inject]
    private void Construct(DiContainer container)
    {
        this.container = container;
    }

    public IRotatable Create(RotatableUpgradeConfig rotatableUpgradeConfig, RectTransform parent)
    {
        IRotatable rotatable = container.InstantiatePrefabForComponent<IRotatable>(rotatableUpgradeConfig.RotatablePrefab, parent);

        rotatable.SetRotatableImage(rotatableUpgradeConfig.RotatableImage);

        return rotatable;
    }
}
