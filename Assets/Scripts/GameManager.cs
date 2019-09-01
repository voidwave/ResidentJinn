//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

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
        public static float TimeLeft = 75;
        public static float TimeScale = 1;
        public static List<HouseObject> HouseObjects;
        public static PostProcessVolume postProcess;
        public static int RayLayerMask = 2;
        // Start is called before the first frame update
        public static void Init(Camera mainCam, Unit jinn, Unit man, Unit wolf, Transform ObjectsParent, Image fearImage, Text winLoseText, GameObject winLoseUI)
        {
            WinLoseUI = winLoseUI;
            WinLoseText = winLoseText;
            MainCam = mainCam;
            postProcess = mainCam.GetComponent<PostProcessVolume>();
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

            if (Boot.JinnDimension)
                TimeScale = 0.3f;
            else
                TimeScale = 1;

            Time.timeScale = Mathf.Lerp(Time.timeScale, TimeScale, Time.deltaTime);
            AudioManager.Update();
            postProcess.weight = Boot.JinnDimension ? Mathf.Lerp(postProcess.weight, 1, Time.deltaTime) : Mathf.Lerp(postProcess.weight, 0, Time.deltaTime);


            if (TimeLeft <= 0)
                GameOver(false, 3);

            TimeLeft -= Time.deltaTime;


        }

        private static string[,] Reasons = {
            { "نﺎﺴﻧﻻﺍ ﺖﻓﻮﺧ", "ﻚﻠﻛﺍ ﺐﺋﺬﻟﺍ", "ﻚﺗﻮﻗ ﺖﻬﺘﻧﺍ" , "ﺖﻗﻮﻟﺍ ﻰﻫﻰﺘﻧﺍ"},
             { "You scared the Human!", "The Wolf ate You!", "You ran out of Power", "You ran out of Time" } };
        public static void GameOver(bool Victory, int reason)
        {
            GameActive = false;
            WinLoseUI.SetActive(true);
            if (Victory)
            {
                //Win screen
                Debug.Log("You Win");
                WinLoseText.text = Boot.language == 0 ? "ﺖﺤﺠﻧ" : "You Won";
                WinLoseText.text += "\n";
                WinLoseText.text += Reasons[Boot.language, reason];
            }
            else
            {
                //Lose screen
                Debug.Log("You Lose");
                WinLoseText.text = Boot.language == 0 ? "ﺖﻠﺸﻓ" : "You Failed";
                WinLoseText.text += "\n";
                WinLoseText.text += Reasons[Boot.language, reason];
            }
        }

    }
}