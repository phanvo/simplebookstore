using System;
using System.Linq;          // add this to use linq Contains() method
using static System.Console;

namespace PV_Assign1
{
    class Program
    {
        static void LoadBookCount(Book anyBook)
        {
            bool isValid = false;
            int result = -1;
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

        static void PerformUserAction(Book book1, Book book2, Book book3)
        {
            bool isValid = false;
            int userInput = -1;
            int[] options = new int[] { 1, 2, 3 };
            bool checkContinue = true;

            while (checkContinue)
            {
                while (!isValid || checkContinue == true)
                {
                    Write("Enter the input option: ");
                    isValid = int.TryParse(ReadLine(), out userInput);
                    if(!isValid)
                        WriteLine("Input error. Only integer input is allowed!");
                    
                    if(isValid && !options.Contains(userInput))
                    {
                        WriteLine("Input error. Only input 1, 2 or 3 is allowed!");
                        isValid = false;
                    }

                    if (isValid)
                        break;
                }

                switch (userInput)
                {
                    case 1:
                        WriteLine("Case 1");
                        checkContinue = true;
                        //ViewOrder(book1, book2, book3);
                        break;
                    case 2:
                        WriteLine("Case 2");
                        checkContinue = true;
                        //UpdateOrder(book1, book2, book3);
                        break;
                    case 3:
                        Clear();
                        WriteLine("Thank you for placing an order with us. Good Bye!");
                        checkContinue = false;
                        break;
                }
            }
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

            PerformUserAction(bookList[0], bookList[1], bookList[2]);
        }
    }
}
