using System;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Desdiene.GooglePlayApi
{
    public class GpgsAutentification : MonoBehaviourExt
    {
        private PlayGamesPlatform _platform;

        protected override void AwakeExt()
        {
            ConfigurePlayGamesPlatform();
            TryAuthenticate();
        }

        public SignInStatus SignInStatus { get; private set; } = SignInStatus.NotAuthenticated;
        public bool IsAuthenticated => _platform.IsAuthenticated();

        public PlayGamesPlatform Get()
        {
            if (_platform == null) throw new NullReferenceException("GPGS platform is not initialized!");

            return _platform;
        }

        public void TryAuthenticate()
        {
            if (IsAuthenticated) return;
            else Authenticate();
        }

        private void Authenticate()
        {
            _platform.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
            {
                Debug.Log($"Authenticate is completed with code: {result}.");
                SignInStatus = result;
            });
        }

        private void SignOut()
        {
            _platform.SignOut();
            Debug.Log($"GPGS Sign out have performed");
        }

        private void ConfigurePlayGamesPlatform()
        {
            if (_platform != null)
            {
                Debug.LogError("PlayGamesPlatform.Activate() is already activated!");
                return;
            }

            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration
                .Builder()
                .EnableSavedGames()
                .Build();

            PlayGamesPlatform.InitializeInstance(configuration);

#if UNITY_EDITOR
            PlayGamesPlatform.DebugLogEnabled = false;
#endif
#if UNITY_ANDROID	
            PlayGamesPlatform.DebugLogEnabled = true;
#endif

            _platform = PlayGamesPlatform.Activate();
        }
    }
}
