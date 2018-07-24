using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImmutableObjectDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime t1 = DateTime.Now;

            //String s = "";

            //for (int i = 0; i < 10000; i++)
            //{
            //    s += "a";
            //}

            //DateTime t2 = DateTime.Now;
            //Console.WriteLine("String length {0} - duration {1}", s.Length, t2.Subtract(t1).TotalMilliseconds);
            

            //DateTime t3 = DateTime.Now;

            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    sb.Append("a");
            //}
            //string s2 = sb.ToString();

            //DateTime t4 = DateTime.Now;
            //Console.WriteLine("String length {0} - duration {1}", s2.Length, t4.Subtract(t3).TotalMilliseconds);

            Document doc = new Document();
            doc.AddSentence("Hello!!!");
            doc.AddSentence("This is an example.");
            doc.AddSentence("AAAAAAAAAA");

            Document doc2 = new Document();
            doc2.AddSentence("3213123");
            doc2.AddSentence("13123132");
            doc2.AddSentence("3123213");

            //Thread thr1 = new Thread(new ParameterizedThreadStart(GenerateString));
            //thr1.Start(doc);

            //Thread thr2 = new Thread(new ParameterizedThreadStart(GenerateString));
            //thr2.Start(doc2);

            Thread thr1 = new Thread(new ParameterizedThreadStart(GenerateStringWithMonitor));
            thr1.Start(doc);

            Thread thr2 = new Thread(new ParameterizedThreadStart(GenerateStringWithMonitor));
            thr2.Start(doc2);
        }

//=====================================================================================================
        private static StringBuilder sb = new StringBuilder();

        private static object locker = new object();
        //[MethodImpl(MethodImplOptions.Synchronized)]
        private static void GenerateString(object doc)
        {
            sb.Clear();
            foreach (var s in (doc as Document).sentences)
            {
                sb.Append(s);
                Thread.Sleep(50);
                
            }

            Console.WriteLine(sb.ToString());
        }

        private static void GenerateStringWithMonitor(object doc)
        {
            lock (sb)
            {
                sb.Clear();
                foreach (var s in (doc as Document).sentences)
                {
                    sb.Append(s);
                    Thread.Sleep(50);

                }

                Console.WriteLine(sb.ToString());
            }

        }

        class Document
        {
            public List<string> sentences = new List<string>();
            public void AddSentence(string sentence)
            {
                sentences.Add(sentence);
            }
        }
    }
}
