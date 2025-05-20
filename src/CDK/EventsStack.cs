using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SQS;
using Constructs;


namespace CDK
{
    public class EventsStack : Stack
    {

        // Topics
        public Topic ParcelIngestedTopic { get; private set; }
        public Topic LocationIngestedTopic { get; private set; }
        public Topic ServiceIngestedTopic { get; private set; }
        public Topic ProductIngestedTopic { get; private set; }

        public EventsStack(Construct scope = null, string id = null, IStackProps props = null) : base(scope, id, props)
        {
            ParcelIngestedTopic = new Topic(this, "parcel-ingested", new TopicProps
            {
                TopicName = "parcel-ingested"
            });

            LocationIngestedTopic = new Topic(this, "location-info-ingested", new TopicProps
            {
                TopicName = "location-ingested"
            });

            ServiceIngestedTopic = new Topic(this, "service-info-ingested", new TopicProps
            {
                TopicName = "service-info-ingested"
            });

            ProductIngestedTopic = new Topic(this, "product-info-ingested", new TopicProps
            {
                TopicName = "product-info-ingested"
            });

        }

    }
}
