namespace Assets.Desdiene.GooglePlayApi
{
    public interface IGPGSAuthentication
    {
        bool IsAuthenticated { get; }

        void TryAuthenticate();
    }
}
