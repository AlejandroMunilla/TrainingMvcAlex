namespace TrainingAlex.Models
{
    public class ContactModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Request { get; set; }

        public enum Services
        {
            IT,
            Mining
        }

        public Services Service { get; set; }

    }
}
