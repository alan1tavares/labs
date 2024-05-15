namespace ConsoleApp;

[AttributeUsageAttribute(AttributeTargets.Property)]
public class HTMLFormInputTypeAttribute : Attribute
{
    public InputType InputType { get; }

    public HTMLFormInputTypeAttribute(InputType inputType)
    {
        InputType = inputType;
    }

}
