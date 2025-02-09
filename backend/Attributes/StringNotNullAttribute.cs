using System.ComponentModel.DataAnnotations;
namespace backend.Attributes;

public class StringNotNullAttribute : ValidationAttribute
{
    override public bool IsValid(object value)
    {
        if (value is null)
            return false;

        return true;
    }
}