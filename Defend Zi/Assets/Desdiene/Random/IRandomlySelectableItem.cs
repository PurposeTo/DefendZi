namespace Desdiene.Random
{
    public interface IRandomlySelectableItem<T>
    {
        string Name { get; }
        T Item { get; }
        uint ChanceMass { get; }
    }
}
