using System;
using FluentValidation;
using ValidateCop.Exceptions;

namespace ValidateCop.Laws
{
    /// <summary>
    /// Alpha
    ///
    /// The field under validation must be entirely alphabetic characters.
    /// </summary>
    public static class AfterDateValidators
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

        public static IRuleBuilderOptions<T, string> After<T>(this IRuleBuilder<T, string> ruleBuilder, string referenceDate)
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

                    if (originalDate.CompareTo(refDate) > 0)
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

        public static IRuleBuilderOptions<T, DateTime> After<T>(this IRuleBuilder<T, DateTime> ruleBuilder, string referenceDate)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                try
                {
                    DateTime refDate = GetReferenceDateInDateFormat(referenceDate);

                    if (m.CompareTo(refDate) > 0)
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

        public static IRuleBuilderOptions<T, DateTime> After<T>(this IRuleBuilder<T, DateTime> ruleBuilder, DateTime referenceDate)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                try
                {
                    if (m.CompareTo(referenceDate) > 0)
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
