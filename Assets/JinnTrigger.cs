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
            {
                ParticleManager.Emit(transform.position, ParticleType.ShadowBomb);
                if (Vector3.Distance(GameManager.Jinn.transform.localPosition, GameManager.Man.transform.localPosition) < 10)
                {
                    Debug.Log("Scare Human");
                    //GameManager.Jinn.AbilityCurrentCD[0] = GameManager.Jinn.AbilityCoolDown[0];
                    GameManager.Man.Scared(1, GameManager.Jinn.transform.localPosition);
                }
            }

            if (collider.CompareTag("RoomCollider"))
            {
                Debug.Log("Hit Collider");
                GameManager.Jinn.Stunned = 1;
                ParticleManager.Emit(transform.position, ParticleType.ShadowBomb);
                GameManager.Jinn.transform.localPosition = new Vector3(0, 0.5f, 0);
            }

        }
        void OnTriggerStay(Collider collider)
        {
            if (collider.CompareTag("PowerUp"))
            {
                Debug.Log("PowerUp");
                GameManager.Jinn.Power += Time.deltaTime;
                ParticleManager.Emit(transform.position, ParticleType.ShadowBomb);
            }
        }
    }
}
