using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Core.Interfaces;

namespace AuthService.Core
{
    public class AuthServiceManager : IAuthService
    {
        private readonly Dictionary<string, IAuthMethod> _methods = new();

        private readonly List<IAuthUpdatable> _updatableMethods = new();
        
        public void RegisterMethod(IAuthMethod method)
        {
            _methods[method.MethodId] = method;
#if USE_FIREBASE_AUTH
            if (method is IUsesFirebaseAuth usesFirebaseAuth) usesFirebaseAuth.SetFirebaseAuth(_mainAuth);
#endif
            if (method is IAuthUpdatable updatable) _updatableMethods.Add(updatable);
        }

        public void Update()
        {
            foreach (var method in _updatableMethods) method.Update();
        }

        public async Task<AuthResponse> SignInWith(string methodId)
        {
            if (!_methods.TryGetValue(methodId, out var method) || !method.IsAvailable())
            {
                return new AuthResponse { Success = false };
            }

            CurrentAuth = await method.SignInAsync();
            return CurrentAuth;
        }

        public async Task<AuthResponse> LinkWith(string methodId)
        {
            if (!_methods.TryGetValue(methodId, out var method) || !method.IsAvailable())
            {
                return new AuthResponse { Success = false };
            }
            return await method.LinkAsync();
        }

        public async Task SignOut()
        {
            if (CurrentAuth == null) return;
            if (_methods.TryGetValue(CurrentAuth.ProviderId, out var method)) await method.SignOutAsync();
            CurrentAuth = null;
        }

        public AuthResponse CurrentAuth { get; private set; }
    }
}