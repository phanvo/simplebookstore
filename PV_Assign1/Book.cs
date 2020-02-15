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
            string resultStr = "*{0, 46}: {1, -50}*\n";
            resultStr += "*{2, 46}: {3, -50}*\n";
            resultStr += "*{4, 46}: {5, -50}*\n";
            resultStr += "*{6, 46}: {7, -50}*\n";
            resultStr += $"*{new string(' ', 98)}*";
            return String.Format(resultStr, "Title", BookTitle,
                                            "Unit Price", UnitPrice,
                                            "Book Count", BookCount,
                                            "Book Sub Total", BookSubTotal);
        }
    }
}
