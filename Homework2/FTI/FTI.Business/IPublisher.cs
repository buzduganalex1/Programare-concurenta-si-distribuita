namespace FTI.Business
{
    public interface IPublisher
    {
        void PublishMessage(string message);

        void CreateTopic();
    }
}
