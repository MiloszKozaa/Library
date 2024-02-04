using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public Dictionary<string, string[]> Errors {  get; set; }
        public ApplicationException(string error) : base(error)
        {
            Errors = new()
            {
                {
                    "Message",
                    new[] { error }
                }
            };
        }

    }
}
