using System;
using OnTime.Game;
using TMPro;
using UnityEngine;

namespace OnTime.UI
{
    public class CharacterChanger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buyButton;
        [SerializeField] private Renderer cube;
        private int index = 0;
        private int selectedColor = 0;
        private SaveSystem saveSystem;
        private PlayerData loadPlayerData; 
        [SerializeField] private ShopItem[] item;

        private void Awake()
        {
            saveSystem = new SaveSystem();
            loadPlayerData = saveSystem.LoadPlayerData();
        }

        private void Start()
        {
            LoadSaveData();
            BuyButtonTextChange();
            ChangeColor();
        }

        private void LoadSaveData()
        {
            LoadCharacterSaveData();
            if (!String.IsNullOrEmpty(loadPlayerData.skinIndex.ToString()))
            {
                item[0].isSlected = false;
                selectedColor = loadPlayerData.skinIndex;
                index = loadPlayerData.skinIndex;
                item[selectedColor].isSlected = true;
            }
        }
        
        public void RightButtonPress()
        {
            index++;
            if (index >= item.Length)
            {
                index = 0;
            }
            ChangeColor();
            BuyButtonTextChange();
        }
        
        public void LeftButtonPress()
        {
            index--;
            if (index < 0)
            {
                index = item.Length - 1;
            }
            ChangeColor();
            BuyButtonTextChange();
        }

        private void BuyButtonTextChange()
        {
            buyButton.text = item[index].Price.ToString();
            if (item[index].isBought)
            {
                buyButton.text = "SELECT";
            }
            if (item[index].isSlected)
            {
                buyButton.text = "SELECTED";
            }
        }

        public ShopItem GetSkin()
        {
            return item[index];
        }

        public void SetCurrentItemAsBought()
        {
            item[index].isBought = true;
            BuyButtonTextChange();
        }

        public void SelectSkin()
        {
            if (item[index].isBought)
            {
                item[selectedColor].isSlected = false;
                item[index].isSlected = true;
                var itemColor = item[index].characterColor;
                selectedColor = index;
                BuyButtonTextChange();
                saveSystem.SaveInfo(itemColor.name,loadPlayerData.coins,selectedColor);
                SaveCharacterMenuData(item);
            }
        }

        private void ChangeColor()
        {
            cube.material = item[index].characterColor;
        }

        public ShopItem[] GetShopsItems()
        {
            return item;
        }

        private void LoadCharacterSaveData()
        {
            for (int index = 0; index < item.Length; index++)
            {
                item[index].isSlected = saveSystem.LoadCharacterData().isSelected[index];
                item[index].isBought = saveSystem.LoadCharacterData().isBought[index];
            }
        }

        private void SaveCharacterMenuData(ShopItem[] item)
        {
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
}