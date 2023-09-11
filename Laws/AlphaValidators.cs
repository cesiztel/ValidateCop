using System.Text.RegularExpressions;
using FluentValidation;

namespace ValidateCop.Laws
{
    /// <summary>
    /// Alpha
    ///
    /// The field under validation must be entirely alphabetic characters.
    /// </summary>
    public static class AlphaValidators
    {
        const string ALPHANUMERIC_REGEX = "^[a-zA-Z ]+$";
        public static IRuleBuilderOptions<T, string> Alpha<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                return Regex.IsMatch(m, ALPHANUMERIC_REGEX);
            }).WithMessage("'{PropertyName}' should have an alphanumeric characters.");
        }
    }
}
