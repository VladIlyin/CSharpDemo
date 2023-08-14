using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.AsyncAwait
{
    internal class DataProvider
    {
        // Method might not be used async/await
        public async Task<int> GetData()
        {
            var res = await Task.FromResult(0);

            return res;
        }

        // Async/await unnesseccary here
        public Task<int> GetDataAsync()
        {
            return GetData();
        }
    }
}
