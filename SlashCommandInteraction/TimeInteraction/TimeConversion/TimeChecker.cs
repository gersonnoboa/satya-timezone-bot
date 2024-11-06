using System.Text.RegularExpressions;

namespace Dev.Noboa;

class TimeChecker
{
	public static bool IsTimeCorrect(string time)
	{
		const string regexPattern = @"\b((1[0-2]|0?[1-9]):([0-5][0-9]) ?([AaPp][Mm])?|([01]?[0-9]|2[0-3]):[0-5][0-9]|(1[0-2]|0?[1-9]) ?([AaPp][Mm]))\b";
		return Regex.IsMatch(time, regexPattern);
	}
}
