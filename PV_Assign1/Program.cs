using System;
using System.Linq;          // add this to use linq Contains() method in array
using static System.Console;

namespace PV_Assign1
{
    class Program
    {
        // utility method to support user input validation with 2 parameters and an optional parameter
        static void ValidateUserInput(string infoStr, out int userInput, int[] options = null)
        {
            userInput = -1;
            bool isValid = false;   // flag to check if user input is valid

            // loop if isValid = false
            while (!isValid)
            {
                Write(infoStr);     // display info
                isValid = int.TryParse(ReadLine(), out userInput);      // try to parse user input into integer

                // check if isValid = true, then proceed further validation
                if (isValid)
                {
                    // if parameter options is specified and user input value is not included in the options array
                    if (options != null && !options.Contains(userInput))
                    {
                        // print error and guide user; isValid now becomes false
                        // String.Join is used to display all values in the options array
                        WriteLine("--- Input error. Only input {0} is allowed! ---", String.Join(", ", options));
                        isValid = false;
                    } 
                    else if (userInput < 0)     // else, check if user input value is negative
                    {
                        // print error and guide user; isValid now becomes false
                        WriteLine("--- Input error. Only non-negative integer input is allowed! ---");
                        isValid = false;
                    }
                }
                else  // otherwise, print error and guide user
                {
                    WriteLine("--- Input error. Only integer input is allowed! ---");
                }
            }
        }

        static void LoadBookCount(Book anyBook, bool isUpdate = false)
        {
            // display info for user to input book count
            // flag isUpdate = true is for info to update count; otherwise, info to initialize count by default
            string infoStr = String.Format("Enter the {0} for {1}: ", isUpdate ? "new counts" : "book count",
                                                                      anyBook.BookTitle);

            // handle user input
            ValidateUserInput(infoStr, out int userInput);
            anyBook.BookCount = userInput;      // update book count
        }

        static void PerformUserAction(Book book1, Book book2, Book book3)
        {
            // display main menu info for user to input
            string infoStr = "\n\nWhat would you like to do?\n";
            infoStr += "Press 1 for View Order, Press 2 for Update Order, Press 3 for quitting the application: ";

            // handle user input with an additional restriction to a given list
            ValidateUserInput(infoStr, out int userInput, new int[] { 1, 2, 3 });

            // based on user input, execute respective method
            switch (userInput)
            {
                case 1:     // display current order
                    ViewOrder(book1, book2, book3);
                    break;
                case 2:     // allow to update book count for the current order
                    UpdateOrder(book1, book2, book3);
                    break;
                case 3:     // clear screen, display a goodbye message and exit the program
                    Clear();
                    WriteLine("Thank you for placing an order with us. Good Bye!");
                    break;
            }
        }

        static void ViewOrder(Book book1, Book book2, Book book3)
        {
            WriteLine("\nOkay! Lets view your order!\n");

            // create and display a string line with asterisks, 74 in total
            string asteriskLine = new string('*', 74);
            WriteLine(asteriskLine);

            // display book details for 3 books
            WriteLine(book1.ToString());
            WriteLine(book2.ToString());
            WriteLine(book3.ToString());

            // call GetOrderTotals method to compute order prices
            double totalAfterTaxAndDiscount = GetOrderTotals(book1, book2, book3, out double totalBeforeTaxAndDiscount,
                                   out double taxes, out double discountAmount);

            // concatenate strings to form complete order prices with indentation; display currency with precision range
            string billCalculationStr = "*{0, 35}: {1, -35:C2}*\n";
            billCalculationStr += "*{2, 35}: {3, -35:C2}*\n";
            billCalculationStr += "*{4, 35}: {5, -35:C2}*\n";
            billCalculationStr += "*{6, 35}: {7, -35:C2}*";

            // format the concatenated string
            WriteLine(billCalculationStr, "Total before tax and discount", totalBeforeTaxAndDiscount,
                                          "Taxes", taxes,
                                          "Discount", discountAmount,
                                          "Total after tax and discount", totalAfterTaxAndDiscount);
            WriteLine(asteriskLine);    // reuse and display a string line with asterisks

            PerformUserAction(book1, book2, book3);     // call PerformUserAction to continue user input in main menu
        }

        static double GetOrderTotals(Book book1, Book book2, Book book3,
                                   out double totalBeforeTaxAndDiscount,
                                   out double taxes, out double discountAmount)
        {
            // compute total before adding tax and subtracting discount
            totalBeforeTaxAndDiscount = book1.BookSubTotal + book2.BookSubTotal + book3.BookSubTotal;
            taxes = totalBeforeTaxAndDiscount * 0.07;       // compute 7% tax

            // compute 10% discount with pre-tax total > 300; otherwise, no discount
            discountAmount = totalBeforeTaxAndDiscount > 300 ? totalBeforeTaxAndDiscount * 0.1 : 0;

            // compute total after adding tax and subtracting discount
            double totalAfterTaxAndDiscount = totalBeforeTaxAndDiscount + taxes - discountAmount;
            return totalAfterTaxAndDiscount;
        }

        static void UpdateOrder(Book book1, Book book2, Book book3)
        {
            WriteLine("\nOkay! Lets update your order!\n");

            // concatenate strings to form available options
            string infoStr = "\nPress 1 to update book counts for {0}\n";
            infoStr += "Press 2 to update book counts for {1}\n";
            infoStr += "Press 3 to update book counts for {2}\n";
            infoStr += "Press 4 to cancel Update Order\n";
            infoStr += "Enter your option: ";

            // format the concatenated string
            infoStr = String.Format(infoStr, book1.BookTitle, book2.BookTitle, book3.BookTitle);

            // handle user input with an additional restriction to a given list
            ValidateUserInput(infoStr, out int userInput, new int[] { 1, 2, 3, 4 });

            // check if user input = 4, then call PerformUserAction to continue user input in main menu
            // and skip the rest logic of UpdateOrder method (i.e. user does not want to update order anymore)
            if (userInput == 4)
            {
                PerformUserAction(book1, book2, book3);
                return;
            }

            // otherwise, continue to process updating order for a selected book
            Book selectedBook = null;

            // set selected book based on user input
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

            //// format string with book title
            //infoStr = String.Format("Enter the new counts for {0}: ", selectedBook.BookTitle);

            //// handle user input for book count
            //ValidateUserInput(infoStr, out int updatedCount);
            //selectedBook.BookCount = updatedCount;

            // call LoadBookCount to process updating book count for selected book, add flag true to indicate updating
            LoadBookCount(selectedBook, true);

            // confirm updating book count successfully 
            WriteLine("Great! Book Counts for {0} has been updated to {1}", selectedBook.BookTitle, selectedBook.BookCount);

            PerformUserAction(book1, book2, book3);     // call PerformUserAction to continue user input in main menu
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
