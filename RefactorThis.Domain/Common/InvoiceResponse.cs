namespace RefactorThis.Domain.Common
{
    public static class InvoiceResponse
    {
        #region Public Fields

        public const string InvalidInvoice = "The invoice is in an invalid state. It has an amount of 0 and it has payments.";
        public const string InvoiceNowFullyPaid = "The invoice is now fully paid.";
        public const string NoInvoiceFoundForPaymentReference = "There is no invoice matching this payment.";
        public const string NoPartialPaymentExistsAndAmountPaidExceedsInvoiceAmount = "The payment is greater than the invoice amount.";
        public const string NoPartialPaymentExistsAndAmountPaidIsLessThanInvoiceAmount = "The invoice is now partially paid.";
        public const string NoPaymentNeeded = "No payment needed.";
        public const string PartialPaymentExistsAndAmountPaidEqualsAmountDue = "The final partial payment received. The invoice is now fully paid.";
        public const string PartialPaymentExistsAndAmountPaidEqualsInvoiceAmount = "The invoice was already fully paid.";
        public const string PartialPaymentExistsAndAmountPaidExceedsAmountDue = "The payment is greater than the partial amount remaining.";
        public const string PartialPaymentExistsAndAmountPaidIsLessThanAmountDue = "Another partial payment received, still not fully paid.";

        #endregion
    }
}
