using Upgrades.Data;

public interface IUpgradeShop
{
    bool BuyUpgrade(long price, UpgradeConfig upgradeConfig);
}
