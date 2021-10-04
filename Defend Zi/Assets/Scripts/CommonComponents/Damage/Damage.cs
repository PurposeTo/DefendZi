public class Damage : IDamage
{
    private readonly uint _points;

    public Damage() : this(1) { }

    public Damage(uint points)
    {
        _points = points;
    }

    uint IDamage.Value => _points;
}
