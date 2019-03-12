using System.Collections.Generic;
using Imor.Business;

namespace Imor.Api.Contracts
{
    public class CreateImageCommand
    {
        public string Uri { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}