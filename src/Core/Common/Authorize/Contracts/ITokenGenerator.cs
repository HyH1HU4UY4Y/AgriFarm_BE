using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Authorize.Contracts
{
    public interface ITokenGenerator
    {

        string GenerateJwt(Member user);
    }
}
