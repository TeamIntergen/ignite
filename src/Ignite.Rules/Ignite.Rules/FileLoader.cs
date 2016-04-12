using System.IO;
using System.Threading.Tasks;

namespace Ignite.Rules
{
    public class FileLoader : ILoader
    {
        public Task<string> Load(string path)
        {
            if (File.Exists(path) &&  !string.IsNullOrEmpty(path))
            {
                var fileContents = File.ReadAllText(path);
                return Task.FromResult(fileContents);
            }

            throw new FileNotFoundException($"File does not exist at '{path}'");
        }
    }
}