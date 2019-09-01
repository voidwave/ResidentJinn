//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;
using System.Collections.Generic;
namespace ResidentJinn
{
    public class HouseObject : MonoBehaviour
    {
        public List<UnitType> CanUse;
        public float CoolDown, UseTime;
        public float CurrentCD = 0;

        [Range(-100, 100)]
        public float ConditionMoodMin, ConditionMoodMax;
        [Range(0, 100)]
        public float ConditionFearMin, ConditionFearMax;
        public BoxCollider roomCollider;

        //change in mood after using this object
        public float MoodDelta, FearDelta;

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
}
