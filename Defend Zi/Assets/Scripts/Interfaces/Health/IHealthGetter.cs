using Desdiene.Types.Percentale;
public interface IHealthGetter<T>
{
    IPercentable<T> Value { get; }
}
