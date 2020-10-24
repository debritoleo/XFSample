using System;
using System.Collections.Generic;
using System.Text;

namespace XFSample.Services.Validations.Base
{
    public interface IValidity
    {
        bool IsValid { get; set; }
        bool Validate();
    }
}
