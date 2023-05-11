using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private string _urlSuffix;
    /*
    #region SignUp - Register

    [Space]
    [Header("Register")]
    [SerializeField] private TMP_InputField _registerEmailIF;
    [SerializeField] private TMP_InputField _registerUserNameIF;
    [SerializeField] private TMP_InputField _registerPasswordIF;
    [SerializeField] private Button _registerBtn;

    public void SignUp()
    {
        if (string.IsNullOrWhiteSpace(_registerEmailIF.text))
        {
            RedInputAlert(_registerEmailIF);
        }

        if (string.IsNullOrWhiteSpace(_registerUserNameIF.text))
        {
            RedInputAlert(_registerUserNameIF);
        }

        if (string.IsNullOrWhiteSpace(_registerPasswordIF.text))
        {
            RedInputAlert(_registerPasswordIF);
        }

        if (string.IsNullOrWhiteSpace(_registerEmailIF.text) || string.IsNullOrWhiteSpace(_registerUserNameIF.text) || string.IsNullOrWhiteSpace(_registerPasswordIF.text))
            return;

        UserLogin user = new UserLogin();
        user.email = _registerEmailIF.text;
        user.userName = _registerUserNameIF.text;
        user.password = _registerPasswordIF.text;

        string data = JsonUtility.ToJson(user);
        _registerBtn.interactable = false;
        StartCoroutine(PostRequestSignUp(ConnectionBuilder.Instance.URL + _urlSuffix, data));
    }

    private IEnumerator PostRequestSignUp(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.responseCode == 201)
        {
            StartCoroutine(PostRequestLogin(url + _loginUrlSuffix, json));
            print("Successful");
        }
        else if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            ConnectionBuilder.Instance.NetworkError();
            print(uwr.responseCode.ToString());
        }
        else
        {
            print(uwr.error);
        }
        uwr.uploadHandler.Dispose();
        uwr.downloadHandler.Dispose();
        uwr.Dispose();
        _registerBtn.interactable = true;
    }
    #endregion

    #region Forgot Password

    [Space]
    [Header("Forgot Password")]
    [SerializeField] private string _forgotPasswordSuffix;
    [SerializeField] private TMP_InputField _forgotPasswordEmailIF;
    [SerializeField] private Button _forgotPasswordBtn;
    [SerializeField] private GameObject _noUserWithThisEmail;
    [SerializeField] private GameObject _weSendEmail;

    [SerializeField] private string _emailForgotPassword;//hide

    public void ForgotPassword()
    {
        if (string.IsNullOrWhiteSpace(_forgotPasswordEmailIF.text))
        {
            RedInputAlert(_forgotPasswordEmailIF);
            return;
        }
        string data = _forgotPasswordEmailIF.text;
        _emailForgotPassword = data;
        print("email = "+_emailForgotPassword);
        _forgotPasswordBtn.interactable = false;
        StartCoroutine(PostRequestForgotPassword(ConnectionBuilder.Instance.URL + _urlSuffix + _forgotPasswordSuffix, data));
    }

    private IEnumerator PostRequestForgotPassword(string url, string json)
    {
        var uwr = new UnityWebRequest(url + "?email=" + json, "POST");
        yield return uwr.SendWebRequest();
        if (uwr.responseCode == 200)
        {
            _weSendEmail.SetActive(true);
            print("Successful");
        }
        else if (uwr.responseCode == 401)
        {
            _noUserWithThisEmail.SetActive(true);
            print("Incorrect email");
        }
        else if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            ConnectionBuilder.Instance.NetworkError();
            print(uwr.responseCode.ToString());
        }
        else
        {
            print(uwr.error);
        }
        uwr.Dispose();
        _forgotPasswordBtn.interactable = true;
    }
    #endregion

    #region Password Reset
    [Space]
    [Header("Password Reset")]
    [SerializeField] private string _passwordResetSuffix;
    [SerializeField] private GameObject _passwordResetWindow;
    [SerializeField] private TMP_InputField _passwordResetNewPasswordIF;
    [SerializeField] private TMP_InputField _passwordResetConfirmPasswordIF;
    [SerializeField] private Button _passwordResetBtn;
    [SerializeField] private GameObject _passwordChangedSuccessfully;
    private string _passwordResetToken = "";
    [Button("Reset Password With Token")]
    public void StartPasswordReset(string token)
    {
        if (_emailForgotPassword == "")
            return;
        _passwordResetToken = token;
        _passwordResetWindow.SetActive(true);
    }
    public void ResetPassword()
    {
        if (string.IsNullOrWhiteSpace(_passwordResetNewPasswordIF.text))
        {
            RedInputAlert(_passwordResetNewPasswordIF);
        }

        if (string.IsNullOrWhiteSpace(_passwordResetConfirmPasswordIF.text) || _passwordResetNewPasswordIF.text != _passwordResetConfirmPasswordIF.text)
        {
            RedInputAlert(_passwordResetConfirmPasswordIF);
        }

        if (string.IsNullOrWhiteSpace(_passwordResetNewPasswordIF.text) || string.IsNullOrWhiteSpace(_passwordResetConfirmPasswordIF.text)|| _passwordResetNewPasswordIF.text != _passwordResetConfirmPasswordIF.text)
            return;

        UserResetPassword user = new UserResetPassword();
        user.email = _emailForgotPassword;
        user.newPassword = _passwordResetNewPasswordIF.text;
        user.token = _passwordResetToken;
        string data = JsonUtility.ToJson(user);
        _passwordResetBtn.interactable = false;
        StartCoroutine(PostRequestResetPassword(ConnectionBuilder.Instance.URL + _urlSuffix+ _passwordResetSuffix, data));
    }

    private IEnumerator PostRequestResetPassword(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.responseCode == 201)
        {
            _passwordChangedSuccessfully.SetActive(true);
            print("Successful");
        }
        else if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            ConnectionBuilder.Instance.NetworkError();
            print(uwr.responseCode.ToString());
        }
        else
        {
            print(uwr.error);
        }
        uwr.uploadHandler.Dispose();
        uwr.downloadHandler.Dispose();
        uwr.Dispose();
        _passwordResetBtn.interactable = true;
    }
    #endregion

    #region Sign Out

    [Space]
    [Header("Sign Out")]
    [SerializeField] private Button _signOutBtn;
    public void SignOut()
    {
        _isLoggedIn = false;
        _signOutBtn.gameObject.SetActive(_isLoggedIn);
        ConnectionBuilder.Instance.SetToken(null);
        print("Signed out");
    }

    #endregion
    */

    

    #region UserData



    [System.Serializable]
    public class UserResetPassword
    {
        public string email;
        public string newPassword;
        public string token;
    }
    #endregion
}