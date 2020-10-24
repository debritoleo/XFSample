using System.Net.Mail;
using XFSample.Services.Validations.Base;

namespace XFSample.Services.Validations.Rules
{
    public class IsValidEmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                var emailStr = $"{value}";
                var mail = new MailAddress(emailStr);
                return mail.Address == emailStr;
            }
            catch
            {
                return false;
            }
        }
    }
}
