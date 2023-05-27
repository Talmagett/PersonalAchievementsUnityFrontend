﻿using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
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
    }

    public enum Role
    {
        Admin,
        Hunter
    }
    #endregion

    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;

    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _loginURL;
    [BoxGroup("Response")]
    [SerializeField] private string _signUpURL;

    [SerializeField] private AuthenticationPanelView _authenticationView;
    [SerializeField] private AuthView _authView;
    [SerializeField] private ProfileView _profileView;
    [Inject] private LoadingView _loading;

    private bool _isProcessing;
    private void OnEnable()
    {
        _authView.OnSignUpClick += OnSignUpClickedHandler;
        _authView.OnLogInClick += OnLoginClickedHandler;
        _profileView.OnSignOutClicked += OnSignOutClickedHandler;
    }

    private void OnDisable()
    {
        _authView.OnSignUpClick -= OnSignUpClickedHandler;
        _authView.OnLogInClick -= OnLoginClickedHandler;
        _profileView.OnSignOutClicked -= OnSignOutClickedHandler;
    }
    private void OnLoginClickedHandler(UserData userLogin)
    {
        string data = JsonUtility.ToJson(userLogin);
        if (_isProcessing)
            return;
        PostRequestLogin($"{_responseURL}/{_loginURL}", data);
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
        _authenticationView.SetVisible(true);
    }
    private async UniTask PostRequestLogin(string url, string json)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.POST, json);
        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200")
        {
            var response = JsonUtility.FromJson<AuthResponse>(result.Description);

            SetUserData(response.token, (response.role == "Admin") ? Role.Admin : Role.Hunter, response.id);
            _authenticationView.SetVisible(false);
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

    public static string UserToken { get; private set; } = null;
    public static Role UserRole { get; private set; } = Role.Hunter;
    public static string Id { get; private set; } = null;
    private void SetUserData(string? token, Role role = Role.Hunter, string id = null)
    {
        UserToken = token;
        UserRole = role;
        Id = id;
    }
}