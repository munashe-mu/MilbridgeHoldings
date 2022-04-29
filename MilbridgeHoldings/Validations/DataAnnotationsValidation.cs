namespace MilbridgeHoldings.Validations
{
    using MilbridgeHoldings.Models.Data.Local;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DataAnnotationsValidation
    {
        public static ActionResult<List<ValidationResult>> Data(object obj)
        {
            try
            {
                List<ValidationResult> results = new();
                if (obj == null)
                {
                    ValidationResult result = new("The object is null");
                    results.Add(result);
                    return new ActionResult<List<ValidationResult>>
                    {
                        Success = false,
                        Data = results
                    };
                }
                ValidationContext context = new(obj, null, null);
                bool valid = Validator.TryValidateObject(obj, context, results, true);
                return new ActionResult<List<ValidationResult>>
                {
                    Success = valid,
                    Data = results
                };
            }
            catch (Exception)
            {
                List<ValidationResult> results = new() { new ValidationResult("The input field is invalid") };
                return new ActionResult<List<ValidationResult>>
                {
                    Success = false,
                    Data = results
                };

            }

        }
    }
}
