using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("The object not found!") { }
        public NotFoundException(string message) : base("The object not found!\n" + message) { }
    }
}
