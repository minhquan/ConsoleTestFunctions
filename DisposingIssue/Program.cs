using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisposingIssue
{
    class Program
    {
        static DataContext _Db;

        static void Main(string[] args)
        {
            var thread = new Thread(() => RunThisInSeparateThread());
            thread.Start();

            using (_Db = new DataContext("Main context"))
            {
                RunThisInMainThread();
            }
        }

        static void RunThisInSeparateThread()
        {
            using (var db = new DataContext("Separate context"))
            {
                Thread.Sleep(2000);
                db.PrintMyName();
            }
        }

        static void RunThisInMainThread()
        {
            _Db.PrintMyName();
        }
    }
}
