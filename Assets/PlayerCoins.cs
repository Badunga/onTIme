using System;
using System.Collections;
using System.Collections.Generic;
using OnTime.Game;
using OnTime.UI;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    private int coins;
    private SaveSystem saveSystem;
    private void Awake()
    {
        saveSystem = new SaveSystem();
        coins = saveSystem.LoadPlayerData().coins;
    }

    public int GetCalculatedCoinsAfterGameOver(int points)
    {
        var loadedData = saveSystem.LoadPlayerData();
        var calculatedCoins = points * 3;
        coins = coins + calculatedCoins;
        return calculatedCoins;
    }

    public void SaveCoinsAfterGameover()
    {
        var loadedData = saveSystem.LoadPlayerData();
        saveSystem.SaveInfo(loadedData.colors,coins,loadedData.skinIndex);
    }

    public int GetCoins()
    {
        return coins;
    }
}
