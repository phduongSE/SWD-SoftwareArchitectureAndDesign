using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeAndFilter
{
    public abstract class Source
    {
        private Queue<Book> Buffer;

        public Source()
        {
            Buffer = new Queue<Book>(ConstantDataManager.BUFFER_SIZE);
        }

        public void Write(Book book)
        {
            if (Buffer.Count < ConstantDataManager.BUFFER_SIZE)
            {
                Buffer.Enqueue(book);
            }
        }

        public Book Read()
        {
            if (Buffer.Count > 0)
            {
                return Buffer.Dequeue();
            }

            return null;
        }

        public abstract void Run();
    }
}
