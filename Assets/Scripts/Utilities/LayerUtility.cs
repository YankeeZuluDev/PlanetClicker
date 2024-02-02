using System.Collections.Generic;
using UnityEngine;
using Upgrades.Data;

/// <summary>
/// This is a utility class, used for managing layers of items orbiting around the planet
/// </summary>
public class LayerUtility
{
    private List<List<IRotatable>> rotatablesLayersList;
    private Dictionary<RotatableUpgradeConfig, int> upgradeToCountMap;
    private Dictionary<int, float> layerToRadiusMap;
    private int maxRotatablesPerLayer;
    private int currentLayerIdx;
    private float halfPlanetRectHeight;
    private float orbitOffset;
    private float orbitLayerOffset;


    public List<List<IRotatable>> RotatablesLayersList => rotatablesLayersList;
    public List<IRotatable> CurrentLayer => rotatablesLayersList[currentLayerIdx];
    public int LayersCount => rotatablesLayersList.Count;
    public int CurrentLayerLength => rotatablesLayersList[currentLayerIdx].Count;
    public bool HasNoLayers => rotatablesLayersList.Count == 0;
    public int CurrentLayerIdx => currentLayerIdx;


    public LayerUtility(int maxRotatablesPerLayer, float planetRectHeight, float orbitOffset, float orbitLayerOffset)
    {
        this.maxRotatablesPerLayer = maxRotatablesPerLayer;
        this.orbitOffset = orbitOffset;
        this.orbitLayerOffset = orbitLayerOffset;
        currentLayerIdx = 0;
        halfPlanetRectHeight = planetRectHeight * 0.5f;
        upgradeToCountMap = new();
        layerToRadiusMap = new();
        rotatablesLayersList = new List<List<IRotatable>>()
        {
            new List<IRotatable>(maxRotatablesPerLayer)
        };
    }

    public bool IsUpgradeTypePerLayerLimitExceeded(RotatableUpgradeConfig rotatableUpgradeConfig)
    {
        return upgradeToCountMap[rotatableUpgradeConfig] >= maxRotatablesPerLayer;
    }

    public bool IsLayerLimitExceeded()
    {
        return rotatablesLayersList[currentLayerIdx].Count >= maxRotatablesPerLayer;
    }

    public bool HasUpgradeMapped(RotatableUpgradeConfig config)
    {
        return upgradeToCountMap.ContainsKey(config);
    }
    
    public void AddRotatableToCurrentLayer(IRotatable rotatable)
    {
        rotatablesLayersList[currentLayerIdx].Add(rotatable);
    }

    public void NextLayer()
    {
        currentLayerIdx++;
        rotatablesLayersList.Add(new List<IRotatable>(maxRotatablesPerLayer));
    }

    public void MapUpgrade(RotatableUpgradeConfig config)
    {
        upgradeToCountMap.Add(config, 0);
    }

    public void IncrementUpgradeCount(RotatableUpgradeConfig config)
    {
        upgradeToCountMap[config]++;
    }

    public float GetOrbitRadiusByLayer(int layerIdx)
    {
        if (!layerToRadiusMap.ContainsKey(layerIdx))
        {
            float orbitRadius = halfPlanetRectHeight + (orbitOffset + orbitLayerOffset * layerIdx);
            layerToRadiusMap.Add(layerIdx, orbitRadius);
        }

        return layerToRadiusMap[layerIdx];
    }
}
