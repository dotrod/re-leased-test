using RefactorThis.Domain.Common;
using RefactorThis.Persistence;
using System;
using System.Linq;

namespace RefactorThis.Domain
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;

        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public string ProcessPayment(Payment payment)
        {
            var inv = _invoiceRepository.GetInvoice(payment.Reference);

            var responseMessage = string.Empty;

            if (inv == null)
            {
                throw new InvalidOperationException(InvoiceResponse.NoInvoiceMatchingPayment);
            }
            else
            {
                if (inv.Amount == 0)
                {
                    if (inv.Payments == null || !inv.Payments.Any())
                    {
                        responseMessage = InvoiceResponse.NoPaymentNeeded;
                    }
                    else
                    {
                        throw new InvalidOperationException(InvoiceResponse.InvalidInvoice);
                    }
                }
                else
                {
                    if (inv.Payments != null && inv.Payments.Any())
                    {
                        if (inv.Payments.Sum(x => x.Amount) != 0 && inv.Amount == inv.Payments.Sum(x => x.Amount))
                        {
                            responseMessage = InvoiceResponse.InvoiceAlreadyFullyPaid;
                        }
                        else if (inv.Payments.Sum(x => x.Amount) != 0 && payment.Amount > (inv.Amount - inv.AmountPaid))
                        {
                            responseMessage = InvoiceResponse.PaymentGreaterThanPartialAmount;
                        }
                        else
                        {
                            if ((inv.Amount - inv.AmountPaid) == payment.Amount)
                            {
                                switch (inv.Type)
                                {
                                    case InvoiceType.Standard:
                                        inv.AmountPaid += payment.Amount;
                                        inv.Payments.Add(payment);
                                        responseMessage = InvoiceResponse.FinalPartialPaymentReceived;
                                        break;
                                    case InvoiceType.Commercial:
                                        inv.AmountPaid += payment.Amount;
                                        inv.TaxAmount += payment.Amount * 0.14m;
                                        inv.Payments.Add(payment);
                                        responseMessage = InvoiceResponse.FinalPartialPaymentReceived;
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                            }
                            else
                            {
                                switch (inv.Type)
                                {
                                    case InvoiceType.Standard:
                                        inv.AmountPaid += payment.Amount;
                                        inv.Payments.Add(payment);
                                        responseMessage = InvoiceResponse.PartialPaymentReceived;
                                        break;
                                    case InvoiceType.Commercial:
                                        inv.AmountPaid += payment.Amount;
                                        inv.TaxAmount += payment.Amount * 0.14m;
                                        inv.Payments.Add(payment);
                                        responseMessage = InvoiceResponse.PartialPaymentReceived;
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (payment.Amount > inv.Amount)
                        {
                            responseMessage = InvoiceResponse.PaymentGreaterThanInvoiceAmount;
                        }
                        else if (inv.Amount == payment.Amount)
                        {
                            switch (inv.Type)
                            {
                                case InvoiceType.Standard:
                                    inv.AmountPaid = payment.Amount;
                                    inv.TaxAmount = payment.Amount * 0.14m;
                                    inv.Payments.Add(payment);
                                    responseMessage = InvoiceResponse.InvoiceNowFullyPaid;
                                    break;
                                case InvoiceType.Commercial:
                                    inv.AmountPaid = payment.Amount;
                                    inv.TaxAmount = payment.Amount * 0.14m;
                                    inv.Payments.Add(payment);
                                    responseMessage = InvoiceResponse.InvoiceNowFullyPaid;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else
                        {
                            switch (inv.Type)
                            {
                                case InvoiceType.Standard:
                                    inv.AmountPaid = payment.Amount;
                                    inv.TaxAmount = payment.Amount * 0.14m;
                                    inv.Payments.Add(payment);
                                    responseMessage = InvoiceResponse.InvoiceNowPartiallyPaid;
                                    break;
                                case InvoiceType.Commercial:
                                    inv.AmountPaid = payment.Amount;
                                    inv.TaxAmount = payment.Amount * 0.14m;
                                    inv.Payments.Add(payment);
                                    responseMessage = InvoiceResponse.InvoiceNowPartiallyPaid;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }

            inv.Save();

            return responseMessage;
        }
    }
}