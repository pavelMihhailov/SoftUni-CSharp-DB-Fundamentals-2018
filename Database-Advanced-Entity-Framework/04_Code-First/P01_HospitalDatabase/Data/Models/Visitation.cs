using System;

namespace P01_HospitalDatabase.Data.Models
{
    public class Visitation
    {
        public Visitation()
        {
        }

        public Visitation(int visitationId, int doctorId, DateTime date, string comments, int patientId)
        {
            VisitationId = visitationId;
            DoctorId = doctorId;
            Date = date;
            Comments = comments;
            PatientId = patientId;
        }

        public int VisitationId { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
