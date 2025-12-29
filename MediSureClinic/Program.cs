using System;

namespace MediSureClinic
{
    /// <summary>
    /// Main program class for MediSure Clinic Billing system.
    /// Handles user interaction and bill management.
    /// </summary>
    public class Program
    {
        #region Fields

        // Stores the most recently created patient bill
        static PatientBill? LastBill;

        // Indicates whether a bill has been created
        static bool HasLastBill;

        #endregion

        #region Bill Creation

        /// <summary>
        /// Collects billing details from the user and creates a new PatientBill.
        /// Performs validation on all inputs.
        /// </summary>
        public void CreateUser()
        {
            Console.Write("Enter Bill Id: ");
            string? id = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Enter correct Bill ID.");
                return;
            }

            Console.Write("Enter Patient Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Enter correct Patient Name.");
                return;
            }

            Console.Write("Is the patient insured? (Y/N): ");
            string? insured = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(insured))
            {
                Console.WriteLine("Enter Y or N.");
                return;
            }

            Console.Write("Enter Consultation Fee: ");
            string? cFeeInput = Console.ReadLine();
            if (!decimal.TryParse(cFeeInput, out decimal cFee) || cFee < 0)
            {
                Console.WriteLine("Enter correct Consultation Fee.");
                return;
            }

            Console.Write("Enter Lab Charges: ");
            string? lChargeInput = Console.ReadLine();
            if (!decimal.TryParse(lChargeInput, out decimal lCharge) || lCharge < 0)
            {
                Console.WriteLine("Enter correct Lab Charges.");
                return;
            }

            Console.Write("Enter Medicine Charges: ");
            string? mChargeInput = Console.ReadLine();
            if (!decimal.TryParse(mChargeInput, out decimal mCharge) || mCharge < 0)
            {
                Console.WriteLine("Enter correct Medicine Charges.");
                return;
            }

            // Create a new PatientBill object using user input
            PatientBill bill = new PatientBill(
                id,
                name,
                insured.Equals("Y"),
                cFee,
                lCharge,
                mCharge
            );

            // Store the bill as the latest bill
            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.\n");
        }

        #endregion

        #region View Bill

        /// <summary>
        /// Displays details of the last created bill.
        /// </summary>
        public void VLastBill()
        {
            if (!HasLastBill || LastBill == null)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            Console.WriteLine("------------------- Last Bill -----------------");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {LastBill.HasInsurance}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicationCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("----------------------------------------------\n");
        }

        #endregion

        #region Clear Bill

        /// <summary>
        /// Clears the stored last bill and resets state.
        /// </summary>
        public void CLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.\n");
        }

        #endregion

        #region Application Entry Point

        /// <summary>
        /// Displays menu and controls application flow.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        public static void Main(string[] args)
        {
            Program p = new Program();
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("===== MediSure Clinic Billing =====");
                Console.WriteLine("1. Create New Bill");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Enter a valid option.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        p.CreateUser();
                        break;
                    case 2:
                        p.VLastBill();
                        break;
                    case 3:
                        p.CLastBill();
                        break;
                    case 4:
                        Console.WriteLine("Application closed.");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.\n");
                        break;
                }
            }
        }

        #endregion
    }
}
