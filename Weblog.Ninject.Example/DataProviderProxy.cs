using System.Linq;
using System.Collections.Generic;

namespace Weblog.Ninject.Example
{
    class DataProviderProxy : IDataProvider
    {
        public IDataProvider DataProvider { get; private set; }

        public DataProviderProxy(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public IEnumerable<int> GetData()
        {
            return DataProvider.GetData().Select(i => i + 100);
        }
    }
}