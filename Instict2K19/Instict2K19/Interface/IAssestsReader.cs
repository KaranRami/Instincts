using System;
using System.Threading.Tasks;

namespace Instict2K19.Interface
{
    public interface IAssestsReader
    {
        Task<string> ReadAssets(string filename);
    }
}
