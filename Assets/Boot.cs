//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using System;
using UnityEngine;
using UnityEngine.UI;
namespace ResidentJinn
{
    public class Boot : MonoBehaviour
    {
        public bool StartedGame = false;
        public int language = (int)Language.Arabic;
        public Unit Jinn, Man, Wolf;
        public Transform ObjectsParent, ParticleParent;
        public Text Status, Timer,
        Ablility0Title, Ablility1Title, Ablility2Title, Ablility3Title,
        Ablility0Desc, Ablility1Desc, Ablility2Desc, Ablility3Desc,
        GameDescription, SettingsText;
        private LanguageSwitcher switcher;
        public Image FearImage;

        void Start()
        {

            int minutes = (int)(GameManager.TimeLeft / 60);
            Timer.text = (language == (int)Language.Arabic ? "ﻲﻘﺒﺘﻣ ﻦﻣﺯ " : "Time Left ") + minutes + ":" + (GameManager.TimeLeft - minutes * 60).ToString("00.0");
            switcher = GetComponent<LanguageSwitcher>();
            GameManager.Init(GetComponent<Camera>(), Jinn, Man, Wolf, ObjectsParent, FearImage);
            ParticleManager.Initilaize(ParticleParent);
            ChangeLanguage(language);
        }

        void Update()
        {
            GameManager.Update();
            UIUpdate();
        }

        private float updateTimerRate = 5;
        private float currentUIUpdateTimer = 0;
        private void UIUpdate()
        {
            if (!GameManager.GameActive)
                return;

            currentUIUpdateTimer -= Time.deltaTime;

            if (currentUIUpdateTimer <= 0)
            {
                int minutes = (int)(GameManager.TimeLeft / 60);
                //TimerUpdate
                Timer.text = (language == (int)Language.Arabic ? "ﻲﻘﺒﺘﻣ ﻦﻣﺯ " : "Time Left ") + minutes + ":" + (GameManager.TimeLeft - minutes * 60).ToString("00.0");
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

            switch (index)
            {
                case 0:
                    //ability0
                    ParticleManager.Emit(Jinn.transform.localPosition, ParticleType.ShadowBomb);
                    break;
                case 1:
                    //ability1
                    ParticleManager.Emit(Jinn.transform.localPosition, ParticleType.ShadowFire);
                    break;
                case 2:
                    //ability2
                    break;
                case 3:
                    //ability3
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
            Timer.text = (language == (int)Language.Arabic ? "ﻲﻘﺒﺘﻣ ﻦﻣﺯ " : "Time Left ") + minutes + ":" + (GameManager.TimeLeft - minutes * 60).ToString("00.0");
        }

        public void PauseGame(bool pause)
        {
            StartedGame = true;
            GameManager.GameActive = !pause;
            ChangeLanguage(language);
        }
    }

    public enum Language
    {
        Arabic = 0,
        English = 1
    }
}
