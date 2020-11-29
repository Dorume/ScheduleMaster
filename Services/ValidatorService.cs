using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace ScheduleMaster.Services
{
    public static class ValidatorService
    {
        public static bool Validate<T>(T obj, out string error)
        {
            ValidationContext context = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(obj, context, results, true);
            if (valid)
            {
                error = "";
                return true;
            }
            else
            {
                error = results.ToString();
                return false;
            }
        }
    }
}