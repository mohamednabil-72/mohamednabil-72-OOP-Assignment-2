using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Member : Person
    {
        private readonly List<Loan> _loans = new List<Loan>();

        public int MaxLoans { get; }
        public decimal DiscountPercent { get; }

        public IReadOnlyList<Loan> Loans
        {
            get { return _loans; }
        }

        protected Member(string personId, string fullName, string phone, int maxLoans, decimal discountPercent)
            : base(personId, fullName, phone)
        {
            if (maxLoans <= 0)
                throw new ArgumentException("Maximum loans must be positive.");
            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Discount percent must be between 0 and 100.");

            MaxLoans = maxLoans;
            DiscountPercent = discountPercent;
        }

        public int ActiveLoansCount
        {
            get
            {
                int count = 0;
                foreach (Loan loan in _loans)
                {
                    if (loan.Status == LoanStatus.Borrowed)
                        count++;
                }
                return count;
            }
        }

        public Loan Borrow(string loanId, LibraryItem item, DateTime borrowDate)
        {
            if (item == null)
                throw new ArgumentException("Item must not be null.");
            if (ActiveLoansCount >= MaxLoans)
                throw new InvalidOperationException(FullName + " has reached the maximum of " + MaxLoans + " active loans.");
            if (item.IsWithdrawn)
                throw new InvalidOperationException("'" + item.Title + "' is withdrawn from circulation and cannot be borrowed.");
            if (item.IsOnLoan)
                throw new InvalidOperationException("'" + item.Title + "' is already on loan.");

            Loan loan = new Loan(loanId, this, item, borrowDate);
            item.IsOnLoan = true;
            _loans.Add(loan);
            return loan;
        }
    }

    public class StudentMember : Member
    {
        public StudentMember(string personId, string fullName, string phone)
            : base(personId, fullName, phone, 3, 0m)
        {
        }
    }

    public class PremiumMember : Member
    {
        public PremiumMember(string personId, string fullName, string phone, decimal discountPercent)
            : base(personId, fullName, phone, 10, discountPercent)
        {
        }

        public int ReadingPoints
        {
            get
            {
                int returned = 0;
                foreach (Loan loan in Loans)
                {
                    if (loan.Status == LoanStatus.Returned)
                        returned++;
                }
                return returned * 5;
            }
        }
    }
}
