using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OnTime.Game;
using OnTime.UI;
using UnityEngine;

public class FirstStartDataLoader : MonoBehaviour
{
    private SaveSystem saveSystem;
    private CharacterChanger characterChanger;
    private const string defaultColor = "Player";

    private void Awake()
    {
        characterChanger = FindObjectOfType<CharacterChanger>();
        saveSystem = new SaveSystem();
    }

    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + FileName.Player))
        {
            saveSystem.SaveInfo(defaultColor,0,0);
        }

        LoadOfCharacterData();
    }

    public void LoadOfCharacterData()
    {
        ShopItem[] item = characterChanger.GetShopsItems();
        bool[] isSelected = new bool[item.Length];
        bool[] isBought = new bool[item.Length];;
        for (int index = 0; index < item.Length; index++)
        {
           isSelected[index] = item[index].isSlected;
           isBought[index] = item[index].isBought;
        }
        saveSystem.SaveCharacterMenu(isSelected,isBought);
    }
}
