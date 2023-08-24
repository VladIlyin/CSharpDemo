using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Concurrency.Synchronization
{
    public partial class SynchronizationPrimitivesDemo
    {
        private HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromMilliseconds(500)
        };

        private SemaphoreSlim _httpCallGate = new(5);

        [DemoCaption("SemaphoreSlim - HttpClient calls")]
        public async Task Demo3()
        {
            await Task.WhenAll(GetTasks());

            IEnumerable<Task> GetTasks()
            {
                for (var i = 0; i < 100; i++)
                {
                    yield return YandexCall();
                }
            }

            async Task YandexCall()
            {
                try
                {
                    await _httpCallGate.WaitAsync();

                    var response = await _httpClient.GetAsync("http://www.yandex.ru");
                    Console.WriteLine(response.StatusCode);

                    _httpCallGate.Release();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
