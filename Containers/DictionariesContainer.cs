namespace TrainingAlex.Containers
{
    public class DictionariesContainer : IDisposable
    {
        public Dictionary<string, string> EnDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> EsDictionary = new Dictionary<string, string>();


    public void Dispose()
        {
            EnDictionary.Clear();
            EsDictionary.Clear();
        }
    }
}
