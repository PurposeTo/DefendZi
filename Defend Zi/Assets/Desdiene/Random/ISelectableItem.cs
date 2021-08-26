namespace Desdiene.Random
{
    public interface ISelectableItem<T>
    {
        string Name { get; }
        T Item { get; }
        uint ChanceMass { get; }
    }
}
