using System;
using Desdiene.Types.Percentale;

//содержит гет, сет и событие об изменении
public interface IHealth<T> : IDamageTaker
{
    event Action OnDied;
    IPercentable<T> Health { get; }
}
