using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Presentation.Helpers
{
    public static class ModelStateExtensions
    {
        public static Dictionary<string, string> ToErrorDictionary(this ModelStateDictionary modelState)
        {
            Dictionary<string, string> errorsDict = new Dictionary<string, string>();

            foreach (var key in modelState.Keys)
            {
                var errors = modelState[key].Errors;
                foreach (var error in errors)
                {
                    errorsDict.Add(key, error.ErrorMessage);
                }
            }

            return errorsDict;
        }
    }
}

