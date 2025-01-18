namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal class Country: IEquatable<Country>
{
    private Country(string value, List<Region> regions) { Value = value; Regions = regions; }

    public string Value { get; }
    public List<Region> Regions { get; }

    internal static Country Ecuador => new("Ecuador", [Region.Guayaquil]);
    internal static Country UnitedStates => new("USA", [Region.NewYork]);
    internal static Country UnitedKingdom => new("UK", [Region.London]);
    internal static Country Estonia => new("Estonia", [Region.Tallinn]);
    internal static Country Canada => new("Canadá", [Region.Halifax, Region.StJohns]);
    
    public static Country[] AllCountries => [Estonia, Ecuador, UnitedKingdom, UnitedStates, Canada];

    public bool Equals(Country? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Country)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}