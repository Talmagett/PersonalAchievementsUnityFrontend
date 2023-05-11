using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AchievementsController : MonoBehaviour
{
    [SerializeField] private string _responseURL;

    [SerializeField] private SingleAchievementView _singleAchievementView;
    [Inject] private LoadingView _loading;

    public void GetAchievement()
    {
        GetRequestAchievement(_responseURL,1);
    }


    private async UniTask GetRequestAchievement(string url, int id)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url+"/"+id, APIService.RequestType.GET);
        
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
}