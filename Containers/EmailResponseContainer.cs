namespace TrainingAlex.Containers
{
    public class EmailResponseContainer
    {
        public string? StringMessage { get; set; }
        public enum EmailResponse
        {
            Debug,
            Sucess,
            Error
        }
        public EmailResponse MyEmailResponse { get; set; }
    }
}
