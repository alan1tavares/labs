namespace ConsoleApp;

public class HTMLFormTypeAttribute : Attribute
{
    public string Name { get; }

    public HTMLFormTypeAttribute(string name)
    {
        Name = name;
    }

}
