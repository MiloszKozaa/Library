using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions
{
    public class ValidatorException : ApplicationException
    {
        public ValidatorException(string error) : base(error)
        {

        }
    }
}
