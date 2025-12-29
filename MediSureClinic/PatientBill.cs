using System;
using System.Collections.Generic;
using System.Text;

namespace MediSureClinic
{
    /// <summary>
    /// Represents a billing record for a patient.
    /// Handles charge calculation, insurance discount, and final payable amount.
    /// </summary>
    public class PatientBill
    {
        #region Properties

        // Unique identifier for the bill
        public string BillId { get; set; }

        // Name of the patient
        public string PatientName { get; set; }

        // Indicates whether the patient has insurance
        public bool HasInsurance { get; set; }

        // Consultation charges
        public decimal ConsultationFee { get; set; }

        // Laboratory charges
        public decimal LabCharges { get; set; }

        // Medication charges
        public decimal MedicationCharges { get; set; }

        // Total amount before discount
        public decimal GrossAmount { get; set; }

        // Discount applied based on insurance
        public decimal DiscountAmount { get; set; }

        // Final amount payable after discount
        public decimal FinalPayable { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new PatientBill object and calculates billing amounts.
        /// </summary>
        /// <param name="BillId">Bill identifier</param>
        /// <param name="PatientName">Patient name</param>
        /// <param name="HasInsurance">Insurance status</param>
        /// <param name="ConsulationFee">Consultation fee</param>
        /// <param name="LabCharges">Lab charges</param>
        /// <param name="MedicationCharges">Medication charges</param>
        public PatientBill(string BillId, string PatientName, bool HasInsurance, decimal ConsulationFee, decimal LabCharges, decimal MedicationCharges)
        {
            // Assign basic bill details
            this.BillId = BillId;
            this.PatientName = PatientName;
            this.HasInsurance = HasInsurance;
            this.ConsultationFee = ConsulationFee;
            this.LabCharges = LabCharges;
            this.MedicationCharges = MedicationCharges;

            // Calculate gross amount
            GrossAmount = ConsultationFee + LabCharges + MedicationCharges;

            // Apply insurance discount if applicable
            if (HasInsurance)
            {
                DiscountAmount = GrossAmount / 10;
            }
            else
            {
                DiscountAmount = 0;
            }

            // Calculate final payable amount
            FinalPayable = GrossAmount - DiscountAmount;
        }

        #endregion
    }
}
