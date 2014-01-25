using System.Collections.Generic;

namespace Weblog.Ninject.Example
{
    public class SequenceDataProvider : IDataProvider
    {
        private int _start;
        private int _end;
        private int _step;
        public SequenceDataProvider(int start, int end, int step)
        {
            _start = start;
            _end = end;
            _step = step;
        }

        public IEnumerable<int> GetData()
        {
            for (int i = _start; i <= _end; i += _step)
            {
                yield return i;
            }
        }
    }
}
