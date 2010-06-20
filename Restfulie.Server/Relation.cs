namespace Restfulie.Server
{
    public class Relation
    {
        public string Name { get; private set;}
        public string Url { get; private set; }

        public Relation(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
