using System.Collections.Generic;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;

namespace Imor.Database
{
    public class SparQlRepository
    {
        public string Execute(string query)
        {
            var stringResults = new List<string>();

            var ds = new InMemoryDataset(DatabaseInitializer.Initialize());

            var sparqlparser = new SparqlQueryParser();

            ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);

            var sparqlQuery = sparqlparser.ParseFromString(query);

            var results = processor.ProcessQuery(sparqlQuery);

            if (results is SparqlResultSet)
            {
                var sparqlResultSet = (SparqlResultSet) results;

                foreach (var entry in sparqlResultSet)
                {
                    stringResults.Add(entry.ToString());
                }
            }

            return string.Join(",", stringResults);
        }
    }
}

