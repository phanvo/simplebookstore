using System;
using static System.Console;

namespace PV_Assign1
{
    class Program
    {
        private static void LoadBookCount(Book anyBook)
        {
            bool isValid = false;
            int result = 0;
            while(!isValid)
            {
                Write("Enter the book count for {0}: ", anyBook.BookTitle);
                isValid = int.TryParse(ReadLine(), out result);
                if (isValid && result < 0)
                    isValid = false;
                if (!isValid)
                    WriteLine("Input error. Only integer input is allowed and that must be non-negative!");
            }
            anyBook.BookCount = result;
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
                LoadBookCount(item);
                //WriteLine(item.ToString());
            }

        }
    }
}
