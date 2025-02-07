using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Extensions;
public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
        => modelState
            .Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();
    
}