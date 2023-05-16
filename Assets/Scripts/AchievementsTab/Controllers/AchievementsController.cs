using Codice.CM.Common;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class AchievementsController : MonoBehaviour
{
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _getByUsernameURL;
    [BoxGroup("Response")]
    [SerializeField] private string _getAllURL;
    [SerializeField] private SingleAchievementView _singleAchievementView;
    [Inject] private LoadingView _loading;
    [SerializeField]
    private int _startIndex;
    [SerializeField]
    private int _endIndex;
    public void GetAchievement()
    {
        GetRequestAchievement($"{_responseURL}/{_getByUsernameURL}/1");
    }
    public void GetAllAchievements()
    {
        GetAllAchievementsRequest($"{_responseURL}/{_getAllURL}?startIndex={_startIndex}&endIndex={_endIndex}");
    }
    public void GetAllAchievements(string userName)
    {
        GetAllAchievementsRequest($"{_responseURL}/{_getAllURL}?startIndex={_startIndex}&endIndex={_endIndex}&userId={userName}");
    }
    private async UniTask GetRequestAchievement(string url)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);

        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200")
        {
            //MessageController.Instance.CallMessage(result.Code, result.Description);

            var achievement = JsonUtility.FromJson<CrudAchievementController.CrudAchievement>(result.Description);

            print(result.Description);
            switch (achievement.progressType)
            {
                case CrudAchievementController.ProgressType.Single:
                    _singleAchievementView
                        .SetTitle(achievement.name)
                        .SetDescription(achievement.description)
                        .SetUnlocked(achievement.isUnlocked)
                        .SetGlobal(achievement.isGlobal)
                        .SetPrivate(achievement.isPrivate)
                        .SetNotes(achievement.notes)
                        .SetVisible(true);

                    break;
                case CrudAchievementController.ProgressType.Multiple:
                    break;
                case CrudAchievementController.ProgressType.Tasks:
                    break;
                case CrudAchievementController.ProgressType.Infinite:
                    break;
                default:
                    break;
            }
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
    private async UniTask GetAllAchievementsRequest(string url)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);

        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200")
        {
            //MessageController.Instance.CallMessage(result.Code, result.Description);

            //var achievement = JsonUtility.FromJson<CrudAchievementController.CrudAchievement>(result.Description);

            print(result.Description);
            /*
            switch (achievement.progressType)
            {
                case CrudAchievementController.ProgressType.Single:
                    _singleAchievementView
                        .SetTitle(achievement.name)
                        .SetDescription(achievement.description)
                        .SetUnlocked(achievement.isUnlocked)
                        .SetGlobal(achievement.isGlobal)
                        .SetPrivate(achievement.isPrivate)
                        .SetNotes(achievement.notes)
                        .SetVisible(true);

                    break;
                case CrudAchievementController.ProgressType.Multiple:
                    break;
                case CrudAchievementController.ProgressType.Tasks:
                    break;
                case CrudAchievementController.ProgressType.Infinite:
                    break;
                default:
                    break;
            }*/
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