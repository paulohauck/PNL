using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SQS;
using Constructs;


namespace CDK
{
    public class ApiGatewayStack : Stack
    {

        public RestApi ApiGateway { get; private set; }
        

        public ApiGatewayStack(Construct scope = null, string id = null, IStackProps props = null) : base(scope, id, props)
        {

            ApiGateway = new RestApi(this, "PliApi", new RestApiProps
            {
                RestApiName = "PLI API",
                Description = "PLI API"
            });

        }

    }
}
