using System;

namespace Desdiene.UnityScenes.Loadings.Components
{
    public class SceneEnablingAfterLoading
    {
        public enum Mode
        {
            Allow,
            Forbid
        }

        /// <returns>IsForbid?</returns>
        public static bool IsForbid(Mode mode) => !IsAllow(mode);

        /// <returns>IsAllow?</returns>
        public static bool IsAllow(Mode mode)
        {
            switch (mode)
            {
                case Mode.Allow:
                    return true;
                case Mode.Forbid:
                    return false;
                default:
                    throw new ArgumentException($"{mode} is unknown Enum.{nameof(SceneEnablingAfterLoading)} value");
            }
        }

        /// <returns>IsAllow mode</returns>
        public static Mode GetMode(bool isAllow)
        {
            switch (isAllow)
            {
                case true:
                    return Mode.Allow;
                case false:
                    return Mode.Forbid;
            }
        }
    }
}
