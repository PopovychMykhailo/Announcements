using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Domain.Exceptions
{
    public class WrongDateException : Exception
    {
        public WrongDateException() : base("The date is incorrect!") { }
        public WrongDateException(string message) : base("The date is incorrect!\n" + message) { }
    }
}
