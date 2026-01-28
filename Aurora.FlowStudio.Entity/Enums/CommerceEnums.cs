namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// Address type
    /// </summary>
    public enum AddressType
    {
        Shipping = 1,
        Billing = 2,
        Both = 3,
        Warehouse = 4,
        Store = 5,
        Office = 6,
        Home = 7,
        Work = 8
    }

    /// <summary>
    /// Product status
    /// </summary>
    public enum ProductStatus
    {
        Active,
        Draft,
        Archived,
        OutOfStock
    }

    /// <summary>
    /// Cart status
    /// </summary>
    public enum CartStatus
    {
        Active,
        Abandoned,
        Completed,
        Expired
    }

    /// <summary>
    /// Order status
    /// </summary>
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Processing,
        Shipped,
        Delivered,
        Cancelled,
        Refunded,
        Failed
    }

    /// <summary>
    /// Payment status
    /// </summary>
    public enum PaymentStatus
    {
        Pending,
        Authorized,
        Paid,
        PartiallyPaid,
        Refunded,
        PartiallyRefunded,
        Voided,
        Failed
    }

    /// <summary>
    /// Fulfillment status
    /// </summary>
    public enum FulfillmentStatus
    {
        Unfulfilled,
        PartiallyFulfilled,
        Fulfilled,
        Shipped,
        Delivered,
        PickedUp,
        Cancelled
    }

    /// <summary>
    /// Transaction type
    /// </summary>
    public enum TransactionType
    {
        Payment,
        Refund,
        Authorization,
        Capture,
        Void
    }

    /// <summary>
    /// Transaction status
    /// </summary>
    public enum TransactionStatus
    {
        Pending,
        Success,
        Failed,
        Cancelled
    }

    /// <summary>
    /// Payment method
    /// </summary>
    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        PayPal,
        Stripe,
        BankTransfer,
        CashOnDelivery,
        Cryptocurrency,
        Wallet,
        Other
    }

    /// <summary>
    /// Product review status
    /// </summary>
    public enum ReviewStatus
    {
        Pending,
        Approved,
        Rejected,
        Flagged
    }

    /// <summary>
    /// Discount type
    /// </summary>
    public enum DiscountType
    {
        Percentage,
        FixedAmount,
        FreeShipping,
        BuyXGetY
    }

    /// <summary>
    /// Discount status
    /// </summary>
    public enum DiscountStatus
    {
        Active,
        Scheduled,
        Expired,
        Disabled
    }
}
