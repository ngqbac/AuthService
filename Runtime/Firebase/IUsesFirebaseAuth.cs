#if USE_FIREBASE_AUTH
using Firebase.Auth;

namespace AuthService.Firebase
{
    public interface IUsesFirebaseAuth
    {
        FirebaseAuth MainAuth { get; set; }
        void SetFirebaseAuth(FirebaseAuth auth);
    }
}
#endif