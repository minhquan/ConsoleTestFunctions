using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposingIssue
{
    class DataContext : IDisposable
    {
        private readonly string _InstanceName;

        bool disposed;

        public DataContext(string instanceName)
        {
            _InstanceName = instanceName;
            disposed = false;
        }

        public void PrintMyName()
        {
            if (disposed)
                throw new ObjectDisposedException(_InstanceName);

            Console.WriteLine(_InstanceName);
        }

        public void Dispose()
        {
            Console.WriteLine($"{_InstanceName} is disposed.");
            disposed = true;
        }
    }
}
