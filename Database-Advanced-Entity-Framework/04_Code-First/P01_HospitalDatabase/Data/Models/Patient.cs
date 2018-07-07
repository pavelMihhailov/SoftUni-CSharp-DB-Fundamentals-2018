using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
        }

        public Patient(int patientId, string firstName, string lastName, string address, string email, bool hasInsurance)
        {
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            HasInsurance = hasInsurance;
        }

        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; } = new List<PatientMedicament>();

        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();
    }
}
