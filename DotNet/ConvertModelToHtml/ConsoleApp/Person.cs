namespace ConsoleApp;

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }

    [HTMLFormType("TextArea")]
    public string? Summary { get; set; }
}
