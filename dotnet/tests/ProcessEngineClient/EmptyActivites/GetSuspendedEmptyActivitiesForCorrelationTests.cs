namespace ProcessEngine.Client.Tests
{
    using System;
    using System.Threading.Tasks;

    using ProcessEngine.Client.Contracts;
    using ProcessEngine.Client.Tests.xUnit;
    using ProcessEngine.ConsumerAPI.Contracts.DataModel;

    using Xunit;

    [Collection("ProcessEngineClient collection")]
    public class GetSuspendedEmptyActivitiesForCorrelationTests : ProcessEngineBaseTest
    {
        private readonly ProcessEngineClientFixture fixture;

        public GetSuspendedEmptyActivitiesForCorrelationTests(ProcessEngineClientFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void GetSuspendedEmptyActivitiesForCorrelation_EmptyParameters_ShouldThrowException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await this
                .fixture
                .ProcessEngineClient
                .GetSuspendedEmptyActivitiesForCorrelation("")
            );
        }

        [Fact]
        public void GetSuspendedEmptyActivitiesForCorrelation_ProcessModelNotFound_ShouldThrowException()
        {
            Assert.ThrowsAsync<Exception>(async () => await this
                .fixture
                .ProcessEngineClient
                .GetSuspendedEmptyActivitiesForCorrelation("Test"));
        }

        [Fact]
        public async Task BPMN_GetSuspendedEmptyActivitiesForCorrelation_ShouldFetchEmptyActivityList()
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
                .GetSuspendedEmptyActivitiesForCorrelation(processInstance.CorrelationId);

            Assert.NotEmpty(emptyActivities);
        }
    }
}
