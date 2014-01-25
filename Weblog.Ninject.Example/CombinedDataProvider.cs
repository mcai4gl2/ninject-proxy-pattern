using System.Collections.Generic;
using System.Linq;

namespace Weblog.Ninject.Example
{
    public class CombinedDataProvider : IDataProvider
    {
        public IList<IDataProvider> DataProviders { get; private set; }

        public CombinedDataProvider(IList<IDataProvider> dataProviders)
        {
            DataProviders = dataProviders;
        }

        public IEnumerable<int> GetData()
        {
            return DataProviders.SelectMany(provider => provider.GetData());
        }
    }
}
