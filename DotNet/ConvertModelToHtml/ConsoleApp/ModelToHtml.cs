
using System.Reflection;
using System.Reflection.Emit;
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
            htmlBuilder.AppendLine($"<div>");
            htmlBuilder.AppendLine($"  {GetFieldIn(prop)}");
            htmlBuilder.AppendLine($"</div>");
        }
        return htmlBuilder.ToString();
    }

    private static string GetFieldIn(PropertyInfo prop)
    {
        var inputType = GetInputTypeIn(prop);
        if (inputType == InputType.TextArea)
            return $"<textarea></textarea>";
        if (inputType == InputType.Radio)
            return GetRadio(prop);
        
        var fieldBuilder = new StringBuilder();
        fieldBuilder.Append($"<label>{prop.Name}: ");
        fieldBuilder.Append($"<input type=\"{inputType.ToString().ToLower()}\"/>");
        fieldBuilder.Append("</label>");
        return fieldBuilder.ToString();
    }

    private static InputType GetInputTypeIn(PropertyInfo prop)
    {
        var annotation = prop.GetCustomAttributes(typeof(HTMLFormInputTypeAttribute), true)
                        .Cast<HTMLFormInputTypeAttribute>().FirstOrDefault();
        if (annotation == null)
            return InputType.Text;
        return annotation.InputType;
    }
    public static string GetRadio(PropertyInfo prop)
    {
        if (prop.PropertyType.IsEnum)
        {
            var list = prop.PropertyType.GetEnumNames();
            var labels = new StringBuilder();
            foreach (var name in list) {
                labels.AppendLine($"<label><input type=\"radio\"/>{name}</label>");
            }
            return labels.ToString();
        }
        return "";
    }
}
