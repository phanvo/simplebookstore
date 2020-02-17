using System;
using System.Collections.Generic;
using System.Text;

namespace PV_Assign1
{
    class Book
    {
        public string BookTitle { get; }
        public double UnitPrice { get; }
        public int BookCount { get; set; }
        public double BookSubTotal
        { 
            get
            {
                return UnitPrice * BookCount;
            }
        }

        public Book(string title, double price)
        {
            BookTitle = title;
            UnitPrice = price;
        }

        public override string ToString()
        {
            string resultStr = "*{0, 35}: {1, -35}*\n";
            resultStr += "*{2, 35}: {3, -35:C2}*\n";
            resultStr += "*{4, 35}: {5, -35}*\n";
            resultStr += "*{6, 35}: {7, -35:C2}*\n";
            resultStr += $"*{new string(' ', 72)}*";
            return String.Format(resultStr, "Title", BookTitle,
                                            "Unit Price", UnitPrice,
                                            "Book Count", BookCount,
                                            "Book Sub Total", BookSubTotal);
        }
    }
}
