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
            //bool checkContinue = true;

            //while (checkContinue)
            //{
                //while (!isValid || checkContinue == true)
                while (!isValid)
                {
                    WriteLine("\n\nWhat would you like to do?");
                    Write("Press 1 for View Order, Press 2 for Update Order, Press 3 for quitting the application: ");
                    isValid = int.TryParse(ReadLine(), out userInput);
                    if(!isValid)
                        WriteLine("Input error. Only integer input is allowed!");
                    
                    if(isValid && !options.Contains(userInput))
                    {
                        WriteLine("Input error. Only input 1, 2 or 3 is allowed!");
                        isValid = false;
                    }
                }

                switch (userInput)
                {
                    case 1:
                        //checkContinue = true;
                        ViewOrder(book1, book2, book3);
                        break;
                    case 2:
                        //checkContinue = true;
                        UpdateOrder(book1, book2, book3);
                        break;
                    case 3:
                        Clear();
                        WriteLine("Thank you for placing an order with us. Good Bye!");
                        //checkContinue = false;
                        break;
                }
            //}
        }

        static void ViewOrder(Book book1, Book book2, Book book3)
        {
            string asteriskLine = new string('*', 100);
            WriteLine("\nOkay! Lets view your order!\n");
            WriteLine(asteriskLine);
            WriteLine(book1.ToString());
            WriteLine(book2.ToString());
            WriteLine(book3.ToString());

            double totalAfterTaxAndDiscount = GetOrderTotals(book1, book2, book3, out double totalBeforeTaxAndDiscount,
                                   out double taxes, out double discountAmount);

            string billCalculationStr = "*{0, 46}: {1, -50:C2}*\n";
            billCalculationStr += "*{2, 46}: {3, -50:C2}*\n";
            billCalculationStr += "*{4, 46}: {5, -50:C2}*\n";
            billCalculationStr += "*{6, 46}: {7, -50:C2}*";

            WriteLine(billCalculationStr, "Total before tax and discount", totalBeforeTaxAndDiscount,
                                          "Taxes", taxes,
                                          "Discount", discountAmount,
                                          "Total after tax and discount", totalAfterTaxAndDiscount);
            WriteLine(asteriskLine);

            PerformUserAction(book1, book2, book3);
        }

        static double GetOrderTotals(Book book1, Book book2, Book book3,
                                   out double totalBeforeTaxAndDiscount,
                                   out double taxes, out double discountAmount)
        {
            totalBeforeTaxAndDiscount = book1.BookSubTotal + book2.BookSubTotal + book3.BookSubTotal;
            taxes = totalBeforeTaxAndDiscount * 0.07;
            discountAmount = totalBeforeTaxAndDiscount > 300 ? totalBeforeTaxAndDiscount * 0.1 : 0;
            double totalAfterTaxAndDiscount = totalBeforeTaxAndDiscount + taxes - discountAmount;
            return totalAfterTaxAndDiscount;
        }

        static void UpdateOrder(Book book1, Book book2, Book book3)
        {
            string infoStr = "\nPress 1 to update book counts for {0}\n";
            infoStr += "Press 2 to update book counts for {1}\n";
            infoStr += "Press 3 to update book counts for {2}\n";
            infoStr += "Press 4 to cancel Update Order\n";
            infoStr += "Enter your option: ";

            bool isValid = false;
            int userInput = -1;
            int[] options = new int[] { 1, 2, 3, 4 };

            WriteLine("\nOkay! Lets update your order!\n");

            while (!isValid)
            {
                Write(infoStr, book1.BookTitle, book2.BookTitle, book3.BookTitle);
                isValid = int.TryParse(ReadLine(), out userInput);

                if (!isValid)
                    WriteLine("Input error. Only integer input is allowed!");

                if (isValid && !options.Contains(userInput))
                {
                    WriteLine("Input error. Only input 1, 2, 3 or 4 is allowed!");
                    isValid = false;
                }
            }

            if (userInput == 4)
            {
                PerformUserAction(book1, book2, book3);
                return;
            }

            infoStr = "Enter the new counts for {0}: ";
            isValid = false;
            int updatedCount = 0;
            Book selectedBook = null;

            switch (userInput)
            {
                case 1:
                    selectedBook = book1;
                    break;
                case 2:
                    selectedBook = book2;
                    break;
                case 3:
                    selectedBook = book3;
                    break;
            }

            while (!isValid)
            {
                Write(infoStr, selectedBook.BookTitle);
                isValid = int.TryParse(ReadLine(), out updatedCount);

                if (isValid && updatedCount < 0)
                    isValid = false;

                if (!isValid)
                    WriteLine("Input error. Only integer input is allowed and that must be non-negative!");
            }
            selectedBook.BookCount = updatedCount;

            WriteLine("Great! Book Counts for {0} has been updated to {1}", selectedBook.BookTitle, selectedBook.BookCount);

            PerformUserAction(book1, book2, book3);
        }

        static void Main(string[] args)
        {
            Book[] bookList = new Book[] 
            {
                //new Book("title A", 17.3),
                //new Book("title B", 25.8),
                //new Book("title C", 39.4)
                new Book("title A", 9.99),
                new Book("title B", 14.99),
                new Book("title C", 17.99)
            };

            WriteLine("Welcome to wholesale book ordering system!");
            WriteLine("You can place orders for three different book counts!");

            WriteLine("\nThe books we have in stock are...");
            foreach (var book in bookList)
            {
                WriteLine("{0} with unit price {1:C2}", book.BookTitle, book.UnitPrice);
            }

            WriteLine("\n\nLet us begin by entering the counts for each of these books");
            foreach (var book in bookList)
            {
                LoadBookCount(book);
            }

            PerformUserAction(bookList[0], bookList[1], bookList[2]);
        }
    }
}
