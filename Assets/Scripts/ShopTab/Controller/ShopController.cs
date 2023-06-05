using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ShopController : MonoBehaviour
{
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _postUpgradeRequestURL;
    [SerializeField] private ShopView _shopView;
    [Inject] private LoadingView _loading;
    private bool _isProcessing;
    private void OnEnable()
    {
        _shopView.OnUpgrade += Upgrade;
    }
    private void OnDisable()
    {
        _shopView.OnUpgrade -= Upgrade;
    }
    public void Upgrade(int level)
    {
        if (_isProcessing)
            return;
        PostUpgradeRequest($"{_responseURL}/{_postUpgradeRequestURL}?level=", level);
    }

    private async UniTask PostUpgradeRequest(string url, int level)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        print($"{url}{level}");
        var result = await APIService.SendRequest($"{url}{level}", APIService.RequestType.PUT);

        _isProcessing = false;
        _loading.SetVisible(false);

        if (result.Code == "200")
        {
            MessageController.Instance.CallMessage("Success", "Added");
            AuthController.UpdateLevel(level);
        }
        else if (result.Code == "401")
        {
            MessageController.Instance.CallMessage("Unauthorized", "Wrong username or password");
        }
        else
        {
            MessageController.Instance.CallMessage(result.Code, result.Description);
        }
    }
}