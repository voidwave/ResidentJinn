//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
namespace ResidentJinn
{
    public static class GameManager
    {
        private static bool GameActive = true;
        private static Camera MainCam;
        public static Unit Jinn, Man, Wolf;
        private static float TimeLeft = 300;
        public static List<HouseObject> HouseObjects;
        // Start is called before the first frame update
        public static void Init(Camera mainCam, Unit jinn, Unit man, Unit wolf, Transform ObjectsParent)
        {
            MainCam = mainCam;
            Jinn = jinn;
            Man = man;
            Wolf = wolf;

            //objects
            HouseObjects = new List<HouseObject>();
            for (int i = 0; i < ObjectsParent.childCount; i++)
                HouseObjects.Add(ObjectsParent.GetChild(i).GetComponent<HouseObject>());
        }

        private static RaycastHit mouseHit;
        public static void Update()
        {
            if (!GameActive)
                return;

            if (TimeLeft <= 0)
                GameOver(false);

            TimeLeft -= Time.deltaTime;

            JinnInput();
        }

        private static void JinnInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                    if (Physics.Raycast(MainCam.ScreenPointToRay(Input.mousePosition), out mouseHit, 100))
                        Jinn.destination = new Vector3(mouseHit.point.x, Jinn.transform.localPosition.y, mouseHit.point.z);
            }
        }

        private static void GameOver(bool Victory)
        {
            GameActive = false;

            if (Victory)
            {
                //Win screen
                Debug.Log("You Win");
            }
            else
            {
                //Lose screen
                Debug.Log("You Lose");
            }
        }

    }
}