using System.ComponentModel.DataAnnotations;

namespace ConsoleApp;

public class Person
{
    public int Id { get; set; }
    
    [HTMLFormInputType(InputType.Text)]
    public required string Name { get; set; }

    [HTMLFormInputType(InputType.Email)]
    public required string Email { get; set; }

    [HTMLFormInputType(InputType.Radio)]
    public Gender Gender { get; set; }

    [HTMLFormInputType(InputType.TextArea)]
    public string? Summary { get; set; }
}
