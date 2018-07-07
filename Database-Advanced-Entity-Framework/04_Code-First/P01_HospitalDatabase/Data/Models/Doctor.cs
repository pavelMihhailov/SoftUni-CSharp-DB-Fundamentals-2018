using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
        }

        public Doctor(string name, string speciality)
        {
            Name = name;
            Speciality = speciality;
        }

        public int DoctorId { get; set; }
        
        public string Name { get; set; }

        public string Speciality { get; set; }

        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();
    }
}
