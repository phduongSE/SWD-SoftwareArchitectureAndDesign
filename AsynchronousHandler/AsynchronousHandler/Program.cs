using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            EventSource2 source = new EventSource2();
            IEventListener primeListener = new PrimeListener();
            IEventListener squareListener = new SquareListener();

            source.Subcribe(primeListener);
            source.Subcribe(squareListener);

            //source.SendData += PrintHaha;

            source.Run();

            Console.ReadLine();
        }
        public static void PrintHaha(int data)
        {
            Console.WriteLine("Haha");
        }
    }

   

    interface IEventListener
    {
        void HandleData(int data);
    }

    abstract class EventSource
    {
        public abstract void Subcribe(IEventListener listener);
        public abstract void UnSubcribe(IEventListener listener);
        protected abstract void NotifyData(int data);

        public void Run()
        {
            Random rd = new Random();
            while (true)
            {
                int data = rd.Next(1, 100);
                NotifyData(data);
                Thread.Sleep(2000);
            }
        }
    }

    class EventSource1 : EventSource
    {
        private List<IEventListener> listeners = new List<IEventListener>();
        public override void Subcribe(IEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public override void UnSubcribe(IEventListener listener)
        {
            listeners.Remove(listener);
        }

        protected override void NotifyData(int data)
        {
            foreach (var listener in listeners)
            {
                listener.HandleData(data);
            }
        }
    }

    class EventSource2 : EventSource
    {
        public delegate void ReceiveDataEventHandler(int data);
        public event ReceiveDataEventHandler SendData;

        public override void Subcribe(IEventListener listener)
        {
            SendData += listener.HandleData;
        }

        public override void UnSubcribe(IEventListener listener)
        {
            SendData -= listener.HandleData;
        }

        protected override void NotifyData(int data)
        {
            SendData(data);
        }
    }


    class PrimeListener : IEventListener
    {
        private bool CheckPrime(int data)
        {
            if (data <= 1) return false;
            if (data == 2) return true;
            if (data % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(data));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (data % i == 0) return false;
            }
            return true;
        }

        public void HandleData(int data)
        {
            if (CheckPrime(data))
            {
                Console.WriteLine("{0} is a prime number", data);
            }
            else
            {
                Console.WriteLine("{0} is not a prime number", data);
            }
        }
    }


    class SquareListener : IEventListener
    {
        private bool CheckSquare(int data)
        {
            if (Math.Sqrt(data) % 1 == 0)
            {
                return true;
            }

            return false;
        }

        public void HandleData(int data)
        {
            if (CheckSquare(data))
            {
                Console.WriteLine("{0} is a square number", data);
            }
            else
            {
                Console.WriteLine("{0} is not a square number", data);
            }
        }
    }
}
