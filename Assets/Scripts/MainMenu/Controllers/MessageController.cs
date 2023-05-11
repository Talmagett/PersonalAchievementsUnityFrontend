using DG.Tweening;
using Sirenix.OdinInspector;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] private Transform _messagesParent;
    [SerializeField] private MessageView _messageView;
    [SerializeField] private Sprite _networkErrorSprite;

    public static MessageController Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public void AlertInputValidation(TMP_InputField inputField)
    {
        inputField.image.color = Color.red;
        inputField.image.DOKill();
        inputField.image.DOColor(Color.white, 1f).SetEase(Ease.InSine);
    }

    [Button]
    public void CallMessage(string title, string text, Sprite icon = null)
    {
        var view = Instantiate(_messageView, _messagesParent);
        view.SetTitle(title);
        view.SetDescription(text);
        view.SetIcon(icon);
        view.SubmitButton.onClick.AddListener(() => Destroy(view.gameObject));
    }

    public void CallNetworkErrorMessage()
    {
        CallMessage("Network error", "Please check your cellular or Wi-Fi connection and retry.", _networkErrorSprite);
    }
}