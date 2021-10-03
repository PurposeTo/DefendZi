using Desdiene.TimeControls.Scalers;

namespace Desdiene.TimeControls
{
    public interface ITime
    {
        void Set(float timeScale);
        void Stop();
        void Run();
    }
}
