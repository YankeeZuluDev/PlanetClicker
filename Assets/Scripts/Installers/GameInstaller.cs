using UnityEngine;
using Zenject;
using Upgrades.Logic;
using Upgrades.Buttons;
using Score.Text;
using Score.Model;
using Score.View;
using Score.Controller;

public class GameInstaller : MonoInstaller
{
    [Header("Instances")]
    [SerializeField] private Planet planet;
    [SerializeField] private InfoUI infoUI;
    [SerializeField] private UpgradeShop upgradeShop;

    [Header("Prefabs")]
    [SerializeField] private GameObject urlImageLoaderPrefab;
    [SerializeField] private GameObject randomPlanetImageLoaderPrefab;
    [SerializeField] private GameObject upgradeHandlerPrefab;
    [SerializeField] private GameObject rotationHandlerPrefab;
    [SerializeField] private GameObject saveLoadDataMangerPrefab;
    [SerializeField] private GameObject upgradeButtonsStateControllerPrefab;
    [SerializeField] private GameObject scoreTextSpawnerPrefab;
    [SerializeField] private GameObject upgradeButtonsStoragePrefab;
    [SerializeField] private GameObject textPoolPrefab;
    [SerializeField] private GameObject gameDataWrapperPrefab;
    [SerializeField] private GameObject scoreControllerPrefab;

    public override void InstallBindings()
    {
        InstallFactories();
        InstallInstances();
        InstallPrefabs();
    }

    private void InstallFactories()
    {
        Container.Bind<RotatableFactory>().AsSingle().NonLazy();
        Container.Bind<UpgradeButtonFactory>().AsSingle().NonLazy();
        Container.Bind<TextFactory>().AsSingle().NonLazy();
    }

    private void InstallInstances()
    {
        Container.Bind<Planet>().FromInstance(planet).AsSingle().NonLazy();
        Container.Bind<InfoUI>().FromInstance(infoUI).AsSingle().NonLazy();
        Container.Bind<IUpgradeShop>().To<UpgradeShop>().FromInstance(upgradeShop).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ScoreModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<ScoreView>().FromNew().AsSingle().NonLazy();
    }

    private void InstallPrefabs()
    {
        Container.Bind<URLImageLoader>().FromComponentInNewPrefab(urlImageLoaderPrefab).AsSingle().NonLazy();
        Container.Bind<RandomPlanetImageLoader>().FromComponentInNewPrefab(randomPlanetImageLoaderPrefab).AsSingle().NonLazy();
        Container.Bind<IUpgradeHandler>().To<UpgradeHandler>().FromComponentInNewPrefab(upgradeHandlerPrefab).AsSingle().NonLazy();
        Container.Bind<IRotationHandler>().To<RotationHandler>().FromComponentInNewPrefab(rotationHandlerPrefab).AsSingle().NonLazy();
        Container.Bind<SaveLoadDataManager>().FromComponentInNewPrefab(saveLoadDataMangerPrefab).AsSingle().NonLazy();
        Container.Bind<UpgradeButtonsStateController>().FromComponentInNewPrefab(upgradeButtonsStateControllerPrefab).AsSingle().NonLazy();
        Container.Bind<IScoreTextSpawner>().To<ScoreTextSpawner>().FromComponentInNewPrefab(scoreTextSpawnerPrefab).AsSingle().NonLazy();
        Container.Bind<UpgradeButtonsStorage>().FromComponentInNewPrefab(upgradeButtonsStoragePrefab).AsSingle().NonLazy();
        Container.Bind<TextPool>().FromComponentInNewPrefab(textPoolPrefab).AsSingle().NonLazy();
        Container.Bind<GameDataWrapper>().FromComponentInNewPrefab(gameDataWrapperPrefab).AsSingle().NonLazy();
        Container.Bind<IScoreController>().To<ScoreController>().FromComponentInNewPrefab(scoreControllerPrefab).AsSingle().NonLazy();
    }
}