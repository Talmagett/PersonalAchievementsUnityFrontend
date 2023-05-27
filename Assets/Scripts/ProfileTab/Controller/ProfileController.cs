using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ProfileController : MonoBehaviour
{
    #region Data
    [System.Serializable]
    public class UserAccountDataDto
    {
        public string email;
        public string firstName;
        public string lastName;
        public string userName;
        public string phoneNumber;
    }
    #endregion
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _getUserDataURL;
    [BoxGroup("Response")]
    [SerializeField] private string _updateUserDataURL;
    [SerializeField] private ProfileView _profileView;

    [Inject] private LoadingView _loading;
    private void OnEnable()
    {
        _profileView.OnSaveClicked += OnSaveUserDataHandler;
    }
    private void OnDisable()
    {
        _profileView.OnSaveClicked -= OnSaveUserDataHandler;
    }
    public void OnSaveUserDataHandler(UserAccountDataDto userAccountDataDto)
    {
        string json = JsonUtility.ToJson(userAccountDataDto);
        PostUserData($"{_responseURL}/{_updateUserDataURL}", json);
    }
    public void Show()
    {
        GetUserData($"{_responseURL}/{_getUserDataURL}");
    }

    private async UniTask PostUserData(string url, string data)
    {
        _loading.SetVisible(true);
        print(url + ":" + data);
        var result = await APIService.SendRequest(url, APIService.RequestType.PUT, data);
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "201" || result.Code == "200")
        {
            MessageController.Instance.CallMessage("Succesfully", "Updated");
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
    private async UniTask GetUserData(string url)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);

        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200")
        {
            var data = JsonUtility.FromJson<UserAccountDataDto>(result.Description);
            _profileView.Show(data);
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