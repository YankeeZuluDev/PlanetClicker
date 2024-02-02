using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class TextFactory : IFactory<GameObject, Transform, TextMeshProUGUI>
{
    private DiContainer container;

    [Inject]
    private void Construct(DiContainer container)
    {
        this.container = container;
    }

    public TextMeshProUGUI Create(GameObject prefab, Transform parent)
    {
        return container.InstantiatePrefabForComponent<TextMeshProUGUI>(prefab, parent);
    }
}
