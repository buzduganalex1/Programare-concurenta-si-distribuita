namespace Imor.Business
{
    public static class ImorEnum
    {
        private const string ImorPrefix = "http://www.semanticweb.org/ImagesOntology#";

        public const string HasA = "http://www.semanticweb.org/ImagesOntology#hasA";

        public const string TagLabel = "http://www.semanticweb.org/ImagesOntology#label";

        public const string Image = "http://www.semanticweb.org/ImagesOntology#Image";

        public const string Tag = "http://www.semanticweb.org/ImagesOntology#Tag";

        public const string Description = "http://www.semanticweb.org/ImagesOntology#description";

        public const string RdfType = "http://www.w3.org/1999/02/22-rdf-syntax-ns#type";

        public const string Content = "http://www.semanticweb.org/ImagesOntology#Content";

        public const string IsSimilar = "http://www.semanticweb.org/ImagesOntology#similarTag";
        
        public static string GetUri(string value)
        {
            return $"{ImorPrefix}{value}";
        }
    }
}