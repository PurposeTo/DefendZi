using System;

namespace Desdiene.TimeControls.Scalers
{
    public interface ITimeScaler
    {
        void SetPause(bool pause);
        void SetScale(float timeScale);
    }
}
