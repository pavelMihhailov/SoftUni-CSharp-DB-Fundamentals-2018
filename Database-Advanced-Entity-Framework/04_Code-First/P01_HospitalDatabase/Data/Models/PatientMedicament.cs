namespace P01_HospitalDatabase.Data.Models
{
    public class PatientMedicament
    {
        public PatientMedicament()
        {
        }

        public PatientMedicament(int patientId, int medicamentId)
        {
            PatientId = patientId;
            MedicamentId = medicamentId;
        }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public int MedicamentId { get; set; }

        public Medicament Medicament { get; set; }
    }
}
