using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PipeAndFilter
{
    public abstract class Pipe
    {
        public Source LeftSource { get; set; }
        public Source RightSource { get; set; }

        public void Run()
        {
            if (LeftSource != null && RightSource != null)
            {
                while (true)
                {
                    Book book = LeftSource.Read();
                    if (book != null)
                    {
                        RightSource.Write(book);
                    }
                    Thread.Sleep(100);
                }
            }
        }
    }
}
