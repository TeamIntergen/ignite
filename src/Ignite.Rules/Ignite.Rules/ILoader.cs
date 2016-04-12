using System.Threading.Tasks;

namespace Ignite.Rules
{
    public interface ILoader
    {
        Task<string> Load(string path);
    }
}