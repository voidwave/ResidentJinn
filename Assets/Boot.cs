//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;
namespace ResidentJinn
{
    public class Boot : MonoBehaviour
    {
        public Unit Jinn, Man, Wolf;

        void Start()
        {
            GameManager.Init(GetComponent<Camera>(), Jinn, Man, Wolf);
        }

        void Update()
        {
            GameManager.Update();
        }
    }
}
