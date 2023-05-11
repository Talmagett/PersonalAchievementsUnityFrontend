using System;
using TMPro;
using UnityEngine;

public class LoginView : MonoBehaviour
{
    [Space]
    [SerializeField] private TMP_InputField _emailIF;
    [SerializeField] private TMP_InputField _passwordIF;

    public event Action<LoginController.UserLogin> OnClick;
    public void Login()
    {
        bool isValidated = true;
        if (string.IsNullOrWhiteSpace(_emailIF.text))
        {
            MessageController.Instance.AlertInputValidation(_emailIF);
            isValidated = false;
        }

        if (string.IsNullOrWhiteSpace(_passwordIF.text))
        {
            MessageController.Instance.AlertInputValidation(_passwordIF);
            isValidated = false;
        }
        else if (_passwordIF.text.Length < 6)
        {
            MessageController.Instance.AlertInputValidation(_passwordIF);
            MessageController.Instance.CallMessage("Password", "Password is too short");
            isValidated = false;
        }

        if (!isValidated)
            return;

        LoginController.UserLogin user = new LoginController.UserLogin();
        user.email = _emailIF.text;
        user.password = _passwordIF.text;

        OnClick?.Invoke(user);
    }
}