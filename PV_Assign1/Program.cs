using System;
using System.Collections.Generic;
using static System.Console;

namespace PV_Assign1
{
    class Program
    {
        private static void LoadBookCount(Book anyBook)
        {
            
        }

        static void Main(string[] args)
        {
            Book[] bookList = new Book[] 
            {
                new Book("title A", 17.3),
                new Book("title B", 25.8),
                new Book("title C", 39.4)
            };

            foreach (var item in bookList)
            {
                WriteLine(item.ToString());
            }

        }
    }
}
