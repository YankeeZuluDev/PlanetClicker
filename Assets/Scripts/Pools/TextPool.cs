using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Score.Text
{
    /// <summary>
    /// An object pool for score text
    /// </summary>
    public class TextPool : MonoBehaviour
    {
        [SerializeField] private GameObject textPrefab;

        private ObjectPool<TextMeshProUGUI> pool;
        private TextFactory textFactory;
        private Planet planet;

        public ObjectPool<TextMeshProUGUI> Pool => pool;

        [Inject]
        private void Construct(TextFactory textFactory, Planet planet)
        {
            this.planet = planet;
            this.textFactory = textFactory;
        }

        private void Awake()
        {
            pool = new ObjectPool<TextMeshProUGUI>(OnSpawn, OnGet, OnRelease, OnKill);
        }

        private TextMeshProUGUI OnSpawn()
        {
            return textFactory.Create(textPrefab, planet.ScoreFadingTextParent);
        }

        private void OnGet(TextMeshProUGUI text)
        {
            text.gameObject.SetActive(true);
            text.alpha = 1;
        }

        private void OnRelease(TextMeshProUGUI text)
        {
            text.gameObject.SetActive(false);
        }

        private void OnKill(TextMeshProUGUI text)
        {
            Destroy(text.gameObject);
        }
    }
}
