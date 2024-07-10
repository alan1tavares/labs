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
}