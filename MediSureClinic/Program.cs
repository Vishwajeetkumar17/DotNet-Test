using System;

namespace MediSureClinic
{

    /// <summary>
    /// Provides the main entry point and user interface logic for the MediSure Clinic billing application. Supports
    /// creating, viewing, and clearing patient bills through a console-based menu.
    /// </summary>
    public class Program
    {
        static PatientBill? LastBill;
        static bool HasLastBill;

        /// <summary>
        /// Prompts the user to enter patient billing information and creates a new patient bill record based on the
        /// provided input.
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
            if (!decimal.TryParse(Console.ReadLine(), out decimal cFee) || cFee < 0)
            {
                Console.WriteLine("Enter correct Consultation Fee.");
                return;
            }

            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal lCharge) || lCharge < 0)
            {
                Console.WriteLine("Enter correct Lab Charges.");
                return;
            }

            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal mCharge) || mCharge < 0)
            {
                Console.WriteLine("Enter correct Medicine Charges.");
                return;
            }

            PatientBill bill = new PatientBill(
                id,
                name,
                insured.Equals("Y", StringComparison.OrdinalIgnoreCase),
                cFee,
                lCharge,
                mCharge
            );

            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.\n");
        }

        /// <summary>
        /// Displays the details of the most recently created bill in a formatted output to the console.
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

        /// <summary>
        /// Clears the last bill information and resets the related state.
        /// </summary>
        public void CLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.\n");
        }

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

                if (!int.TryParse(Console.ReadLine(), out int choice))
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
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option.\n");
                        break;
                }
            }
        }
    }
}
