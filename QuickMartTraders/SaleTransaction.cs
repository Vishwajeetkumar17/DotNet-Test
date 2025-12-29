namespace QuickMartTraders
{
    /// <summary>
    /// Represents a sales transaction.
    /// Stores customer details, item information, amounts,
    /// and profit or loss calculation results.
    /// </summary>
    public class SaleTransaction
    {
        #region Transaction Details

        // Invoice number for the transaction
        public string InvoiceNo { get; set; }

        // Name of the customer
        public string CustomerName { get; set; }

        // Name of the item sold
        public string ItemName { get; set; }

        // Quantity of items sold
        public int Quantity { get; set; }

        // Total purchase amount
        public decimal PurchaseAmount { get; set; }

        // Total selling amount
        public decimal SellingAmount { get; set; }

        #endregion

        #region Profit / Loss Details

        // Indicates whether the transaction resulted in profit, loss, or break-even
        public string ProfitOrLossStatus { get; set; }

        // Amount of profit or loss
        public decimal ProfitOrLossAmount { get; set; }

        // Profit margin percentage
        public decimal ProfitMarginPercent { get; set; }

        #endregion
    }
}
