using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Ignite.Rules
{
    public class WebLoader : ILoader
    {
        public async Task<string> Load(string path)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(path);
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        throw new ArgumentException($"unable to load response '{path}'");
                    }

                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}