using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBookStore.DataAccess
{
    public interface IDataConnection
    {
        string DatabasePath { get; }
    }
}
