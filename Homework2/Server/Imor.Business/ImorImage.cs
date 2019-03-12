
using System.Collections.Generic;

namespace Imor.Business
{
    public class ImorImage
    {
        public static Dictionary<string, string> RdfPropertiesDictionary = new Dictionary<string, string>()
        {
            {ImorEnum.Description, "Description"},
            {ImorEnum.Content, "Content"}
        };
        
        public string Uri { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public IEnumerable<ImorTag> Tags { get; set; }        
    }
}