using System.Threading.Tasks;

namespace AuthService.Core.Interfaces
{
    public interface IAuthService
    {
        void RegisterMethod(IAuthMethod method);
        void Update();
        Task<AuthResponse> SignInWith(string methodId);
        Task<AuthResponse> LinkWith(string methodId);
        Task SignOut();
        AuthResponse CurrentAuth { get; }
    }
}