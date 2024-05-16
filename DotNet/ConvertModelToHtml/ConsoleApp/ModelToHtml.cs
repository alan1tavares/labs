
using System.Reflection;
using System.Text;

namespace ConsoleApp;

public class ModelToHtml
{
    internal static string Convert(Type type)
    {
        var htmlBuilder = new StringBuilder();
        TypeInfo typeInfo = type.GetTypeInfo();
        var props = typeInfo.DeclaredProperties;
        foreach (var prop in props)
        {
            string inputType;
            var annotation = prop.GetCustomAttributes(typeof(HTMLFormInputTypeAttribute), true)
                .Cast<HTMLFormInputTypeAttribute>().FirstOrDefault();
            if (annotation != null)
                inputType = annotation.InputType.ToString();
            else
                inputType = "text";

            htmlBuilder.AppendLine($"<div>");
            htmlBuilder.AppendLine($"  <label>{prop.Name}</label>");
            htmlBuilder.AppendLine($"  <input type=\"{inputType}\"/>");
            htmlBuilder.AppendLine($"</div>");
        }
        return htmlBuilder.ToString();
    }
}
