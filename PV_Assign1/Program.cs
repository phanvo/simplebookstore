using System;
using System.Linq;          // add this to use linq Contains() method in Array
using static System.Console;

namespace PV_Assign1
{
    class Program
    {
        static void ValidateUserInput(string infoStr, out int userInput, int[] options = null)
        {
            userInput = -1;
            bool isValid = false;
            while (!isValid)
            {
                Write(infoStr);
                isValid = int.TryParse(ReadLine(), out userInput);

                if (isValid)
                {
                    if (options != null && !options.Contains(userInput))
                    {
                        WriteLine("--- Input error. Only input {0} is allowed! ---", String.Join(", ", options));
                        isValid = false;
                    } 
                    else if (userInput < 0)
                    {
                        WriteLine("--- Input error. Only non-negative integer input is allowed! ---");
                        isValid = false;
                    }
                }
                else
                {
                    WriteLine("--- Input error. Only integer input is allowed! ---");
                }
            }
        }

        static void LoadBookCount(Book anyBook)
        {
            string infoStr = String.Format("Enter the book count for {0}: ", anyBook.BookTitle);
            ValidateUserInput(infoStr, out int userInput);
            anyBook.BookCount = userInput;
        }

        static void PerformUserAction(Book book1, Book book2, Book book3)
        {
            string infoStr = "\n\nWhat would you like to do?\n";
            infoStr += "Press 1 for View Order, Press 2 for Update Order, Press 3 for quitting the application: ";

            ValidateUserInput(infoStr, out int userInput, new int[] { 1, 2, 3 });

            switch (userInput)
            {
                case 1:
                    ViewOrder(book1, book2, book3);
                    break;
                case 2:
                    UpdateOrder(book1, book2, book3);
                    break;
                case 3:
                    Clear();
                    WriteLine("Thank you for placing an order with us. Good Bye!");
                    break;
            }
        }

        static void ViewOrder(Book book1, Book book2, Book book3)
        {
            WriteLine("\nOkay! Lets view your order!\n");

            string asteriskLine = new string('*', 74);
            WriteLine(asteriskLine);
            WriteLine(book1.ToString());
            WriteLine(book2.ToString());
            WriteLine(book3.ToString());

            double totalAfterTaxAndDiscount = GetOrderTotals(book1, book2, book3, out double totalBeforeTaxAndDiscount,
                                   out double taxes, out double discountAmount);

            string billCalculationStr = "*{0, 35}: {1, -35:C2}*\n";
            billCalculationStr += "*{2, 35}: {3, -35:C2}*\n";
            billCalculationStr += "*{4, 35}: {5, -35:C2}*\n";
            billCalculationStr += "*{6, 35}: {7, -35:C2}*";

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
            WriteLine("\nOkay! Lets update your order!\n");

            string infoStr = "\nPress 1 to update book counts for {0}\n";
            infoStr += "Press 2 to update book counts for {1}\n";
            infoStr += "Press 3 to update book counts for {2}\n";
            infoStr += "Press 4 to cancel Update Order\n";
            infoStr += "Enter your option: ";

            infoStr = String.Format(infoStr, book1.BookTitle, book2.BookTitle, book3.BookTitle);

            ValidateUserInput(infoStr, out int userInput, new int[] { 1, 2, 3, 4 });

            if (userInput == 4)
            {
                PerformUserAction(book1, book2, book3);
                return;
            }

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

            infoStr = String.Format("Enter the new counts for {0}: ", selectedBook.BookTitle);

            ValidateUserInput(infoStr, out int updatedCount);
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
                new Book("Blade in the Ice", 9.99),
                new Book("Clue of the Cold Pendant", 14.99),
                new Book("The Oaken Dagger", 17.99)
            };

            string welcomeStr = "Welcome to wholesale book ordering system!\n";
            welcomeStr += "You can place orders for three different book counts!\n";
            welcomeStr += "\nThe books we have in stock are...";

            WriteLine(welcomeStr);

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
