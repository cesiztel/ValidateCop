using FluentValidation;

namespace ValidateCop.Laws
{
    /// <summary>
    /// After (Date)
    ///
    /// The field under validation must be a value after a given date. You may pass
    /// as reference date the following values:
    ///
    /// - String human format date: today, tomorrow
    /// - String date format: 12/5/2020. The date must be valid
    /// - Another date field
    /// </summary>
    public static class AfterDateValidators
    {
        /* public static IRuleBuilderOptions<T, string> After<T>(this IRuleBuilder<T, string> ruleBuilder, string referenceDate)
        {
            return ruleBuilder.Must(m => m == "yes").WithMessage("'{PropertyName}' should have the value yes");
        }*/ 
    }
}
