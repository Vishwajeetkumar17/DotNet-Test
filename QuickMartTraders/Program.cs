namespace QuickMartTraders
{
    /// <summary>
    /// Provides the main entry point and user interface logic for the QuickMart Traders sales transaction application.
    /// </summary>
    public class Program
    {
        static SaleTransaction? LastTransaction;
        static bool HasLastTransaction;

        /// <summary>
        /// Prompts the user to enter transaction details and creates a new sales transaction based on the provided
        /// input.
        /// </summary>
        /// <remarks>This method interacts with the console to collect transaction information, including
        /// invoice number, customer name, item name, quantity, purchase amount, and selling amount. It validates each
        /// input and will not create a transaction if any required value is missing or invalid. Upon successful entry,
        /// the transaction is saved and relevant calculations are performed. The most recent transaction is stored for
        /// later retrieval. This method is intended for interactive console applications.</remarks>
        public void CreateTransaction()
        {
            Console.Write("Enter Invoice No: ");
            string? invoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            string? customerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Customer Name cannot be empty.");
                return;
            }

            Console.Write("Enter Item Name: ");
            string? itemName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(itemName))
            {
                Console.WriteLine("Item Name cannot be empty.");
                return;
            }

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchaseAmount) || purchaseAmount <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than zero.");
                return;
            }

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal sellingAmount) || sellingAmount < 0)
            {
                Console.WriteLine("Selling Amount cannot be negative.");
                return;
            }

            SaleTransaction transaction = new SaleTransaction();
            transaction.InvoiceNo = invoiceNo;
            transaction.CustomerName = customerName;
            transaction.ItemName = itemName;
            transaction.Quantity = quantity;
            transaction.PurchaseAmount = purchaseAmount;
            transaction.SellingAmount = sellingAmount;

            Calculate(transaction);

            LastTransaction = transaction;
            HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            PrintCalculation(transaction);
            Console.WriteLine("-------------------------\n");
        }

        /// <summary>
        /// Displays the details of the most recent transaction to the console.
        /// </summary>
        /// <remarks>If there is no available transaction, a message is displayed prompting the user to
        /// create a new transaction. The output includes invoice number, customer name, item details, quantities,
        /// amounts, status, and profit or loss information.</remarks>
        public void ViewLastTransaction()
        {
            if (!HasLastTransaction || LastTransaction == null)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.\n");
                return;
            }

            Console.WriteLine("------ Last Transaction ------");
            Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
            Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
            Console.WriteLine($"Item: {LastTransaction.ItemName}");
            Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
            Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------\n");
        }

        /// <summary>
        /// Performs a recalculation using the most recent transaction, if available.
        /// </summary>
        /// <remarks>If no previous transaction exists, the method does not perform any calculation and
        /// displays a message indicating that a new transaction should be created first.</remarks>
        public void Recalculate()
        {
            if (!HasLastTransaction || LastTransaction == null)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.\n");
                return;
            }

            Calculate(LastTransaction);
            PrintCalculation(LastTransaction);
            Console.WriteLine("----------------------------------\n");
        }

        /// <summary>
        /// Calculates and updates the profit or loss status, amount, and profit margin percentage for the specified
        /// sale transaction.
        /// </summary>
        /// <remarks>This method modifies the provided transaction object in place. Ensure that the
        /// purchase amount is not zero to avoid division by zero errors when calculating the profit margin
        /// percentage.</remarks>
        /// <param name="transaction">The sale transaction to evaluate. Must not be null. The method updates the transaction's profit or loss
        /// status, amount, and profit margin percentage based on its purchase and selling amounts.</param>

        private void Calculate(SaleTransaction transaction)
        {
            if (transaction.SellingAmount > transaction.PurchaseAmount)
            {
                transaction.ProfitOrLossStatus = "PROFIT";
                transaction.ProfitOrLossAmount = transaction.SellingAmount - transaction.PurchaseAmount;
            }
            else if (transaction.SellingAmount < transaction.PurchaseAmount)
            {
                transaction.ProfitOrLossStatus = "LOSS";
                transaction.ProfitOrLossAmount = transaction.PurchaseAmount - transaction.SellingAmount;
            }
            else
            {
                transaction.ProfitOrLossStatus = "BREAK-EVEN";
                transaction.ProfitOrLossAmount = 0;
            }

            transaction.ProfitMarginPercent =
                (transaction.ProfitOrLossAmount / transaction.PurchaseAmount) * 100;
        }

        /// <summary>
        /// Outputs the profit or loss status, amount, and margin percentage of the specified sale transaction to the
        /// console.
        /// </summary>
        /// <param name="transaction">The sale transaction whose profit or loss details are to be printed. Cannot be null.</param>
        private void PrintCalculation(SaleTransaction transaction)
        {
            Console.WriteLine($"Status: {transaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {transaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {transaction.ProfitMarginPercent:F2}");
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            bool running = true;

            while (running)
            {
                Console.WriteLine("======= QuickMart Traders =========");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid option. Please enter a number.\n");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        program.CreateTransaction();
                        break;
                    case 2:
                        program.ViewLastTransaction();
                        break;
                    case 3:
                        program.Recalculate();
                        break;
                    case 4:
                        Console.WriteLine("Thank you. Application closed normally.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid menu option.\n");
                        break;
                }
            }
        }
    }
}
