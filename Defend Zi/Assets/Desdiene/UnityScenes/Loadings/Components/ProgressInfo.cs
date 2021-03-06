using System;
using UnityEngine;

namespace Desdiene.UnityScenes.Loadings.Components
{
    public class ProgressInfo
    {
        private readonly AsyncOperation _loadingOperation;

        public ProgressInfo(AsyncOperation loadingOperation)
        {
            _loadingOperation = loadingOperation ?? throw new ArgumentNullException(nameof(loadingOperation));
        }

        public bool IsDone => _loadingOperation.isDone;
        public SceneEnablingAfterLoading.Mode AlowingEnableMode
        {
            get
            {
                return SceneEnablingAfterLoading.GetMode(_loadingOperation.allowSceneActivation);
            }
        }
        public float Progress => _loadingOperation.progress;
        public bool Equals90Percents => Mathf.Approximately(Progress, 0.9f);
        public bool LessThan90Percents => Progress < 0.9f && !Equals90Percents;
        public bool MoreThan90Percents => Progress > 0.9f && !Equals90Percents;
        public bool Equals100Percents => Mathf.Approximately(Progress, 1f);
        public bool LessThan100Percents => Progress < 1f && !Equals100Percents;
        public bool LessOrEqualsThan90Percents => LessThan90Percents || Equals90Percents;
        public bool MoreOrEqualsThan90Percents => MoreThan90Percents || Equals90Percents;
        public bool LessOrEqualsThan100Percents => LessThan100Percents || Equals100Percents;
        public bool Between90And100PercentsExcluding => MoreThan90Percents && LessThan100Percents;
    }
}
