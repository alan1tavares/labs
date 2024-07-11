using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;
using UseCase;

namespace Test;

public class UnitTestJsonSortProperties
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestJsonSortProperties()
    {
        string json1 = @"{
            ""name"": ""John"",
            ""age"": 30,
            ""city"": ""New York""
        }";

        string json2 = @"{
            ""city"": ""Los Angeles"",
            ""age"": 25,
            ""name"": ""Doe"",
            ""country"": ""USA""
        }";

        var jsonExpected = JObject.Parse(@"{
            ""name"": ""Doe"",
            ""age"": 25,
            ""city"": ""Los Angeles""
        }").ToString(Newtonsoft.Json.Formatting.Indented);

        var result = JsonSortProperties.Sort(json1, json2);
        Assert.That(result, Is.EqualTo(jsonExpected));
    }

    [Test]
    public void TestJsonSortPropertiesChildren()
    {
        string json1 = @"{
            ""name"": ""John"",
            ""details"": {
                ""age"": 30,
                ""city"": ""New York""
            },
            ""hobbies"": [""reading"", ""sports""]
        }";

        string json2 = @"{
            ""details"": {
                ""city"": ""Los Angeles"",
                ""age"": 25,
                ""country"": ""USA""
            },
            ""hobbies"": [""music"", ""movies""],
            ""name"": ""Doe""
        }";

        string jsonExpected = JObject.Parse(@"{
            ""name"": ""Doe"",
            ""details"": {
                ""age"": 25,
                ""city"": ""Los Angeles"",
            },
            ""hobbies"": [""music"", ""movies""]
        }").ToString(Newtonsoft.Json.Formatting.Indented);
       
       var result = JsonSortProperties.Sort(json1, json2);
        Assert.That(result, Is.EqualTo(jsonExpected));
    }
}