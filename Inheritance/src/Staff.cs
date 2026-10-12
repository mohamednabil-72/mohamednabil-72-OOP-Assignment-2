using System;

namespace LibrarySystem
{
    public class Staff : Person
    {
        private readonly decimal _allowance;

        public DateTime HireDate { get; }
        public decimal MonthlySalary { get; private set; }

        protected Staff(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary, decimal allowance)
            : base(personId, fullName, phone)
        {
            if (monthlySalary <= 0)
                throw new ArgumentException("Monthly salary must be greater than zero.");
            if (allowance < 0)
                throw new ArgumentException("Allowance cannot be negative.");

            HireDate = hireDate;
            MonthlySalary = monthlySalary;
            _allowance = allowance;
        }

        public void GiveRaise(decimal percentage)
        {
            if (percentage <= 0)
                throw new ArgumentException("Raise percentage must be greater than zero.");

            MonthlySalary = MonthlySalary + MonthlySalary * percentage / 100m;
        }

        public decimal GetMonthlyPay()
        {
            return MonthlySalary + _allowance;
        }
    }

    public class Librarian : Staff
    {
        public Librarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary)
            : base(personId, fullName, phone, hireDate, monthlySalary, 0m)
        {
        }

        public void ProcessReturn(Loan loan, DateTime returnDate)
        {
            if (loan == null)
                throw new ArgumentException("Loan must not be null.");
            loan.Return(returnDate);
        }

        public void MarkAsLost(Loan loan)
        {
            if (loan == null)
                throw new ArgumentException("Loan must not be null.");
            loan.MarkAsLost();
        }
    }

    public class Shelver : Staff
    {
        public string Section { get; private set; }

        public Shelver(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary, string section)
            : base(personId, fullName, phone, hireDate, monthlySalary, 0m)
        {
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("Section must not be empty.");
            Section = section;
        }

        public void Reassign(string newSection)
        {
            if (string.IsNullOrWhiteSpace(newSection))
                throw new ArgumentException("Section must not be empty.");
            Section = newSection;
        }
    }

    public class HeadLibrarian : Staff
    {
        public HeadLibrarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary)
            : base(personId, fullName, phone, hireDate, monthlySalary, 400m)
        {
        }

        public void ChangeLateFee(LibraryItem item, decimal newFee)
        {
            if (item == null)
                throw new ArgumentException("Item must not be null.");
            item.SetBaseLateFee(newFee);
        }

        public void WithdrawItem(LibraryItem item)
        {
            if (item == null)
                throw new ArgumentException("Item must not be null.");
            item.Withdraw();
        }

        public void RestoreItem(LibraryItem item)
        {
            if (item == null)
                throw new ArgumentException("Item must not be null.");
            item.Restore();
        }
    }
}
