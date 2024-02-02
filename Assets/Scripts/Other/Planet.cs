using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// This class is responsible for listening to planet button click event, and executing associated logic
/// </summary>

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]

public class Planet : MonoBehaviour
{
    [SerializeField] private RectTransform rotatablesParent;
    [SerializeField] private RectTransform scoreFadingTextParent;

    private RandomPlanetImageLoader planetImageLoader;
    private Button button;
    private Image image;

    public delegate void OnPlanetButtonClicked();
    public static event OnPlanetButtonClicked OnPlanetButtonClickedEvent;

    public RectTransform RotatablesParent => rotatablesParent;
    public RectTransform ScoreFadingTextParent => scoreFadingTextParent;

    #region Initialization

    [Inject]
    private void Construct(RandomPlanetImageLoader planetImageLoader)
    {
        this.planetImageLoader = planetImageLoader;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    #endregion

    #region Subscription

    private void Start()
    {
        planetImageLoader.SetRandomPlanetSprite(image);
        button.onClick.AddListener(InvokeOnPlanetButtonClickedEvent);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(InvokeOnPlanetButtonClickedEvent);
    }

    #endregion

    private void InvokeOnPlanetButtonClickedEvent()
    {
        OnPlanetButtonClickedEvent?.Invoke();
    }
}
