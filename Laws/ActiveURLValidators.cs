using System;
using System.Net;
using FluentValidation;
using ValidateCop.Exceptions;

namespace ValidateCop.Laws
{
    /// <summary>
    /// Active URL
    ///
    /// The field under validation must have a valid A or AAAA record according to the
    /// Dns.GetHostEntry C# function.
    /// </summary>
    public static class ActiveURLValidators
    {
        public static bool HaveGetHostEntry(string hostname)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostname);

                return host.AddressList.Length > 0;
            }
            catch (Exception ex)
            {
                throw new ValidateCopException(ex.Message);
            }
        }

        public static IRuleBuilderOptions<T, string> ActiveURL<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, m, context) =>
            {
                try
                {
                    var result = HaveGetHostEntry(m);

                    if (!result)
                    {
                        context.MessageFormatter.AppendArgument("ExceptionMessage", "No valid addresses");
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    context.MessageFormatter.AppendArgument("ExceptionMessage", ex.Message);

                    return false;
                }
            })
            .WithMessage("{PropertyName} must contain a valid address. More information: {ExceptionMessage}");
        }
    }
}
