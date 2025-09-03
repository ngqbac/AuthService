using System.Threading.Tasks;
using AuthService.Core;
#if USE_FIREBASE_AUTH
using Firebase.Auth;
#endif

namespace AuthService
{
    public interface IAuthMethod
    {
        string MethodId { get; }
#if USE_FIREBASE_AUTH
        Credential Credential {get; set;}
#endif
        bool IsAvailable();
        Task<AuthResponse> SignInAsync();
        Task<AuthResponse> LinkAsync();
        Task SignOutAsync();
        void Log(object message);
    }
}