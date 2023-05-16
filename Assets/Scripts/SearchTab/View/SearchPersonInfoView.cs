using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
public class SearchPersonInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private SliderTextView _achievementsCountSlider;
    private string _username=null;
    public void SetData(string username, string count)
    {
        gameObject.SetActive(true);

        _usernameText.text = username;
        _username = username;
        string[] minMax = count.Split('/');
        if (int.TryParse(minMax[0], out int current))
        {
            _achievementsCountSlider.SetValue(current);
        }
        if (int.TryParse(minMax[1], out int max))
        {
            _achievementsCountSlider.SetMaxValue(max);
        }
    }
    public void ViewAchievementOfUser()
    {
        if (_username == null)
            return;

    }
}