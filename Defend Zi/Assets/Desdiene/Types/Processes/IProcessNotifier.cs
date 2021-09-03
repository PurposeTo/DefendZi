using System;

namespace Desdiene.Types.Processes
{
    public interface IProcessNotifier
    {
       event Action<IMutableProcessGetter> OnChanged;
    }
}
