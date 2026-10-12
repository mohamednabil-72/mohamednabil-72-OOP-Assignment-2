using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Program
    {
        public static void Main()
        {
            Librarian librarian = new Librarian("S1", "Mona Adel", "0100000001", new DateTime(2020, 1, 1), 5000m);
            HeadLibrarian head = new HeadLibrarian("S2", "Karim Samy", "0100000002", new DateTime(2018, 5, 10), 8000m);
            Shelver shelver = new Shelver("S3", "Omar Nabil", "0100000003", new DateTime(2022, 3, 15), 3500m, "Fiction");

            StudentMember student = new StudentMember("M1", "Sara Hany", "0111111111");
            PremiumMember premium = new PremiumMember("M2", "Ali Tarek", "0122222222", 10m);

            Book book1 = new Book("B1", "Clean Code", 2m);
            Book book2 = new Book("B2", "The Pragmatic Programmer", 2m);
            Book book3 = new Book("B3", "Refactoring", 2m);
            Book book4 = new Book("B4", "Design Patterns", 2m);
            Dvd dvd = new Dvd("D1", "Inception", 3m);
            Magazine magazine = new Magazine("G1", "National Geographic", 4m);

            DateTime today = new DateTime(2026, 1, 1);

            Console.WriteLine("--- Withdrawn item ---");
            head.WithdrawItem(book1);
            try
            {
                student.Borrow("L1", book1, today);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rejected: " + ex.Message);
            }
            head.RestoreItem(book1);

            Console.WriteLine();
            Console.WriteLine("--- Item already on loan ---");
            student.Borrow("L2", book1, today);
            try
            {
                premium.Borrow("L3", book1, today);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rejected: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("--- Student borrowing a 4th item ---");
            student.Borrow("L4", book2, today);
            student.Borrow("L5", book3, today);
            try
            {
                student.Borrow("L6", book4, today);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rejected: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("--- Monthly pay ---");
            List<Staff> staffList = new List<Staff> { librarian, head, shelver };
            foreach (Staff staff in staffList)
            {
                Console.WriteLine(staff.FullName + " (" + staff.GetType().Name + "): " + staff.GetMonthlyPay());
            }

            Console.WriteLine();
            Console.WriteLine("--- Loan period and daily late fee ---");
            List<LibraryItem> items = new List<LibraryItem> { book4, dvd, magazine };
            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item.Title + " (" + item.GetType().Name + "): " + item.LoanPeriodDays + " days, " + item.GetDailyLateFee() + " per late day");
            }

            Console.WriteLine();
            Console.WriteLine("--- Premium member returns a DVD 5 days late ---");
            Loan dvdLoan = premium.Borrow("L7", dvd, today);
            DateTime returnDate = dvdLoan.DueDate.AddDays(5);
            librarian.ProcessReturn(dvdLoan, returnDate);
            Console.WriteLine("Due date: " + dvdLoan.DueDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("Return date: " + dvdLoan.ReturnDate.Value.ToString("yyyy-MM-dd"));
            Console.WriteLine("Late fee: " + dvdLoan.LateFee);
            Console.WriteLine("Reading points: " + premium.ReadingPoints);

            Console.WriteLine();
            Console.WriteLine("--- Returning the same loan twice ---");
            try
            {
                librarian.ProcessReturn(dvdLoan, returnDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rejected: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("--- Marking a returned loan as lost ---");
            try
            {
                librarian.MarkAsLost(dvdLoan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rejected: " + ex.Message);
            }
        }
    }
}
