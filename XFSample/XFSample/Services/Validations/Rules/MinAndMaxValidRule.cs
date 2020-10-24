using XFSample.Services.Validations.Base;

namespace XFSample.Services.Validations.Rules
{
    public class MinAndMaxValidRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int MinimumLenght { get; set; }
        public int MaximumLenght { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return false;

            var valueStr = value as string;
            return (valueStr.Length > MinimumLenght && 
                    valueStr.Length <= MaximumLenght);
        }
    }
}
