using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassByRefOrVal
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            int a = 3, b = 5;
            //Swap(ref a,ref b);

            SwapByPointer(&a, &b);

            Console.WriteLine("a: " + a);
            Console.WriteLine("b: " + b);

            //================================
            Student st = new Student();
            st.Name = "Cung";
            st.Car = new Car()
            {
                Name = "Mercedes"
            };

            //================================
            List<Student> students = new List<Student>(5);
            for (int i = 0; i < 5; i++)
            {
                Student st1 = new Student();
                students.Add(st1);
            }
            //================================
            Display(students);

            //================================
            Student st2 = st;
            st2.Name = "Hieu";
            Console.WriteLine("Name = {0}, Car = {1}", st.Name, st.Car.Name);

            //================================
            Student st3 = new Student();
            st3.Name = st2.Name;
            st3.Car = st2.Car;
            //================================
            st3.Name = "Nam";
            st3.Car.Name = "Audi";
            Console.WriteLine("Name = {0}, Car = {1}", st.Name, st.Car.Name);
            Console.ReadKey();
        }

        static void Swap(ref int a,ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static void Swap1(ref int a, ref int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }

        static void Swap2(ref int a, ref int b)
        {
            // A XOR A = 0; A XOR 0 = A
            a = a ^ b;
            b = a ^ b; // b = a0 XOR b0 XOR b0 = a0
            a = a ^ b; // a = a0 XOR b0 XOR a0 = b0
        }

        unsafe static void SwapByPointer(int* pa,int* pb)
        {
            int tmp = *pa;
            *pa = *pb;
            *pb = tmp;
        }

        static void Display(List<Student> students)
        {
            foreach (Student st in students)
            {
                Console.WriteLine(st.ToString());
            }
        }

        class Student
        {
            public string Name { get; set; }
            public Car Car { get; set; }
        }

        class Car
        {
            public string Name { get; set; }
        }
    }
}
