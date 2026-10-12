namespace PatternsLab.Problems.Prototype;

public interface IPrototype<out T>
{
    T Clone();
}

public class Weapon : IPrototype<Weapon>
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }

    public Weapon Clone() => new Weapon { Name = Name, Damage = Damage };
}

public abstract class Enemy : IPrototype<Enemy>
{
    private string _modelData;

    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public Weapon Weapon { get; set; } = null!;
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    /
    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

   
    protected Enemy(Enemy source)
    {
        _modelData = source._modelData;                
        Name = source.Name;
        Health = source.Health;
        Weapon = source.Weapon.Clone();                 
        Abilities = new List<string>(source.Abilities); 
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(Orc source) : base(source) { }

    public override Enemy Clone() => new Orc(this);
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    private Elf(Elf source) : base(source) { }

    public override Enemy Clone() => new Elf(this);
}

public class EnemyRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string key, Enemy prototype)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("key required", nameof(key));
        _prototypes[key] = prototype ?? throw new ArgumentNullException(nameof(prototype));
    }

    public Enemy Create(string key)
    {
        if (!_prototypes.TryGetValue(key, out var prototype))
            throw new KeyNotFoundException($"No prototype registered with key '{key}'.");
        return prototype.Clone();
    }
}
