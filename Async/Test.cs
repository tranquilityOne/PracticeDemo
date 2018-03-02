using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncModel
{
    public class Test
    {
        public Task<string> GetHtmlAsync()
        {
            // Execution is synchronous here
            var client = new HttpClient();
            var result = client.GetStringAsync("http://www.dotnetfoundation.org");

            Console.WriteLine(result.Result);
            return result;
        }


        public async Task<string> GetFirstCharactersCountAsync(int count)
        {
            // Execution is synchronous here
            var client = new HttpClient();

            // Execution of GetFirstCharactersCountAsync() is yielded to the caller here
            // GetStringAsync returns a Task<string>, which is *awaited*
            var page = await client.GetStringAsync("http://www.dotnetfoundation.org");

            // Execution resumes when the client.GetStringAsync task completes,
            // becoming synchronous again.

            if (count > page.Length)
            {
                Console.WriteLine(page);
                return page;
            }
            else
            {
                Console.WriteLine(page.Substring(0, count));
                return page.Substring(0, count);
            }
           
        }
    }
}
