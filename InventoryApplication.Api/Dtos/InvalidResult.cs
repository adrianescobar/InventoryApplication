using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InventoryApplication.Api.Dtos
{
    public class InvalidResult
    {
        private InvalidResult()
        {
            Messages = Array.Empty<string>();
        }

        public string[] Messages { get; set; }

        public static InvalidResult Create(string message)
        {
            return new InvalidResult
            {
                Messages = [message]
            };
        }

        public static InvalidResult Create(ModelStateDictionary modelState)
        {
            return new InvalidResult
            {
                Messages = modelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            };
        }
    }
}
