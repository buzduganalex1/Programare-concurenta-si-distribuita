using System;
using System.IO;
using System.Reflection;
using VDS.RDF;

namespace Imor.Database
{
    public static class DatabaseInitializer
    {
        public static string ontology = "C:\\ImagesOntologyV1.owl";

        public static IGraph Initialize()
        {
            return new Graph();
            
            IGraph g = new Graph();

            g.LoadFromUri(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));

            if (!string.IsNullOrEmpty(ontology))
            {
                g.LoadFromFile(ontology);
            }
            else
            {
                throw new Exception("Error: Ontology Database file could not be found at \"" + ontology + "\".");
            }

            return g;
        }
    }
}