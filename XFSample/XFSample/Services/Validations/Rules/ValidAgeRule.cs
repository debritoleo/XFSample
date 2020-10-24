using System;
using XFSample.Services.Validations.Base;

namespace XFSample.Services.Validations.Rules
{
    public class ValidAgeRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value is DateTime birthDay)
            {
                int age = DateTime.Today.Year - birthDay.Year;

                return age >= 18;
            }

            return false;
        }
    }
}
