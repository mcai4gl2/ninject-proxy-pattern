using System;
using System.Collections.Generic;

namespace Weblog.Ninject.Example
{
    class RandomDataProvider : IDataProvider
    {
        public IEnumerable<int> GetData()
        {
            var random = new Random();

            for (int index = 1; index <= 10; index++)
            {
                yield return random.Next();
            }
        }
    }
}