namespace ProcessEngine.Client.Tests
{
    using System.Threading.Tasks;

    using ProcessEngine.Client.Contracts;
    using ProcessEngine.Client.Tests.xUnit;
    using ProcessEngine.ConsumerAPI.Contracts.DataModel;

    using Xunit;

    [Collection("ProcessEngineClient collection")]
    public class GetSuspendedManualTasksForProcessInstanceTests : ProcessEngineBaseTest
    {
        private readonly ProcessEngineClientFixture fixture;

        public GetSuspendedManualTasksForProcessInstanceTests(ProcessEngineClientFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task BPMN_GetSuspendedManualTasksForProcessInstance_ShouldFetchManualTaskList()
        {
            var processModelId = "test_consumer_api_manualtask";
            var payload = new ProcessStartRequest<object>();
            var callbackType = StartCallbackType.CallbackOnProcessInstanceCreated;

            var processInstance = await this
                .fixture
                .ProcessEngineClient
                .StartProcessInstance<object, object>(processModelId, "StartEvent_1", payload, callbackType);

            // Give the ProcessEngine time to reach the ManualTask
            await Task.Delay(1000);

            var manualTasks = await this
                .fixture
                .ProcessEngineClient
                .GetSuspendedManualTasksForProcessInstance(processInstance.ProcessInstanceId);

            Assert.NotEmpty(manualTasks);
        }

    }
}
