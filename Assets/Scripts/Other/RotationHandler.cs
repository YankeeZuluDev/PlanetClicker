using UnityEngine;
using Zenject;
using Upgrades.Data;
using System.Collections.Generic;
using Upgrades.Buttons;

/// <summary>
/// This class is responsible for rotating rotatable gameobject around the planet
/// </summary>

public class RotationHandler : MonoBehaviour, IRotationHandler, IGameDataInitializable
{
    [SerializeField] private float initialAngle;
    [SerializeField] private float angleOffset;
    [SerializeField] private float orbitOffset;
    [SerializeField] private float orbitLayerOffset;
    [SerializeField] private int maxRotatablesPerLayer;

    private Planet planet;
    private LayerUtility layerUtility;
    private RectTransform planetRect;
    private RotatableFactory rotatableFactory;
    private SaveLoadDataManager saveLoadDataManager;
    private UpgradeButtonsStorage upgradeButtonsStorage;
    private Vector2 planetPosition;

    #region Initialization

    [Inject]
    private void Construct(Planet planet, RotatableFactory rotatableFactory, SaveLoadDataManager saveLoadDataManager, UpgradeButtonsStorage upgradeButtonsStorage)
    {
        this.planet = planet;
        this.rotatableFactory = rotatableFactory;
        this.saveLoadDataManager = saveLoadDataManager;
        this.upgradeButtonsStorage = upgradeButtonsStorage;
    }

    private void Awake()
    {
        planetRect = planet.GetComponent<RectTransform>();
        planetPosition = planetRect.anchoredPosition;
        layerUtility = new(maxRotatablesPerLayer, planetRect.rect.height, orbitOffset, orbitLayerOffset);
        saveLoadDataManager.SubscribeForDataInitialization(this);
    }

    public void InitialzieFromGameData(GameData data)
    {
        // Map id to Rotatable upgrade config
        Dictionary<int, RotatableUpgradeConfig> idToConfigMap = new();

        foreach (UpgradeConfig upgradeConfig in upgradeButtonsStorage.UpgradeConfigsList)
        {
            if (upgradeConfig is RotatableUpgradeConfig rotatableConfig)
            {
                idToConfigMap.Add(rotatableConfig.DatabaseID, rotatableConfig);
            }
        }

        foreach (UpgradeIdCountPair pair in data.UpgradeToCountMap)
        {
            // Skip if this is not rotatable upgrade
            if (!idToConfigMap.ContainsKey(pair.ID)) continue;

            for (int i = 0; i < pair.Count; i++)
            {
                AddRotatable(idToConfigMap[pair.ID]);
            }
        }
    }

    #endregion

    public void AddRotatable(RotatableUpgradeConfig rotatableUpgradeConfig)
    {
        if (!layerUtility.HasUpgradeMapped(rotatableUpgradeConfig))
        {
            layerUtility.MapUpgrade(rotatableUpgradeConfig);
        }

        // return if limit per type exceeded
        if (layerUtility.IsUpgradeTypePerLayerLimitExceeded(rotatableUpgradeConfig))
        {
            return;
        }

        layerUtility.IncrementUpgradeCount(rotatableUpgradeConfig);

        // Add new layer if current layer length limit exceeded
        if (layerUtility.IsLayerLimitExceeded())
        {
            layerUtility.NextLayer();
        }

        // Spawn rotatable
        IRotatable rotatable = rotatableFactory.Create(rotatableUpgradeConfig, planet.RotatablesParent);

        layerUtility.AddRotatableToCurrentLayer(rotatable);

        float angle = initialAngle;

        // Add angle offset for last added rotatable depending on the current angle of penultimate rotatable
        if (layerUtility.CurrentLayerLength > 1)
        {
            angle = layerUtility.CurrentLayer[^2].CurrAngle - angleOffset;
        }

        layerUtility.CurrentLayer[^1].SetIntitialAngle(angle);
    }

    // Rotate all rotatables
    private void Update()
    {
        if (layerUtility.HasNoLayers) return;

        // Traverse each layers
        for (int i = 0; i < layerUtility.LayersCount; i++)
        {
            float orbitRadius = layerUtility.GetOrbitRadiusByLayer(i);

            // Traverse each rotatable in layer
            for (int j = layerUtility.RotatablesLayersList[i].Count - 1; j >= 0; j--)
            {
                layerUtility.RotatablesLayersList[i][j].Rotate(orbitRadius, planetPosition);
            }
        }
    }
}
