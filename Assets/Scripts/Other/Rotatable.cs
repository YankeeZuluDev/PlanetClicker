using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for gameobjects, that can be rotated around the planet
/// </summary>

[RequireComponent(typeof(Image))]
public class Rotatable : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotateSpeed;

    private Image image;
    private RectTransform rect;
    private float currAngle;

    public float CurrAngle => currAngle;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        currAngle += Time.deltaTime * rotateSpeed;

        if (currAngle >= 360f)
        {
            currAngle /= 360f;
        }
    }

    private void RotateAroundPlanet(float orbitRadius, Vector2 planetPosition)
    {
        float x = Mathf.Cos(currAngle) * orbitRadius;
        float y = Mathf.Sin(currAngle) * orbitRadius;

        rect.anchoredPosition = new Vector2(x, y) + planetPosition;
    }

    private void LookAtPlanet(Vector2 planetPosition)
    {
        Vector2 lookDir = planetPosition - rect.anchoredPosition;
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rect.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
    }

    public void Rotate(float orbitRadius, Vector2 planetPosition)
    {
        RotateAroundPlanet(orbitRadius, planetPosition);
        LookAtPlanet(planetPosition);
    }

    public void SetIntitialAngle(float angle) => currAngle = angle;

    public void SetRotatableImage(Sprite image) => this.image.sprite = image;
}
