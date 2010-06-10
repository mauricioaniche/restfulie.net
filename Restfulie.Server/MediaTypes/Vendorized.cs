namespace Restfulie.Server.MediaTypes
{
    public class Vendorized : RestfulieMediaType
    {
        private readonly string[] synonyms;

        public Vendorized(string format)
        {
            synonyms = new[] {format};
        }

        public override string[] Synonyms
        {
            get { return synonyms; }
        }
    }
}
