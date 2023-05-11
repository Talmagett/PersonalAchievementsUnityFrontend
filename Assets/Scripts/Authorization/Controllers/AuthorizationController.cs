using UnityEngine;

public class AuthorizationController : MonoBehaviour
{
    [System.Serializable]
    public class TokenJson
    {
        public string token;
    }

    [SerializeField] private AuthorizationView _authorizationView;
    public static string UserToken { get; private set; } = null;
    
    public static void SetToken(string token)
    {
        if (token == null)
            UserToken = null;
        else
            UserToken = JsonUtility.FromJson<TokenJson>(token).token;
    }
    public void SetVisible(bool visible)
    {
        _authorizationView.SetVisible(visible);
    }
}