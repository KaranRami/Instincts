using SQLite;

namespace Instict2K19.DataModel
{
    public class RegisterModel
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string RegisteredByGroup { get; set; }
        public string RegisteredBy { get; set; }
        public string CollgeName { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string SecondaryContactNumber { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string EventName { get; set; }
        public string ParticipantName { get; set; }
        public int? NumberOfParticipipants { get; set; }
        public double FeesCharged { get; set; }
    }
}
