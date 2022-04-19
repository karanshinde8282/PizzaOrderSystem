using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderSystemWebAPI.Common
{
    public class UserDefinedException : Exception
    {
        public UserDefinedException(string message)
        : base(message)
        {
        }
    }
}
