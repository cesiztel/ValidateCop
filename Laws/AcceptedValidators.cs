using FluentValidation;

namespace ValidateCop.Laws
{
    /// <summary>
    /// Accepted validator
    ///
    /// The field under validation must be "yes", "on", 1, or true. This is
    /// useful for validating "Terms of Service" acceptance or similar fields.
    /// </summary>
    public static class AcceptedValidators
    {
        public static IRuleBuilderOptions<T, string> Accepted<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => m == "yes").WithMessage("'{PropertyName}' should have the value yes");
        }

        public static IRuleBuilderOptions<T, bool> Accepted<T>(this IRuleBuilder<T, bool> ruleBuilder)
        {
            return ruleBuilder.Must(m => m == true).WithMessage("'{PropertyName}' should be true");
        }

        public static IRuleBuilderOptions<T, int> Accepted<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(m => m == 1).WithMessage("'{PropertyName}' should have the value 1");
        }
    }
}
