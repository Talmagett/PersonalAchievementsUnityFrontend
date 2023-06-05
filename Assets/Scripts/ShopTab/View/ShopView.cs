using System;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    public event Action<int> OnUpgrade;
    private int _buyingLevelAmount;
    public void Choose(int level)
    {
        _buyingLevelAmount = level;
    }
    public void BuyPremiumLevelConfirm()
    {
        print(_buyingLevelAmount);
        OnUpgrade?.Invoke(_buyingLevelAmount);
    }
}