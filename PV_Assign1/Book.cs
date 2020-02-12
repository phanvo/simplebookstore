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
            string resultStr = "Title: {0}" +
                               "\nUnit Price: {1}" +
                               "\nBook Count: {2}" +
                               "\nBook Sub Total: {3}";
            return String.Format(resultStr, BookTitle, UnitPrice, BookCount, BookSubTotal);
        }
    }
}
