//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ResidentJinn
{
    public static class GameManager
    {
        public static bool GameActive = false;
        public static Camera MainCam;
        public static Unit Jinn, Man, Wolf;
        public static Image FearImage;
        public static Text WinLoseText;
        public static GameObject WinLoseUI;
        public static float TimeLeft = 300;
        public static List<HouseObject> HouseObjects;
        // Start is called before the first frame update
        public static void Init(Camera mainCam, Unit jinn, Unit man, Unit wolf, Transform ObjectsParent, Image fearImage, Text winLoseText, GameObject winLoseUI)
        {
            WinLoseUI = winLoseUI;
            WinLoseText = winLoseText;
            MainCam = mainCam;
            Jinn = jinn;
            Man = man;
            Wolf = wolf;
            FearImage = fearImage;
            //objects
            HouseObjects = new List<HouseObject>();
            for (int i = 0; i < ObjectsParent.childCount; i++)
                HouseObjects.Add(ObjectsParent.GetChild(i).GetComponent<HouseObject>());
        }


        public static void Update()
        {
            if (!GameActive)
                return;

            if (TimeLeft <= 0)
                GameOver(false);

            TimeLeft -= Time.deltaTime;


        }


        public static void GameOver(bool Victory)
        {
            GameActive = false;
            WinLoseUI.SetActive(true);
            if (Victory)
            {
                //Win screen
                Debug.Log("You Win");
                WinLoseText.text = Boot.language == 0 ? "ﺖﺤﺠﻧ" : "You Won";
            }
            else
            {
                //Lose screen
                Debug.Log("You Lose");
                WinLoseText.text = Boot.language == 0 ? "ﺖﻠﺸﻓ" : "You Failed";
            }
        }

    }
}