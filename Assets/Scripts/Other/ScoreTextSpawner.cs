using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Score.Text
{
    /// <summary>
    /// This class is responsible for spawning score per second text above the planet button and score per click text at mouse position
    /// </summary>
    public class ScoreTextSpawner : MonoBehaviour, IScoreTextSpawner
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private float textSpeed;
        [SerializeField] private string textFormat;

        private Transform planetTransform;
        private RectTransform planetRect;
        private TextPool textPool;
        private Camera cam;

        #region Initialization

        [Inject]
        private void Construct(TextPool textPool, Planet planet)
        {
            this.textPool = textPool;
            planetTransform = planet.transform;
        }

        private void Awake()
        {
            cam = Camera.main;
            planetRect = planetTransform.GetComponent<RectTransform>();
        }

        #endregion

        public void SpawnAndAnimateClickScoreText(long scorePerMouseClick)
        {
            Vector2 mousePositionWorld = cam.ScreenToWorldPoint(Input.mousePosition);

            TextMeshProUGUI text = textPool.Pool.Get();

            text.text = $"+{scorePerMouseClick.FormatHumanizeNumber(textFormat)}";

            text.rectTransform.position = mousePositionWorld;

            System.Action returnToPoolAction = () => textPool.Pool.Release(text);

            StartCoroutine(FadeTextAnimationCoroutine(text, fadeDuration, textSpeed, returnToPoolAction));
        }

        public void SpawnAndAnimateScorePerSecondText(long scorePerSecond)
        {
            float planetTopPositionY = planetRect.rect.height * 0.5f;

            TextMeshProUGUI text = textPool.Pool.Get();

            text.text = $"+{scorePerSecond.FormatHumanizeNumber(textFormat)}";

            text.rectTransform.anchoredPosition = new Vector2(planetRect.anchoredPosition.x, planetTopPositionY);

            System.Action returnToPoolAction = () => textPool.Pool.Release(text);

            StartCoroutine(FadeTextAnimationCoroutine(text, fadeDuration, textSpeed, returnToPoolAction));
        }

        private void SpawnAndAnimateText() // TODO: remove code dubbing ^
        {

        }

        private IEnumerator FadeTextAnimationCoroutine(TextMeshProUGUI text, float duration, float speed, System.Action onComplete)
        {
            yield return Tweens.FadeTextYPositionAndAlpha(text, duration, speed, onComplete);
        }
    }
}
