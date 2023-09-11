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
            var registration = new Registration
            {
                TermsAndConditionString = "yes",
                TermsAndConditionInt = 1,
                TermsAndConditionBool = true,
                TermsAndConditionURL = "php.net",
                RegistrationDate = "12/21/2020 14:57:32.8",
                Name = " Ceksksk333"
            };

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
        public string TermsAndConditionString { get; set; }
        public int TermsAndConditionInt { get; set; }
        public bool TermsAndConditionBool { get; set; }
        public string TermsAndConditionURL { get; set; }
        public string RegistrationDate { get; set; }
        public string Name { get; set; }
    }

    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public RegistrationValidator()
        {
            RuleFor(r => r.TermsAndConditionString).Accepted();
            RuleFor(r => r.TermsAndConditionBool).Accepted();
            RuleFor(r => r.TermsAndConditionInt).Accepted();
            RuleFor(r => r.TermsAndConditionURL).ActiveURL();
            //RuleFor(r => r.RegistrationDate).After("12/21/2020 14:57:32.8");
            RuleFor(r => r.RegistrationDate).AfterOrEqual("12/21/2020 14:57:32.8");
            RuleFor(r => r.Name).Alpha();
        }
    }
}
