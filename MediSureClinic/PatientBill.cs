using System;
using System.Collections.Generic;
using System.Text;

namespace MediSureClinic
{
    /// <summary>
    /// Represents a billing record for a patient, including consultation, laboratory, and medication charges, as well
    /// as insurance discounts and the final payable amount.
    /// </summary>
    /// 
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicationCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        public PatientBill(string BillId, string PatientName, bool HasInsurance, decimal ConsulationFee, decimal LabCharges, decimal MedicationCharges)
        {
            this.BillId = BillId;
            this.PatientName = PatientName;
            this.HasInsurance = HasInsurance;
            this.ConsultationFee = ConsulationFee;
            this.LabCharges = LabCharges;
            this.MedicationCharges = MedicationCharges;

            GrossAmount = ConsultationFee + LabCharges + MedicationCharges;
            if (HasInsurance)
            {
                DiscountAmount = GrossAmount / 10;
            }
            else
            {
                DiscountAmount = 0;
            }
            FinalPayable = GrossAmount - DiscountAmount;

        }
    }
}
