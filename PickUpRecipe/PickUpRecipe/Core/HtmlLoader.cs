using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PickUpRecipe.Core
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = settings.BaseUrl;
        }

        public async Task<string> GetSourceByPageId()
        {
            var response = await client.GetAsync(url);

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                int responseLength = (int)response.Content.Headers.ContentLength;
                var sb = new StringBuilder(capacity: responseLength);
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var rdr = new StreamReader(responseStream))
                {
                    char[] charBuffer = new char[4096];

                    while (true)
                    {
                        var read = await rdr.ReadAsync(charBuffer, 0, charBuffer.Length).ConfigureAwait(false);
                        sb.Append(charBuffer, 0, read);

                        if (read == 0)
                        {
                            break;
                        }
                    }
                }

                return sb.ToString();
            }

            return null;
        }
    }
}
