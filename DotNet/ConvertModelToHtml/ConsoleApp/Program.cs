using ConsoleApp;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

Console.WriteLine("Hello, World!");

TypeInfo typeInfo = typeof(Person).GetTypeInfo();

Console.WriteLine("Properties");
var props = typeInfo.DeclaredProperties;
foreach (var prop in props)
{
    Console.WriteLine("Name " + prop.Name);
    prop.GetCustomAttributes(typeof(HTMLFormInputTypeAttribute), true)
        .Cast<HTMLFormInputTypeAttribute>().ToList().ForEach(att => Console.WriteLine("\tAttribute InpuType: " + att.InputType));
    prop.GetCustomAttributes(typeof(DisplayAttribute), true)
        .Cast<DisplayAttribute>().ToList().ForEach(att => Console.WriteLine("\tAttribute Diplay: " + att.GetName()));
}

var formsDirectory = Path.Combine(AppContext.BaseDirectory, "GeneratedForms");
Console.WriteLine(formsDirectory);

if (!Directory.Exists(formsDirectory))
{
    Directory.CreateDirectory(formsDirectory);
}

var models = new List<Type> { typeof(Person) };

foreach (var model in models)
{
    var html = ModelToHtml.Convert(model);
    var filePath = Path.Combine(formsDirectory, $"{model.Name}.html");
    File.WriteAllText(filePath, html);
}