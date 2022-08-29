using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using OnTime.UI;
using UnityEngine;

namespace OnTime.Game
{
    public class SaveSystem
    {
        private BinaryFormatter formatter;
        public SaveSystem()
        {
            formatter = new BinaryFormatter();
        }
        
        public void SaveInfo(string color, int coins, int index)
        {
            
            var stream = CreateSaveStream(FileMode.Create, FileName.Player);
            PlayerData data = new PlayerData();
            data.colors = color;
            data.coins = coins;
            data.skinIndex = index; 
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public PlayerData LoadPlayerData()
        {
            var stream = CreateSaveStream(FileMode.Open, FileName.Player);
            PlayerData data = (PlayerData) formatter.Deserialize(stream);
            stream.Close();
            return data;
        }

        private FileStream CreateSaveStream(FileMode mode, string fileName)
        {
            string path = Application.persistentDataPath + fileName;
            FileStream stream = new FileStream(path, mode);
            return stream;
        }
        
        public void SaveCharacterMenu(Boolean[] isSelect, Boolean[] isBought)
        {
            var stream = CreateSaveStream(FileMode.Create, FileName.Character);
            CharacterMenuData data = new CharacterMenuData();
            data.isBought = isBought;
            data.isSelected = isSelect;
            formatter.Serialize(stream, data);
            stream.Close();
        }
        
        public CharacterMenuData LoadCharacterData()
        {
            var stream = CreateSaveStream(FileMode.Open, FileName.Character);
            CharacterMenuData data = (CharacterMenuData) formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
    }
}