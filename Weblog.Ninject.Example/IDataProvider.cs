using System;
using System.Collections.Generic;

namespace Weblog.Ninject.Example
{
    public interface IDataProvider
    {
        IEnumerable<Int32> GetData();
    }
}
