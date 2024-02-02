using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// This class is used for setting random planet sprite, making web request through url image loader
/// </summary>
public class RandomPlanetImageLoader : MonoBehaviour
{
    [SerializeField] private List<string> planetImageUrlList;

    private URLImageLoader urlImageLoader;
    private string lastUrl;

    [Inject]
    private void Construct(URLImageLoader urlImageLoader)
    {
        this.urlImageLoader = urlImageLoader;
    }

    private void Awake()
    {
        lastUrl = string.Empty;
    }

    private string GetRandomPlanetUrl()
    {
        return planetImageUrlList[Random.Range(0, planetImageUrlList.Count)];
    }
    
    public void SetRandomPlanetSprite(Image image)
    {
        string randomPlanetURL = string.Empty;

        while (lastUrl == randomPlanetURL)
        {
            randomPlanetURL = GetRandomPlanetUrl();
        }

        lastUrl = randomPlanetURL;

        urlImageLoader.SetSpriteFromUrl(image, randomPlanetURL);
    }
}
