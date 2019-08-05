using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users
{
    public class LoginRequest : IValidatableObject
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(UserName))
                results.Add(new ValidationResult("UserName is required"));

            if (string.IsNullOrEmpty(Password))
                results.Add(new ValidationResult("Password is required"));

            return results;
        }
    }
}
