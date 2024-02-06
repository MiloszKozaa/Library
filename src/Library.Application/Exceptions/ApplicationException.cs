using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions
{
    public class ApplicationException
    {
        public string[] Errors {  get; set; }

        public ApplicationException(string error)
        {
            Errors = new[] { error };
        }
        public ApplicationException(string[] errors)
        {
            Errors = errors;
        }

    }
}
