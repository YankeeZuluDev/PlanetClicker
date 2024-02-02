using System.Collections;
using UnityEngine;

/// <summary>
/// This class is responsible for playing planet bounce in-out animation and triggering particle system
/// </summary>

[RequireComponent(typeof(ParticleSystem))] //
public class BounceInOutAnimationPlayer : MonoBehaviour, IAnimationPlayer, IParticleSystemPlayer
{
    [SerializeField] private float bounceStrength;
    [SerializeField] private float bounceDuration;

    private ParticleSystem particles;
    private RectTransform planetRect;
    private bool isPlayingAnimation;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        planetRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Planet.OnPlanetButtonClickedEvent += PlayParticleSystem;
        Planet.OnPlanetButtonClickedEvent += PlayAnimation;
    }

    private IEnumerator PlayBounceInOutAnimationCoroutine()
    {
        isPlayingAnimation = true;

        yield return Tweens.BounceInOut(planetRect, bounceStrength, bounceDuration);

        isPlayingAnimation = false;
    }

    public void PlayParticleSystem()
    {
        particles.Play();
    }

    public void PlayAnimation()
    {
        if (isPlayingAnimation) return;

        StartCoroutine(PlayBounceInOutAnimationCoroutine());
    }

    private void OnDestroy()
    {
        Planet.OnPlanetButtonClickedEvent -= PlayParticleSystem;
        Planet.OnPlanetButtonClickedEvent -= PlayAnimation;
    }
}
