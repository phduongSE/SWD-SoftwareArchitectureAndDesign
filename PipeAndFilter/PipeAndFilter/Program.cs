
namespace PipeAndFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            DBBookFirst dbfirst = new DBBookFirst();
            DBBookSecond dbsecond = new DBBookSecond();

            Pipe1 pipe1 = new Pipe1();
            pipe1.LeftSource = dbfirst;
            pipe1.RightSource = dbsecond;

            (new Thread(dbfirst.Run)).Start();
            (new Thread(dbsecond.Run)).Start();
            (new Thread(pipe1.Run)).Start();
        }
    }
}
