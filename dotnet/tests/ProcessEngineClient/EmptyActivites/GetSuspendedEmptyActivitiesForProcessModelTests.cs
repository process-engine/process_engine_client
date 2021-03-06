namespace ProcessEngine.Client.Tests
{
    using System.Threading.Tasks;

    using ProcessEngine.Client.Contracts;
    using ProcessEngine.Client.Tests.xUnit;
    using ProcessEngine.ConsumerAPI.Contracts.DataModel;

    using Xunit;

    [Collection("ProcessEngineClient collection")]
    public class GetEmptyActivitiesForProcessModelTests : ProcessEngineBaseTest
    {
        private readonly ProcessEngineClientFixture fixture;

        public GetEmptyActivitiesForProcessModelTests(ProcessEngineClientFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task BPMN_GetEmptyActivitiesForProcessModel_ShouldFetchEmptyActivityList()
        {
            var processModelId = "test_consumer_api_emptyactivity";
            var payload = new ProcessStartRequest<object>();
            var callbackType = StartCallbackType.CallbackOnProcessInstanceCreated;

            var processInstance = await this
                .fixture
                .ProcessEngineClient
                .StartProcessInstance<object, object>(processModelId, "StartEvent_1", payload, callbackType);

            // Give the ProcessEngine time to reach the EmptyActivity
            await Task.Delay(1000);

            var emptyActivities = await this
                .fixture
                .ProcessEngineClient
                .GetSuspendedEmptyActivitiesForProcessModel(processModelId);

            Assert.NotEmpty(emptyActivities);
        }
    }
}
