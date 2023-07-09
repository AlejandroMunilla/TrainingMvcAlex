namespace TrainingAlex.Containers
{
    public class EmailContainer
    {
        public string Subject { get; set; } = "Default Subject";
        public string Body { get; set; } = "Default Body";

        public List<string> ToRecipients = new List<string>();

        public List<string> CcRecipients = new List<string>();  //Not used at the moment. Just in case for the future
        public List<string>? AttachmentName { get; set; }       //Not used at the moment. Just in case for the future
        public List<byte[]>? ContentBytesList { get; set; }     //Not used at the moment. Just in case for the future

    }
}
