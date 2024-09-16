namespace clinimanagmanetsystem.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime Date { get; set; }
    }
}
