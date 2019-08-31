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
        private float CurrentCD = 0;

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
            if (CurrentCD > 0)
                CurrentCD -= Time.deltaTime;
        }
        public void UseObject()
        {
            CurrentCD = CoolDown;
        }
    }
}
