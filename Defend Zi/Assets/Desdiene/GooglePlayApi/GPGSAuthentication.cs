using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Desdiene.GooglePlayApi
{
    public class GPGSAuthentication : MonoBehaviourExt, IGPGSAuthentication
    {
        public PlayGamesPlatform Platform { get; private set; }
        public SignInStatus SignInStatus { get; private set; } = SignInStatus.NotAuthenticated;

        public bool IsAuthenticated
        {
            get
            {
                if (Platform != null) { return Platform.IsAuthenticated(); }
                return false;
            }
        }


        protected override void AwakeExt()
        {
            ConfigurePlayGamesPlatform();
            TryAuthenticate();
        }

        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                TryAuthenticate();
            }
        }

        private void OnApplicationQuit()
        {
            SignOut();
        }


        public void TryAuthenticate()
        {
            if (IsAuthenticated)
            {
                Debug.LogError("Authentication has already been passed!");
                return;
            }
            else Authenticate();

        }

        private void Authenticate()
        {
            Platform.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
            {
                Debug.Log($"Authenticate is completed with code: {result}. IsAuthenticated {IsAuthenticated}");
                SignInStatus = result;
            });
        }

        private void ConfigurePlayGamesPlatform()
        {
            if (Platform != null)
            {
                Debug.LogError("PlayGamesPlatform.Activate() is already activated!");
                return;
            }

            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder()
                .EnableSavedGames()
                .Build();

            PlayGamesPlatform.InitializeInstance(configuration);

#if UNITY_EDITOR
            PlayGamesPlatform.DebugLogEnabled = false;
#endif
#if UNITY_ANDROID	
            PlayGamesPlatform.DebugLogEnabled = true;
#endif

            Platform = PlayGamesPlatform.Activate();
        }

        private void SignOut()
        {
            Platform.SignOut();
            Debug.Log($"GPGS Sign out have performed");
        }
    }
}
