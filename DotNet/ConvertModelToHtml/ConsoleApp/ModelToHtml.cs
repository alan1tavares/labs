
using System.Reflection;
using System.Text;

namespace ConsoleApp;

public class ModelToHtml
{
    internal static string Convert(Type model)
    {
        var htmlBuilder = new StringBuilder();
        
        var props = model.GetTypeInfo().DeclaredProperties;
        foreach (var prop in props)
        {
            var inputType = GetInputTypeIn(prop);

            htmlBuilder.AppendLine($"<div>");
            htmlBuilder.AppendLine($"  <label>{prop.Name}</label>");
            htmlBuilder.AppendLine($"  {GetTagInput(inputType)}");
            htmlBuilder.AppendLine($"</div>");
        }
        return htmlBuilder.ToString();
    }

    private static InputType GetInputTypeIn(PropertyInfo prop)
    {
        var annotation = prop.GetCustomAttributes(typeof(HTMLFormInputTypeAttribute), true)
                        .Cast<HTMLFormInputTypeAttribute>().FirstOrDefault();
        if (annotation == null)
            return InputType.Text;
        return annotation.InputType;
    }

    private static string GetTagInput(InputType inputType)
    {
        if (inputType == InputType.TextArea)
            return $"<textarea></textarea>";
        return $"<input type=\"{inputType.ToString().ToLower()}\"/>";
    }
}
