using System.Collections.Generic;
using Desdiene.UI.Elements;

namespace Desdiene.UI
{
    public class FullScreenWindowsContainer
    {
        private readonly HashSet<IFullScreenWindow> _fullScreenWindows = new HashSet<IFullScreenWindow>();

        public void HideOthers(IFullScreenWindow dontHide)
        {
            foreach (IFullScreenWindow windowInCollection in _fullScreenWindows)
            {
                if (windowInCollection != dontHide)
                {
                    windowInCollection.Hide();
                }
            }
        }

        public void Add(IFullScreenWindow fullScreenWindow)
        {
            _fullScreenWindows.Add(fullScreenWindow);
        }

        public void Remove(IFullScreenWindow fullScreenWindow)
        {
            _fullScreenWindows.Remove(fullScreenWindow);
        }
    }
}
