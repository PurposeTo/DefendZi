namespace Desdiene.Randoms
{
    public interface ISelectableItem<T>
    {
        string Name { get; }
        T Item { get; }
        uint ChanceMass { get; }
    }
}
