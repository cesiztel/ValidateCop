using System;
using FluentValidation;
using ValidateCop.Exceptions;

namespace ValidateCop.Laws
{
    /// <summary>
    /// After (Date) or Equal
    ///
    /// The field under validation must be a value after or equal 
    /// a given date. You may pass as reference date the following values:
    ///
    /// - String human format date. Supported values: today, tomorrow
    /// - String date format: 12/5/2020. The date must be valid
    /// - Another date field
    /// </summary>
    public static class AfterDateOrEqualValidators
    {
        public const string TODAY = "today";
        public const string TOMORROW = "tomorrow";

        public static DateTime GetReferenceDateInDateFormat(string referenceDate)
        {
            if (referenceDate == TODAY)
            {
                return DateTime.Now;
            }

            if (referenceDate == TOMORROW)
            {
                return DateTime.Now.AddDays(1);
            }

            DateTime refDate;

            var parseResult = DateTime.TryParse(referenceDate, out refDate);

            if (!parseResult)
            {
                throw new ValidateCopException("{referenceDate} not supported or invalid date");
            }

            return refDate;
        }

        public static IRuleBuilderOptions<T, string> AfterOrEqual<T>(this IRuleBuilder<T, string> ruleBuilder, string referenceDate)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                DateTime originalDate;
                
                var parseResult = DateTime.TryParse(m, out originalDate);

                if (!parseResult)
                {
                    return false;
                }

                try
                {
                    DateTime refDate = GetReferenceDateInDateFormat(referenceDate);

                    if (originalDate.CompareTo(refDate) >= 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    context.MessageFormatter.AppendArgument("ExceptionMessage", ex.Message);
                }

                return false;

            }).WithMessage("'{PropertyName}' should have a valid date. {ExceptionMessage}");
        }

        public static IRuleBuilderOptions<T, DateTime> AfterOrEqual<T>(this IRuleBuilder<T, DateTime> ruleBuilder, string referenceDate)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                try
                {
                    DateTime refDate = GetReferenceDateInDateFormat(referenceDate);

                    if (m.CompareTo(refDate) >= 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    context.MessageFormatter.AppendArgument("ExceptionMessage", ex.Message);
                }

                return false;

            }).WithMessage("'{PropertyName}' should have a valid date. {ExceptionMessage}");
        }

        public static IRuleBuilderOptions<T, DateTime> AfterOrEqual<T>(this IRuleBuilder<T, DateTime> ruleBuilder, DateTime referenceDate)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                try
                {
                    if (m.CompareTo(referenceDate) >= 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    context.MessageFormatter.AppendArgument("ExceptionMessage", ex.Message);
                }

                return false;

            }).WithMessage("'{PropertyName}' should have a valid date. {ExceptionMessage}");
        }
    }
}
