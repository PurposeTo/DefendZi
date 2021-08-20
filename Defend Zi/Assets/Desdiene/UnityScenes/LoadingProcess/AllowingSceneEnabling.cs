using System;

namespace Desdiene.UnityScenes.LoadingProcess
{
    public class AllowingSceneEnabling
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
                    throw new ArgumentException($"{mode} is unknown Enum.{nameof(AllowingSceneEnabling)} value");
            }
        }
    }
}
