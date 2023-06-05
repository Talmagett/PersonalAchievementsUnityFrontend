using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static SearchView;
public class SearchController : MonoBehaviour
{
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _getUsersURL;
    [SerializeField] private SearchView _searchView;
    private bool _isProcessing;
    [Inject] private LoadingView _loading;
    [Inject] private AuthController _authController;
    private void OnEnable()
    {
        _searchView.OnClick += OnClickHandler;
        _authController.OnSignOut += OnSignOutHandler;
    }
    private void OnDisable()
    {
        _searchView.OnClick -= OnClickHandler;
        _authController.OnSignOut -= OnSignOutHandler;
    }
    private void OnClickHandler(string value)
    {
        string data = value;

        if (_isProcessing)
            return;
        GetRequest($"{_responseURL}/{_getUsersURL}/{data}");
    }
    public void OnShow()
    {
    }
    public void OnSignOutHandler()
    {
        _searchView.Clear();
    }
    private async UniTask GetRequest(string url)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);


        _isProcessing = false;
        _loading.SetVisible(false);

        if (result.Code == "200" || result.Code == "204")
        {
            List<UserDto?> userList = JsonConvert.DeserializeObject<List<UserDto>>(result.Description);
            _searchView.ShowResult(userList);
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