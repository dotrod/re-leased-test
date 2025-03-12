namespace RefactorThis.Domain.Common
{
    public static class InvoiceResponse
    {
        #region Public Fields

        public const string FinalPartialPaymentReceived = "The final partial payment received. The invoice is now fully paid.";
        public const string InvalidInvoice = "The invoice is in an invalid state. It has an amount of 0 and it has payments.";
        public const string InvoiceAlreadyFullyPaid = "The invoice was already fully paid.";
        public const string InvoiceNowFullyPaid = "The invoice is now fully paid.";
        public const string InvoiceNowPartiallyPaid = "The invoice is now partially paid.";
        public const string NoInvoiceMatchingPayment = "There is no invoice matching this payment.";
        public const string NoPaymentNeeded = "No payment needed.";
        public const string PartialPaymentReceived = "Another partial payment received, still not fully paid.";
        public const string PaymentGreaterThanPartialAmount = "The payment is greater than the partial amount remaining.";
        public const string PaymentGreaterThanInvoiceAmount = "The payment is greater than the invoice amount.";

        #endregion
    }
}
