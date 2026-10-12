using System;

namespace LibrarySystem
{
    public class LibraryItem
    {
        private readonly decimal _lateFeeMultiplier;

        public string CatalogNumber { get; }
        public string Title { get; }
        public int LoanPeriodDays { get; }
        public decimal BaseLateFee { get; private set; }
        public bool IsWithdrawn { get; private set; }
        public bool IsOnLoan { get; internal set; }

        protected LibraryItem(string catalogNumber, string title, int loanPeriodDays, decimal baseLateFee, decimal lateFeeMultiplier)
        {
            if (string.IsNullOrWhiteSpace(catalogNumber))
                throw new ArgumentException("Catalog number must not be empty.");
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.");
            if (loanPeriodDays <= 0)
                throw new ArgumentException("Loan period must be positive.");
            if (baseLateFee <= 0)
                throw new ArgumentException("Base late fee must be greater than zero.");
            if (lateFeeMultiplier <= 0)
                throw new ArgumentException("Late fee multiplier must be greater than zero.");

            CatalogNumber = catalogNumber;
            Title = title;
            LoanPeriodDays = loanPeriodDays;
            BaseLateFee = baseLateFee;
            _lateFeeMultiplier = lateFeeMultiplier;
        }

        public decimal GetDailyLateFee()
        {
            return BaseLateFee * _lateFeeMultiplier;
        }

        internal void SetBaseLateFee(decimal newFee)
        {
            if (newFee <= 0)
                throw new ArgumentException("Late fee must be greater than zero.");
            BaseLateFee = newFee;
        }

        internal void Withdraw()
        {
            IsWithdrawn = true;
        }

        internal void Restore()
        {
            IsWithdrawn = false;
        }
    }
}
