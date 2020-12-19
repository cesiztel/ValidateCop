using System;
using FluentValidation;
using FluentValidation.Results;
using ValidateCop.Laws;

namespace ValidateCop
{
    public class Program
    {
        static void Main(string[] args)
        {
            var registration = new Registration();
            registration.termsAndConditionString = "no";
            registration.termsAndConditionInt = 1;
            registration.termsAndConditionBool = false;
            registration.termsAndConditionURL = "whateverthingyIwant.net";

            RegistrationValidator validator = new RegistrationValidator();

            ValidationResult result = validator.Validate(registration);

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
        }
    }

    public class Registration
    {
        public string termsAndConditionString { get; set; }
        public int termsAndConditionInt { get; set; }
        public bool termsAndConditionBool { get; set; }
        public string termsAndConditionURL { get; set; }
    }

    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public RegistrationValidator()
        {
            RuleFor(r => r.termsAndConditionString).Accepted();
            RuleFor(r => r.termsAndConditionBool).Accepted();
            RuleFor(r => r.termsAndConditionInt).Accepted();
            RuleFor(r => r.termsAndConditionURL).ActiveURL();
        }
    }
}
