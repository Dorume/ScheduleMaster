using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;
using ScheduleMaster.Services.Interfaces;

namespace ScheduleMaster.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool Validate<T>(T obj, out string error)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(obj, context, results, true);
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