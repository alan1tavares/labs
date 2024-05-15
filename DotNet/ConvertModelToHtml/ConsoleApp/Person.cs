using System.ComponentModel.DataAnnotations;

namespace ConsoleApp;

public class Person
{
    public int Id { get; set; }
    
    [HTMLFormInputTypeAttribute(InputType.Text)]
    public required string Name { get; set; }

    [Display(Name = "Summary")]
    [HTMLFormInputTypeAttribute(InputType.TextArea)]
    public string? Summary { get; set; }
}
