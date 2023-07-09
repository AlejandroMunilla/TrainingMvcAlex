namespace TrainingAlex.Containers
{
    [Serializable]
    public class TransportContainer
    {
        public EmailContainer? MyEmailContainer { get; set; }
        public EmailOptionsContainer? MyOptionsContainer { get; set; }
    }
}
