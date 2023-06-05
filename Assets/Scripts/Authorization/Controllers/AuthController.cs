using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class AuthController : MonoBehaviour
{
    #region Data
    [System.Serializable]
    public class UserData
    {
        public string email;
        public string password;
    }
    [System.Serializable]
    public class AuthResponse
    {
        public string token;
        public string role;
        public string id;
        public int premiumLevel;
    }
    [System.Serializable]
    public class BanUserDto
    {
        public string id;
        public int bannedDays;
    }
    public enum Role
    {
        Admin,
        Hunter
    }
    [System.Serializable]
    public class ObjectResult
    {
        public int statusCode;
        public object value;
    }
    #endregion

    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;

    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _loginURL;
    [BoxGroup("Response")]
    [SerializeField] private string _signUpURL;
    [BoxGroup("Response")]
    [SerializeField] private string _banUserURL;

    [SerializeField] private AuthenticationPanelView _authenticationView;
    [SerializeField] private AuthView _authView;
    [SerializeField] private ProfileView _profileView;
    [SerializeField] private UserBanView _userBanView;
    [Inject] private LoadingView _loading;

    public event Action OnSignOut;
    private bool _isProcessing;
    private void OnEnable()
    {
        _authView.OnSignUpClick += OnSignUpClickedHandler;
        _authView.OnLogInClick += OnLoginClickedHandler;
        _profileView.OnSignOutClicked += OnSignOutClickedHandler;
        _userBanView.OnBanned += OnUserBanHandler;
    }

    private void OnDisable()
    {
        _authView.OnSignUpClick -= OnSignUpClickedHandler;
        _authView.OnLogInClick -= OnLoginClickedHandler;
        _profileView.OnSignOutClicked -= OnSignOutClickedHandler;
        _userBanView.OnBanned -= OnUserBanHandler;
    }
    private void OnLoginClickedHandler(UserData userLogin)
    {
        if (_isProcessing)
            return;
        string data = JsonUtility.ToJson(userLogin);
        PostRequestLogin($"{_responseURL}/{_loginURL}", data);
    }
    private void OnUserBanHandler(BanUserDto banUserDto)
    {
        if (_isProcessing)
            return;
        string data = JsonUtility.ToJson(banUserDto);
        PostRequestBanUser($"{_responseURL}/{_banUserURL}", data);
    }
    private void OnSignUpClickedHandler(UserData userLogin)
    {
        string data = JsonUtility.ToJson(userLogin);
        if (_isProcessing)
            return;
        PostRequestSignUp($"{_responseURL}/{_signUpURL}", data);
    }
    private void OnSignOutClickedHandler()
    {
        SetUserData(null);
        OnSignOut?.Invoke();
        _authenticationView.SetVisible(true);
    }
    private async UniTask PostRequestLogin(string url, string json)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.POST, json, true);
        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;
        if (result.Code == "200")
        {
            var response = JsonUtility.FromJson<AuthResponse>(result.Description);
            SetUserData(response.token, (response.role == "Admin") ? Role.Admin : Role.Hunter, response.premiumLevel, response.id);
            _authenticationView.SetVisible(false);
        }
        else if (result.Code == "403")
        {
            MessageController.Instance.CallMessage("Banned", $"{result.Description}");
        }
        else
        {
            MessageController.Instance.CallMessage("Unauthorized", "Wrong username or password");
        }
    }
    private async UniTask PostRequestSignUp(string url, string json)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.POST, json);
        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200" || result.Code == "201")
        {
            PostRequestLogin($"{_responseURL}/{_loginURL}", json);
        }
        else if (result.Code == "400")
        {
            MessageController.Instance.CallMessage("Wrong email", "Email is registered");
        }
        else
        {
            MessageController.Instance.CallMessage("Unauthorized", "Wrong username or password");
        }
    }
    private async UniTask PostRequestBanUser(string url, string json)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.POST, json);
        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "201")
        {
            MessageController.Instance.CallMessage("Successful", "User was banned");
        }
        else
        {
            MessageController.Instance.CallMessage("Unauthorized", "Wrong username or password");
        }
    }
    public static string UserToken { get; private set; } = null;
    public static Role UserRole { get; private set; } = Role.Hunter;
    public static string Id { get; private set; } = null;
    public static int Level { get; private set; } = 20;
    public static void UpdateLevel(int level)
    {
        Level = level;
    }
    private void SetUserData(string? token, Role role = Role.Hunter, int level = 20, string id = null)
    {
        print(level);
        UserToken = token;
        UserRole = role;
        Level = level;
        Id = id;
    }
}