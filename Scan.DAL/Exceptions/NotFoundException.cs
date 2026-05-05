using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    //we will make a custom exception for not found to throw in response
    public class NotFoundException(string Message) : Exception(Message)
    {

    }
}
