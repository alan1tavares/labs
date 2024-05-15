using ConsoleApp;
using System.Reflection;

Console.WriteLine("Hello, World!");

TypeInfo typeInfo = typeof(Person).GetTypeInfo();

Console.WriteLine("Properties");
var props = typeInfo.DeclaredProperties;
foreach (var prop in props)
{
    Console.WriteLine("Name " + prop.Name);
    prop.GetCustomAttributes(typeof(HTMLFormTypeAttribute), true)
        .Cast<HTMLFormTypeAttribute>().ToList().ForEach(att => Console.WriteLine("Attribute Name: " + att.Name));
}
