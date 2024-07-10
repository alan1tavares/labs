using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UseCase;

public class JsonSortProperties
{
    public static string Sort(string aJsonDefault, string aJsonSort)
    {
        var jsonDefault = JObject.Parse(aJsonDefault);
        var jsonSort = JObject.Parse(aJsonSort);

        JObject result = new();
        foreach (var property in jsonDefault.Properties()) 
        {
            if (jsonSort.ContainsKey(property.Name))
            {
                result.Add(property.Name, jsonSort[property.Name]);
            }
        }
        return result.ToString(Formatting.Indented);
    }

}
