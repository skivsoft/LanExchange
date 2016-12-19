using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanExchange.Application.Interfaces
{
    public interface IDomainServices
    {
        T DeserializeFromFile<T>(string fileName);
    }
}
