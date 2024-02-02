using System.Collections.Generic;
using UnityEngine;
using Upgrades.Buttons;
using Zenject;

namespace Upgrades.Logic
{
    /// <summary>
    /// This class is responsible for toggling active/inactive state of an upgrade button
    /// </summary>
    public class UpgradeButtonsStateController : MonoBehaviour
    {
        private List<UpgradeButton> upgradeButtonsList = new();
        private IScoreInfoProvider scoreInfo;

        [Inject]
        private void Construct(IScoreInfoProvider scoreInfo)
        {
            this.scoreInfo = scoreInfo;
        }

        private void SetButtonState(UpgradeButton upgradeButton, bool value)
        {
            if (upgradeButton.IsActiveState == value) return;

            upgradeButton.ToggleButtonIntractability(value);
            upgradeButton.ToggleButtonMaskVisibility(!value); // Mask should be active when state is inactive and v.v.

            upgradeButton.IsActiveState = value;
        }

        public void UpdateButtonsState()
        {
            foreach (UpgradeButton upgradeButton in upgradeButtonsList)
            {
                bool condition = (scoreInfo.Score >= upgradeButton.Price);

                SetButtonState(upgradeButton, condition);
            }
        }

        public void AddUpgradeButton(UpgradeButton upgradeButton)
        {
            upgradeButtonsList.Add(upgradeButton);
        }
    }
}

#region Explicit implementation of state switching

/*private void SetButtonActiveState(UpgradeButton upgradeButton)
{
    if (upgradeButtonsDict[upgradeButton]) return; // Check if the button is in active state

    upgradeButton.ToggleButtonIntractability(true);
    upgradeButton.ToggleButtonMaskVisibility(flase);

    upgradeButtonsDict[upgradeButton] = true;
}

private void SetButtonInactiveState(UpgradeButton upgradeButton)
{
    if (!upgradeButtonsDict[upgradeButton]) return; // Check if the button is in inactive state

    upgradeButton.ToggleButtonIntractability(false);
    upgradeButton.ToggleButtonMaskVisibility(true);

    upgradeButtonsDict[upgradeButton] = false;
}*/

#endregion
