﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(Dictionary<string, string[]> errors) : base(errors)
        {

        }
    }
}
