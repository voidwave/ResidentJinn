//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;

namespace ResidentJinn
{
    public class ColliderTimer : MonoBehaviour
    {
        public float CoolDown = 30;
        public float CurrentCD = 0;
        void Update()
        {
            if (CoolDown == -1)
                return;

            CurrentCD -= Time.deltaTime;
            if (CurrentCD <= 0)
                gameObject.SetActive(false);
        }
    }
}
