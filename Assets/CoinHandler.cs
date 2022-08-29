using System;
using OnTime.Game;
using OnTime.UI;
using TMPro;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinDisplay;
    private CharacterChanger characterChanger;
    private int coins = 1000000;
    private SaveSystem data;
    
    private void Awake()
    {
        characterChanger = FindObjectOfType<CharacterChanger>();
        data = new SaveSystem();
    }
    
    void Start()
    { 
        if (!String.IsNullOrEmpty(data.LoadPlayerData().coins.ToString()))
        {
            coins = data.LoadPlayerData().coins;
        }
        coinDisplay.text = coins.ToString();
    }

    public void clickBuyButton()
    {
        var getData = data.LoadPlayerData();
        var isBought = characterChanger.GetSkin().isBought;
        var hasEnoughCoins = characterChanger.GetSkin().Price < coins;
        if (!isBought && hasEnoughCoins )
        {
            coins -= characterChanger.GetSkin().Price;
            coinDisplay.text = coins.ToString();
            characterChanger.SetCurrentItemAsBought();
            data.SaveInfo(getData.colors, coins,getData.skinIndex);
        }
    }
}
