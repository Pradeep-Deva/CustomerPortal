using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.Infrastructure.Contracts
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
