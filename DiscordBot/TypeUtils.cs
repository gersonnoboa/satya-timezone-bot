namespace DiscordBot;

public class TypeUtils
{
    public static T? GetValueFromAnonymousType<T>(object? dataitem, string itemkey) {
        var type = dataitem!.GetType();
        var itemValue = (T)type.GetProperty(itemkey)!.GetValue(dataitem, null)!;
        return itemValue;
    }
}