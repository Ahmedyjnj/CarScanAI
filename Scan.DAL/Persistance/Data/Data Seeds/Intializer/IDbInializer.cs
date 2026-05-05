using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDbInializer
    {
        //the intializer has signature for async method

        Task InializeAsync();

        Task IdentityInializeAsync();

    }
}
