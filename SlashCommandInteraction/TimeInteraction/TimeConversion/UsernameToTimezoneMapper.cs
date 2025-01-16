namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal abstract class UsernameToTimezoneMapper
{
	private const string GersonId = "319042254058684417";
	private const string JajeId = "697142901729263739";
	private const string MuertoCaId = "149452987343962112";
	private const string MuertoEuId = "1190132962457555000";
	private const string WilsonId = "138696927112986624";
	private const string BetoId = "751577747800719411";

	public static Region Map(string user)
	{
		return user switch
		{
			GersonId => Region.Estonia,
			JajeId => Region.UnitedKingdom,
			MuertoCaId => Region.CanadaHalifax,
			MuertoEuId => Region.CanadaHalifax,
			BetoId => Region.CanadaStJohns,
			WilsonId => Region.UnitedStates,
			_ => Region.Ecuador,
		};
	}
}
