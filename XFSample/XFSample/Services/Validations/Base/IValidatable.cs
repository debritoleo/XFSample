using System.Collections.Generic;
using System.ComponentModel;

namespace XFSample.Services.Validations.Base
{
    public interface IValidatable<T> : INotifyPropertyChanged, IValidity
    {
        List<IValidationRule<T>> Validations { get; }

        List<string> Errors { get; set; }
    }
}
