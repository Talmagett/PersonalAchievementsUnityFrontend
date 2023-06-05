using TMPro;
using UnityEngine;

public class AchievementsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    public void SetCount(int current, int max)
    {
        _countText.gameObject.SetActive(max != -1);
        _countText.text = $"{current}/{max}";
    }
}