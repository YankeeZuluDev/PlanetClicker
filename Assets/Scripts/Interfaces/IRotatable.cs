using UnityEngine;

public interface IRotatable
{
    float CurrAngle { get; }
    void Rotate(float orbitRadius, Vector2 planetPosition);
    void SetIntitialAngle(float angle);
    void SetRotatableImage(Sprite image);
}
