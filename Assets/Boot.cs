﻿//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

namespace ResidentJinn
{
    public class Boot : MonoBehaviour
    {
        public bool StartedGame = false;
        public static int language = (int)Language.Arabic;
        public Unit Jinn, Man, Wolf;
        public Transform ObjectsParent, ParticleParent, AudioParent;
        public Text Status, Timer,
        Ablility0Title, Ablility1Title, Ablility2Title, Ablility3Title,
        Ablility0Desc, Ablility1Desc, Ablility2Desc, Ablility3Desc,
        GameDescription, SettingsText, WinLoseText, RestartGameText;
        private LanguageSwitcher switcher;
        public GameObject SettingsUI, WinLoseUI;
        public Image FearImage;
        public Image[] AbilityCDIcon;
        public AudioMixer audioMixer;

        void Start()
        {

            Application.targetFrameRate = 60;
            int minutes = (int)(GameManager.TimeLeft / 60);
            Timer.text = (language == (int)Language.Arabic ? "ﻲﻘﺒﺘﻣ ﻦﻣﺯ " : "Time Left ") + minutes + ":" + (GameManager.TimeLeft - minutes * 60).ToString("00");
            switcher = GetComponent<LanguageSwitcher>();
            switcher.Initilize();
            GameManager.Init(GetComponent<Camera>(), Jinn, Man, Wolf, ObjectsParent, FearImage, WinLoseText, WinLoseUI);
            ParticleManager.Initilaize(ParticleParent);
            AudioManager.Initilaize(AudioParent, audioMixer);
            ChangeLanguage(language);
            SettingsUI.SetActive(true);
            WinLoseUI.SetActive(false);
        }

        void Update()
        {
            GameManager.Update();
            UIUpdate();
            InputUpdate();
        }
        private RaycastHit mouseHit;
        private void InputUpdate()
        {
            if (!GameManager.GameActive)
                return;
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                    if (Physics.Raycast(GameManager.MainCam.ScreenPointToRay(Input.mousePosition), out mouseHit, 100))
                        Jinn.destination = new Vector3(mouseHit.point.x, Jinn.transform.localPosition.y, mouseHit.point.z);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                TriggerJinnAbility(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                TriggerJinnAbility(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                TriggerJinnAbility(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                TriggerJinnAbility(3);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                TriggerJinnAbility(3);
        }

        private float updateTimerRate = 5;
        private float currentUIUpdateTimer = 0;
        private void UIUpdate()
        {
            if (!GameManager.GameActive)
                return;

            currentUIUpdateTimer -= Time.deltaTime;

            //Jinn Ability Cooldowns UI update
            for (int i = 0; i < AbilityCDIcon.Length; i++)
                AbilityCDIcon[i].fillAmount = Jinn.AbilityCurrentCD[i] / Jinn.AbilityCoolDown[i];

            if (currentUIUpdateTimer <= 0)
            {
                int minutes = (int)(GameManager.TimeLeft / 60);
                //TimerUpdate
                Timer.text = (language == (int)Language.Arabic ? "ﻲﻘﺒﺘﻣ ﻦﻣﺯ " : "Time Left ") + minutes + ":" + (GameManager.TimeLeft - minutes * 60).ToString("00");
                //StatusUpdate
                string mood = "Chilling";
                if (Man.Mood > 40)
                    mood = language == 0 ? "ﺪﻴﻌﺳ" : "Happy";
                else if (Man.Mood <= 40 && Man.Mood >= -20)
                    mood = language == 0 ? "ﻞﻠﻣ" : "Bored";
                else if (Man.Mood < -20 && Man.Mood >= -50)
                    mood = language == 0 ? "ﻦﻳﺰﺣ" : "Sad";
                else if (Man.Mood < -50)
                    mood = language == 0 ? "ﺐﺌﺘﻜﻣ" : "Depressed";
                if (Man.ScaredTimer > 0)
                    mood = language == 0 ? "ﻒﺋﺎﺧ" : "Afraid";

                Status.text = FearText + Man.Fear.ToString("0.0") + "%\n" +
                              SinText + Man.Sin.ToString("0.0") + "%\n" +
                              MoodText + mood + "\n" +
                              JinnPowerText + Jinn.Power.ToString("0.0") + "%\n" +
                              WolfMoodText + Wolf.Mood.ToString("0.0") + "%";

                currentUIUpdateTimer = 1 / updateTimerRate;
            }



        }

        public void TriggerJinnAbility(int index)
        {
            if (!GameManager.GameActive)
                return;
            if (Jinn.AbilityCurrentCD[index] > 0)
                return;

            switch (index)
            {
                case 0:
                    //ability0
                    ParticleManager.Emit(Jinn.transform.localPosition, ParticleType.ShadowBomb);
                    if (Vector3.Distance(Jinn.transform.localPosition, Man.transform.localPosition) < 10)
                    {
                        Debug.Log("Scare Human");
                        Jinn.AbilityCurrentCD[0] = Jinn.AbilityCoolDown[0];
                        Man.Scared(10, Jinn.transform.localPosition);
                    }
                    break;
                case 1:
                    //ability1
                    ParticleManager.Emit(Jinn.transform.localPosition, ParticleType.ShadowFire);
                    Jinn.AbilityCurrentCD[1] = Jinn.AbilityCoolDown[1];
                    break;
                case 2:
                    //ability2
                    Jinn.AbilityCurrentCD[2] = Jinn.AbilityCoolDown[2];
                    break;
                case 3:
                    //ability3
                    Jinn.AbilityCurrentCD[3] = Jinn.AbilityCoolDown[3];
                    break;
                default:
                    break;
            }
        }


        private string FearText = "Human Fear: ";
        private string SinText = "Human Sin: ";
        private string MoodText = "Human Mood: ";
        private string JinnPowerText = "Jinn Health: ";
        private string WolfMoodText = "Wolf Health: ";
        public void ChangeLanguage(int newLanguage)
        {
            if (switcher == null)
                switcher = GetComponent<LanguageSwitcher>();

            language = newLanguage;
            string Start = language == 0 ? "أﺪﺑﺍ" : "Start";
            string Continue = language == 0 ? "ﻊﺑﺎﺗ" : "Continue";
            FearText = language == 0 ? "نﺎﺴﻧﻻﺍ فﻮﺧ: " : "Human Fear: ";
            SinText = language == 0 ? "نﺎﺴﻧﻻﺍ ﺐﻧﺫ: " : "Human Sin: ";
            MoodText = language == 0 ? "نﺎﺴﻧﻻﺍ جﺍﺰﻣ: " : "Human Mood: ";
            JinnPowerText = language == 0 ? "ﻲﻨﺠﻟﺍ ةﻮﻗ" : "Jinn Power: ";
            WolfMoodText = language == 0 ? "ﺐﺋﺬﻟﺍ جﺍﺰﻣ" : "Wolf Mood: ";
            SettingsText.text = StartedGame ? Continue : Start;
            RestartGameText.text = language == 0 ? "ىﺮﺧﺍ هﺮﻣ ﺐﻌﻟﺍ" : "Play Again";
            TextAnchor anchor = language == 0 ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
            //GameDescription
            GameDescription.text = switcher.GameDescription[language];
            GameDescription.alignment = anchor;
            //Ablitiy Titles and Descriptions
            Ablility0Title.text = switcher.Abilitiy0Title[language];
            Ablility1Title.text = switcher.Abilitiy1Title[language];
            Ablility2Title.text = switcher.Abilitiy2Title[language];
            Ablility3Title.text = switcher.Abilitiy3Title[language];

            Ablility0Desc.text = switcher.Abilitiy0Desc[language];
            Ablility1Desc.text = switcher.Abilitiy1Desc[language];
            Ablility2Desc.text = switcher.Abilitiy2Desc[language];
            Ablility3Desc.text = switcher.Abilitiy3Desc[language];

            // Ablility0Desc.alignment = anchor;
            // Ablility1Desc.alignment = anchor;
            // Ablility2Desc.alignment = anchor;
            // Ablility3Desc.alignment = anchor;

            int minutes = (int)(GameManager.TimeLeft / 60);
            //TimerUpdate
            Timer.text = (language == (int)Language.Arabic ? "ﻲﻘﺒﺘﻣ ﻦﻣﺯ " : "Time Left ") + minutes + ":" + (GameManager.TimeLeft - minutes * 60).ToString("00");
        }

        public void PauseGame(bool pause)
        {
            StartedGame = true;
            GameManager.GameActive = !pause;
            SettingsUI.SetActive(pause);
            Man.agent.isStopped = pause;
            Wolf.agent.isStopped = pause;
            //Jinn.agent.isStopped = pause;
            ChangeLanguage(language);
        }

        public void RestartGame()
        {
            WinLoseUI.SetActive(false);
            StartedGame = true;
            GameManager.GameActive = true;
            SettingsUI.SetActive(false);
            Man.ResetValues();
            Wolf.ResetValues();
            Jinn.ResetValues();
            ChangeLanguage(language);
        }

        public void MuteAudio(bool mute)
        {
            AudioManager.Mute(mute);
        }
    }

    public enum Language
    {
        Arabic = 0,
        English = 1
    }
}
