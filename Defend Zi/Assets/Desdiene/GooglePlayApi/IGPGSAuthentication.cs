using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Desdiene.GooglePlayApi
{
    public interface IGPGSAuthentication
    {
        public PlayGamesPlatform Platform { get; }
        public SignInStatus SignInStatus { get; }
        bool IsAuthenticated { get; }

        void TryAuthenticate();
    }
}
