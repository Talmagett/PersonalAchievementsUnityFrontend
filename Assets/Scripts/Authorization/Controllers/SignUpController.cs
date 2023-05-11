using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SignUpController : MonoBehaviour
{
    [SerializeField] private string _responseURL;
    [SerializeField] private SignUpView _signUpView;
    [Inject] private LoadingView _loading;

    private bool _isProcessing;
    private void OnEnable()
    {
        _signUpView.OnClick += OnClickedHandler;
    }
    private void OnDisable()
    {
        _signUpView.OnClick -= OnClickedHandler;
    }
    private void OnClickedHandler()
    {
        if (_isProcessing)
            return;
        PostRequestLogin(_responseURL);
    }

    private async UniTask PostRequestLogin(string url)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);

        _loading.SetVisible(false);
        _isProcessing = false;
        if (result is null)
            return;

        MessageController.Instance.CallMessage(result.Code, result.Description);
    }
}