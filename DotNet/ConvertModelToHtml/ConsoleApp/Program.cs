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
