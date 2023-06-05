using Sirenix.Utilities;
using System;
using TMPro;
using UnityEngine;
using static AuthController;

public class UserBanView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _banningDays;
    [SerializeField] private SearchPersonInfoView _searchPersonInfoView;
    public event Action<BanUserDto> OnBanned;
    public void BanUser()
    {
        if (_banningDays.text == "0" || _banningDays.text.IsNullOrWhitespace())
        {
            MessageController.Instance.AlertInputValidation(_banningDays);
            return;
        }
        if (int.TryParse(_banningDays.text, out int days))
        {
            BanUserDto banUserDto = new BanUserDto();
            banUserDto.id = _searchPersonInfoView.Id;
            banUserDto.bannedDays = days;
            OnBanned?.Invoke(banUserDto);
        }
    }
}