using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for loading a sprite using UnityWebRequest and setting it to an image
/// </summary>
public class URLImageLoader : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;

    private IEnumerator SetSpriteFromUrlCoroutine(Image image, string url)
    {
        //// temp
        //image.sprite = defaultSprite;
        //yield return null;
        ////

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            image.sprite = Texture2DToSprite(texture);
        }
        else
        {
            Debug.LogError("Failed to load image: " + request.error);
            Debug.LogError("DownloadHandler info: " + request.downloadHandler.error);

            Debug.Log("Loading default sprite...");
            image.sprite = defaultSprite;
        }
    }

    private Sprite Texture2DToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }

    public void SetSpriteFromUrl(Image image, string url)
    {
        StartCoroutine(SetSpriteFromUrlCoroutine(image, url));
    }
}
