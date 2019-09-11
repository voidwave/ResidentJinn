//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;
using System.Collections.Generic;
namespace ResidentJinn
{
    public class HouseObject : MonoBehaviour
    {
        public List<UnitType> CanUse;
        public ObjectType type;
        public float CoolDown, UseTime;
        public float CurrentCD = 0;

        [Range(-100, 100)]
        public float ConditionMoodMin, ConditionMoodMax;
        [Range(0, 100)]
        public float ConditionFearMin, ConditionFearMax;
        public ColliderTimer roomColliderTimer;

        //change in mood after using this object
        public float MoodDelta, FearDelta;

        void Start()
        {
            type = (ObjectType)transform.GetSiblingIndex();
        }
        // Update is called once per frame
        void Update()
        {
            if (!GameManager.GameActive)
                return;

            if (CurrentCD > 0)
                CurrentCD -= Time.deltaTime;
        }
        public void UseObject(Unit user)
        {
            //if this object reduces fear
            //then turn on its special Ability if the Human
            //is afraid

            switch (type)
            {
                //Living Room
                case ObjectType.Sofa:
                    if (user.Sin < 45 && user.Fear > 25)
                    {
                        Debug.Log("Human turned on The Quran Channel");
                        user.Sin -= 25;
                        if (user.Sin < 0)
                            user.Sin = 0;
                        for(int i=0;i<AudioManager.audioSources.Count;i++)
                            AudioManager.audioSources[i].Stop();
                        AudioManager.Play(transform.localPosition, AudioClipName.Quran);
                        roomColliderTimer.CurrentCD = roomColliderTimer.CoolDown;
                        roomColliderTimer.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Human turned on The RealityTV Channel");
                        user.Sin += 15;
                        if (user.Sin > 100)
                            user.Sin = 100;

                        AudioManager.Play(transform.localPosition, AudioClipName.Reality, 1);
                        //roomColliderTimer.CurrentCD = roomColliderTimer.CoolDown;
                        roomColliderTimer.gameObject.SetActive(false);
                    }
                    break;

                //Kitchen
                case ObjectType.Cabinet:
                    Debug.Log("Human spread salt all over the kitchen");
                    roomColliderTimer.CurrentCD = roomColliderTimer.CoolDown;
                    roomColliderTimer.gameObject.SetActive(true);
                    break;


            }

            CurrentCD = CoolDown;
            user.UsingObject = UseTime;
            user.Mood += MoodDelta;
            user.Fear += FearDelta;

            if (user.Mood > 100)
                user.Mood = 100;
            if (user.Mood < -100)
                user.Mood = -100;

            if (user.Fear > 100)
                user.Fear = 100;
            if (user.Fear < 0)
                user.Fear = 0;

        }
    }

    public enum ObjectType
    {
        Exit,
        Fridge,
        Cabinet,
        Sofa,
        Toilet,
        Bed,
        Desk,
        Table,
        KitchenTable
    }
}
