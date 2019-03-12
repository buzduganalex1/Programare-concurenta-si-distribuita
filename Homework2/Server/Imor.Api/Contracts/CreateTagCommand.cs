namespace Imor.Api.Contracts
{
    public class CreateTagCommand
    {
        public string Description { get; set; }

        public string Label { get; set; }

        public string Uri { get; set; }
    }
}