using System;

namespace LibrarySystem
{
    public class Loan
    {
        public string LoanId { get; }
        public DateTime BorrowDate { get; }
        public Member Member { get; }
        public LibraryItem Item { get; }
        public LoanStatus Status { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        public DateTime DueDate
        {
            get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
        }

        public decimal LateFee
        {
            get
            {
                if (ReturnDate == null)
                    return 0m;

                int daysLate = (ReturnDate.Value.Date - DueDate.Date).Days;
                if (daysLate <= 0)
                    return 0m;

                decimal fee = daysLate * Item.GetDailyLateFee();
                decimal discount = fee * Member.DiscountPercent / 100m;
                return fee - discount;
            }
        }

        internal Loan(string loanId, Member member, LibraryItem item, DateTime borrowDate)
        {
            if (string.IsNullOrWhiteSpace(loanId))
                throw new ArgumentException("Loan ID must not be empty.");
            if (member == null)
                throw new ArgumentException("A loan must have a member.");
            if (item == null)
                throw new ArgumentException("A loan must have an item.");

            LoanId = loanId;
            Member = member;
            Item = item;
            BorrowDate = borrowDate;
            Status = LoanStatus.Borrowed;
        }

        internal void Return(DateTime returnDate)
        {
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException("Only a loan with status Borrowed can be returned. Current status: " + Status + ".");
            if (returnDate < BorrowDate)
                throw new ArgumentException("Return date cannot be earlier than the borrow date.");

            ReturnDate = returnDate;
            Status = LoanStatus.Returned;
            Item.IsOnLoan = false;
        }

        internal void MarkAsLost()
        {
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException("Only a loan with status Borrowed can be marked as lost. Current status: " + Status + ".");

            Status = LoanStatus.Lost;
        }
    }
}
