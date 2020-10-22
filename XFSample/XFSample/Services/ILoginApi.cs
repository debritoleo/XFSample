using Refit;
using System.Threading.Tasks;
using XFSample.Models;

namespace XFSample.Services
{
    public interface ILoginApi
    {
        [Get("/83599a37-9b03-47d1-970d-555f8835355c")]
        Task<Credential> Login(string email, string password);
    }
}
