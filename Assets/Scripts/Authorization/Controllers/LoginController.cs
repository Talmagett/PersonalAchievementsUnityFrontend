using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LoginController : MonoBehaviour
{
    [System.Serializable]
    public class UserLogin
    {
        public string email;
        public string password;
    }

    [SerializeField] private string _responseURL;
    [SerializeField] private LoginView _loginView;
    [Inject] private AuthorizationController _authorizationController;
    [Inject] private LoadingView _loading;
    public string? UserId { get; private set; }
    private bool _isProcessing;

    private void OnEnable()
    {
        _loginView.OnClick += OnClickedHandler;
    }
    private void OnDisable()
    {
        _loginView.OnClick -= OnClickedHandler;
    }
    private void OnClickedHandler(UserLogin userLogin)
    {
        string data = JsonUtility.ToJson(userLogin);
        if (_isProcessing)
            return;
        PostRequestLogin(_responseURL, data);
    }
    private async UniTask PostRequestLogin(string url, string json)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.POST, json);
        UserId = null;
        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200")
        {
            var token = result.Description;
            AuthorizationController.SetToken(token);
            MessageController.Instance.CallMessage("Authorized", "Welcome");
            _authorizationController.SetVisible(false);
        }
        else
        {
            MessageController.Instance.CallMessage("Unauthorized", "Wrong username or password");
        }
    }
}
