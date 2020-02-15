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
                        //checkContinue = true;
                        //ViewOrder(book1, book2, book3);
                        break;
                    case 2:
                        WriteLine("Case 2");
                        //checkContinue = true;
                        //UpdateOrder(book1, book2, book3);
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
            billCalculationStr += "*{6, 46}: {7, -50:C2}*\n";

            WriteLine(billCalculationStr, "Total before tax and discount", totalBeforeTaxAndDiscount,
                                          "Taxes", taxes,
                                          "Discount", discountAmount,
                                          "Total after tax and discount", totalAfterTaxAndDiscount);
            WriteLine(asteriskLine);


            //PerformUserAction(book1, book2, book3);
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


            //PerformUserAction(book1, book2, book3);
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

            foreach (var item in bookList)
            {
                LoadBookCount(item);
                //WriteLine(item.ToString());
            }

            //PerformUserAction(bookList[0], bookList[1], bookList[2]);
            ViewOrder(bookList[0], bookList[1], bookList[2]);
        }
    }
}
