namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal abstract class UsernameToTimezoneMapper
{
	internal const string GersonId = "319042254058684417";
	internal const string JajeId = "697142901729263739";
	internal const string MuertoCaId = "149452987343962112";
	internal const string MuertoEuId = "1190132962457555000";
	internal const string WilsonId = "138696927112986624";
	internal const string BetoId = "751577747800719411";
	internal const string PinkFloydId = "225795365708759041";

	public static Region Map(string user)
	{
		return user switch
		{
			GersonId => Region.Tallinn,
			PinkFloydId => Region.Tallinn,
			JajeId => Region.London,
			MuertoCaId => Region.Halifax,
			MuertoEuId => Region.Halifax,
			BetoId => Region.StJohns,
			WilsonId => Region.NewYork,
			_ => Region.Guayaquil,
		};
	}
}
