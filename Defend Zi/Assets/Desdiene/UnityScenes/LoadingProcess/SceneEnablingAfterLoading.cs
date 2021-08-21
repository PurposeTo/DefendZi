using System;

namespace Desdiene.UnityScenes.LoadingOperationAsset
{
    public class SceneEnablingAfterLoading
    {
        public enum Mode
        {
            Allow,
            Forbid
        }

        /// <returns>IsAllow?</returns>
        public static bool Check(Mode mode)
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
        public static Mode Check(bool isAllow)
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
