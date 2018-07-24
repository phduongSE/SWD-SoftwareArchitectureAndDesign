using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataSource = new DataSource();
            var pipe = new Pipe();
            var pipe2 = new Pipe();
            var dataSink = new DataSink();

            var filter1 = new Filter1();
            filter1.LeftSource = dataSource;
            filter1.RightSource = pipe;

            var filter2 = new Filter2();
            filter2.LeftSource = pipe;
            filter2.RightSource = pipe2;

            var filter3 = new Filter3();
            filter3.LeftSource = pipe2;
            filter3.RightSource = dataSink;

            (new Thread(dataSource.Run)).Start();
            (new Thread(dataSink.Run)).Start();
            (new Thread(filter1.Run)).Start();
            (new Thread(filter2.Run)).Start();
            (new Thread(filter3.Run)).Start();
        }
    }

    class ConstantDataManager
    {
        public const int BufferSize = 2000;
        public const int EpmtyData = int.MinValue;
    }

    abstract class Source
    {
        private Queue<int> buffer;

        public Source()
        {
            buffer = new Queue<int>(ConstantDataManager.BufferSize);
        }

        public void Write(int data)
        {
            if (buffer.Count < ConstantDataManager.BufferSize)
            {
                buffer.Enqueue(data);
            }
        }

        public int Read()
        {
            if (buffer.Count > 0)
            {
                return buffer.Dequeue();
            }

            return ConstantDataManager.EpmtyData;
        }

        public abstract void Run();
    }

    class DataSource: Source
    {
        public override void Run()
        {
            Random rd = new Random();
            while (true)
            {
                int data = rd.Next(1, 1000);
                this.Write(data);
                Thread.Sleep(100);
            }
        }
    }

    class Pipe: Source
    {
        public override void Run()
        {
            //throw new NotImplementedException();
        }
    }

    class DataSink : Source
    {
        public override void Run()
        {
            while (true)
            {
                int data = this.Read();
                if (data != ConstantDataManager.EpmtyData)
                {
                    Console.WriteLine(data);
                }
                Thread.Sleep(100);
            }
        }
    }

    abstract class Filter
    {
        public Source LeftSource { get; set; }
        public Source RightSource { get; set; }

        public void Run()
        {
            if (LeftSource != null && RightSource != null)
            {
                while (true)
                {
                    int data = LeftSource.Read();
                    if (data != ConstantDataManager.EpmtyData && IsValid(data))
                    {
                        RightSource.Write(data);
                    }
                    Thread.Sleep(100);
                }
            }
        }

        // Design Pattern Template Method
        public abstract bool IsValid(int data);
    }

    class Filter1 : Filter
    {
        public override bool IsValid(int data)
        {
            return data % 3 == 0;
        }
    }

    class Filter2 : Filter
    {
        public override bool IsValid(int data)
        {
            return data % 5 == 0;
        }
    }

    class Filter3 : Filter
    {
        public override bool IsValid(int data)
        {
            return data % 2 == 0;
        }
    }
}
