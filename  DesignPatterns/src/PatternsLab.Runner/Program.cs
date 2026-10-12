using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: AFTER ===\n");

var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

var registry = new EnemyRegistry();
registry.Register("orc", new Orc());   // slow load happens once per prototype
registry.Register("elf", new Elf());

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();
for (int i = 1; i <= 5; i++)
{
    var orc = registry.Create("orc");
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}
Console.WriteLine($"Cloned 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";
Enemy copy = original.Clone();

Console.WriteLine($"\nOriginal model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
Console.WriteLine($"\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is still: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");
Console.WriteLine($"Copy abilities:     {string.Join(", ", copy.Abilities)}");

Console.WriteLine("\n=== BUILDER: AFTER ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudent());
Console.WriteLine(RegistrationCallSites.CreateVideosOnly());

Console.WriteLine("\nInvalid combinations:");
TryBuild("LiveGroup without GroupCode", () =>
    CourseRegistration.For("x@mail.com", "SEF-101").WithAccessMode("LiveGroup").Build());
TryBuild("VideosOnly with GroupCode", () =>
    CourseRegistration.For("x@mail.com", "SEF-101").VideosOnly().WithGroupCode("G1").Build());
TryBuild("No access mode", () =>
    CourseRegistration.For("x@mail.com", "SEF-101").Build());

static void TryBuild(string label, Func<CourseRegistration> build)
{
    try
    {
        build();
        Console.WriteLine($"  {label}: NO ERROR (unexpected)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  {label}: {ex.GetType().Name} - {ex.Message}");
    }
}
