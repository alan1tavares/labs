using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UseCase;

public class JsonSortProperties
{
    public static string Sort(string aJsonPattern, string aJsonSort)
    {
        var jsonPattern = JObject.Parse(aJsonPattern);
        var jsonSort = JObject.Parse(aJsonSort);

        JObject result = SortPropertiesRecursively(jsonPattern, jsonSort);
        return result.ToString(Formatting.Indented);
    }

    private static JObject SortPropertiesRecursively(JObject aJsonPattern, JObject aJsonSort)
    {
        JObject result = new();

        foreach (var property in aJsonPattern.Properties())
        {
            if (aJsonSort.ContainsKey(property.Name))
            {
                var jsonPatternValue = property.Value;
                var jsonSortValeu = aJsonSort[property.Name];

                if (jsonPatternValue.Type == JTokenType.Object && jsonSortValeu?.Type == JTokenType.Object)
                {
                    result.Add(property.Name, SortPropertiesRecursively((JObject)jsonPatternValue, (JObject)jsonSortValeu));
                }
                else
                {
                    result.Add(property.Name, jsonSortValeu);
                }
            }
        }
        
        return result;
    }

}
