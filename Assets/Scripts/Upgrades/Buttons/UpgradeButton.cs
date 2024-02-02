using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades.Data;
using Zenject;

namespace Upgrades.Buttons
{
    /// <summary>
    /// This class responsible for initializing and handling on click event for upgrade button 
    /// </summary>

    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI upgradeNameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI upgradeCountText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private GameObject buttonMask;
        [SerializeField] private string priceFormat;
        [SerializeField] private Image icon;
        
        private Button button;
        private IUpgradeShop upgradeShop;
        private UpgradeConfig upgradeConfig;
        private int upgradeCount;
        private long price;
        private bool isActiveState = false;

        public long Price => price;
        public bool IsActiveState { get => isActiveState; set => isActiveState = value; }
        public UpgradeConfig UpgradeConfig => upgradeConfig;
        public int UpgradeCount => upgradeCount;

        #region Subscription & initialization

        [Inject]
        private void Construct(IUpgradeShop upgradeShop)
        {
            this.upgradeShop = upgradeShop;
        }

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(OnButtonClick);
            upgradeCountText.text = upgradeCount.ToString();
        }

        public void Initialize(UpgradeConfig upgradeConfig, int upgradeCount)
        {
            this.upgradeConfig = upgradeConfig;
            upgradeNameText.text = upgradeConfig.UpgradeName;
            descriptionText.text = upgradeConfig.Description;
            icon.sprite = upgradeConfig.IconSprite;
            price = upgradeConfig.InitialUpgradePrice;
            priceText.text = price.FormatHumanizeNumber(priceFormat);
            this.upgradeCount = upgradeCount;
        }

        #endregion

        private void OnButtonClick()
        {
            if (upgradeShop.BuyUpgrade(price, upgradeConfig))
            {
                upgradeCount++;
                upgradeCountText.text = upgradeCount.ToString();
            }
        }

        public void ToggleButtonIntractability(bool value) => button.interactable = value;

        public void ToggleButtonMaskVisibility(bool value) => buttonMask.SetActive(value);

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
