//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;

namespace ResidentJinn
{
    public class JinnTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Building"))
                ParticleManager.Emit(transform.position, ParticleType.ShadowBomb);

        }
    }
}
