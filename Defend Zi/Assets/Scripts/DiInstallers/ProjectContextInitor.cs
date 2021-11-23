using System;
using Desdiene.DataSaving;
using Desdiene.DataSaving.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.SceneLoaders.Single;
using Desdiene.Singletons.Unity;
using Desdiene.TimeControls;
using Desdiene.TimeControls.Adapters;
using Desdiene.UI.Components;
using Desdiene.UnityScenes;
using Zenject;

public class ProjectContextInitor : GlobalSingleton<ProjectContextInitor>
{
    [Inject]
    private void Constructor(
        BackgroundMusic _1,
        IRewardedAd _2,
        FullScreenWindowsContainer _3,
        SceneLoader _4,
        ScenesInBuild _5,
        LoadedScenes _6,
        IStorageAsync<GameStatisticsDto> _7,
        GpgsAutentification _8,
        GpgsLeaderboardMono _9,
        GlobalTimeScaleAdapter _10,
        ITime _11,
        IGameSettings _12,
        IScreenOrientation _13,
        TransitionScreen _14,
        IGameStatistics _15,
        IStorage<GameSettingsDto> _16,
        PlayerPrefsSaver _17
        )
    {
        if (_1 == null) throw new ArgumentNullException(nameof(_1));
        if (_2 == null) throw new ArgumentNullException(nameof(_2));
        if (_3 == null) throw new ArgumentNullException(nameof(_3));
        if (_4 == null) throw new ArgumentNullException(nameof(_4));
        if (_5 == null) throw new ArgumentNullException(nameof(_5));
        if (_6 == null) throw new ArgumentNullException(nameof(_6));
        if (_7 == null) throw new ArgumentNullException(nameof(_7));
        if (_8 == null) throw new ArgumentNullException(nameof(_8));
        if (_9 == null) throw new ArgumentNullException(nameof(_9));
        if (_10 == null) throw new ArgumentNullException(nameof(_10));
        if (_11 == null) throw new ArgumentNullException(nameof(_11));
        if (_12 == null) throw new ArgumentNullException(nameof(_12));
        if (_13 == null) throw new ArgumentNullException(nameof(_13));
        if (_14 == null) throw new ArgumentNullException(nameof(_14));
        if (_15 == null) throw new ArgumentNullException(nameof(_15));
        if (_16 == null) throw new ArgumentNullException(nameof(_16));
        if (_17 == null) throw new ArgumentNullException(nameof(_17));
    }
}
