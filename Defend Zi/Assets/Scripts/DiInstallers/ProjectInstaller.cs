﻿using Desdiene.DataSaving.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.SceneLoaders.Single;
using Desdiene.TimeControls;
using Desdiene.TimeControls.Adapters;
using Desdiene.UI.Components;
using Desdiene.UnityScenes;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField, NotNull] private BackgroundMusic _backgroundMusic;
    [SerializeField, NotNull] private TransitionScreen _transitionScreen;

    public override void InstallBindings()
    {
        BindGameStatisticsStorage();
        BindGameStatistics();
        BindGameSettingsStorage();
        BindGameSettings();
        BindRewardedAd();
        BindFullScreenWindowsContainer();
        BindScenesInBuild();
        BindLoadedScenes();
        BindGlobalTimeRef();
        BindGlobalTimeScaler();
        BindSingleSceneLoader();
        BindGPGSAuthentication();
        BindGPGSLeaderboard();
        BindScreenOrientationAdapter();
        BindScreenOrientation();
        BindBackgroundMusic();
        BindTransitionScreen();
    }

    private void BindBackgroundMusic()
    {
        Container
             .Bind<BackgroundMusic>()
             .ToSelf()
             .FromComponentInNewPrefab(_backgroundMusic)
             .AsSingle()
             .Lazy();
    }

    private void BindRewardedAd()
    {
        Container
            .Bind<IRewardedAd>()
            //.To<FailRewardedAdStub>()
            .To<IronSourceAd>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindFullScreenWindowsContainer()
    {
        Container
            .Bind<FullScreenWindowsContainer>()
            .AsSingle()
            .Lazy();
    }

    private void BindSingleSceneLoader()
    {
        Container
            .Bind<SceneLoader>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindScenesInBuild()
    {
        Container
            .Bind<ScenesInBuild>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindLoadedScenes()
    {
        Container
            .Bind<LoadedScenes>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGPGSAuthentication()
    {
        Container
            .Bind<GpgsAutentification>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGPGSLeaderboard()
    {
        Container
            .Bind<GpgsLeaderboard>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGlobalTimeRef()
    {
        Container
            .Bind<GlobalTimeScaleAdapter>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGlobalTimeScaler()
    {
        Container
            .Bind<ITime>()
            .To<GlobalTime>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindTransitionScreen()
    {
        Container
            .Bind<TransitionScreen>()
            .ToSelf()
            .FromComponentInNewPrefab(_transitionScreen)
            .AsSingle()
            .Lazy();
    }

    private void BindScreenOrientationAdapter()
    {
        Container
            .Bind<AndroidScreenAutoRotation>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindScreenOrientation()
    {
        Container
            .Bind<ScreenOrientationWrap>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGameStatisticsStorage()
    {
        Container
            .Bind<IStorageAsync<GameStatisticsDto>>()
            .To<GameStatisticsStorageAsync>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGameStatistics()
    {
        Container
            .Bind<GameStatistics>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }


    private void BindGameSettingsStorage()
    {
        Container
            .Bind<IStorage<GameSettingsDto>>()
            .To<GameSettingsStorage>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGameSettings()
    {
        Container
            .Bind<GameSettings>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }
}
