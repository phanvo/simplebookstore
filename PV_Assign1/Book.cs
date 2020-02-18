using System;

namespace PV_Assign1
{
    class Book
    {
        // auto-implemented properties
        public string BookTitle { get; }    // read-only
        public double UnitPrice { get; }    // read-only
        public int BookCount { get; set; }

        // auto read-only computed property
        public double BookSubTotal
        {
            get
            {
                return UnitPrice * BookCount;
            }
        }

        // default constructor
        public Book()
        {
            BookTitle = "Mock Book";
            UnitPrice = 1.11;
        }

        // overloaded constructor with 2 parameters
        public Book(string title, double price)
        {
            BookTitle = title;
            UnitPrice = price;
        }

        // overridden ToString() method to display book details
        public override string ToString()
        {
            // concatenate strings to form the complete book details with indentation
            string resultStr = "*{0, 35}: {1, -35}*\n";
            resultStr += "*{2, 35}: {3, -35:C2}*\n";    // display currency with precision range
            resultStr += "*{4, 35}: {5, -35}*\n";
            resultStr += "*{6, 35}: {7, -35:C2}*\n";    // display currency with precision range
            resultStr += $"*{new string(' ', 72)}*";

            // format the concatenated string
            return String.Format(resultStr, "Title", BookTitle,
                                            "Unit Price", UnitPrice,
                                            "Book Count", BookCount,
                                            "Book Sub Total", BookSubTotal);
        }
    }
}
