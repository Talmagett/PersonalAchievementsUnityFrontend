using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#nullable enable
public class CrudAchievementController : MonoBehaviour
{
    #region Data
    #endregion
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _createURL;
    [BoxGroup("Response")]
    [SerializeField] private string _editURL;
    [BoxGroup("Response")]
    [SerializeField] private string _deleteURL;

    [SerializeField] private CrudAchievementView _crudAchievementView;
    private bool _isProcessing;
    [Inject] private LoadingView _loading;
    private void OnEnable()
    {
        _crudAchievementView.OnClick += OnClickHandler;
        _crudAchievementView.OnDelete += OnDeleteHandler;
    }
    private void OnDisable()
    {
        _crudAchievementView.OnClick -= OnClickHandler;
        _crudAchievementView.OnDelete -= OnDeleteHandler;
    }
    private void OnClickHandler(AchievementDto crudAchievement)
    {
        string data = JsonConvert.SerializeObject(crudAchievement, formatting: Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        print(data);
        if (_isProcessing)
            return;
        PostRequestSave($"{_responseURL}/{(crudAchievement.id is null ? _createURL : _editURL)}", crudAchievement.id is null ? APIService.RequestType.POST : APIService.RequestType.PUT, data);
    }
    private void OnDeleteHandler(int id)
    {
        string data = JsonUtility.ToJson(id);
        if (_isProcessing)
            return;
        DeleteRequest($"{_responseURL}/{_deleteURL}/{id}");
    }
    private async UniTask PostRequestSave(string url, APIService.RequestType requestType, string json)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, requestType, json);

        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200" || result.Code == "201")
        {
            MessageController.Instance.CallMessage($"{(requestType == APIService.RequestType.POST ? "Created" : "Edited")}", "Achievement");
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
    private async UniTask DeleteRequest(string url)
    {
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.DELETE);

        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200" || result.Code == "201")
        {
            MessageController.Instance.CallMessage("Deleted", "Achievement");
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