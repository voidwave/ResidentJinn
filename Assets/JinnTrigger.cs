//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;

namespace ResidentJinn
{
    public class JinnTrigger : MonoBehaviour
    {

        void OnTriggerEnter(Collider collider)
        {
            if (!Boot.JinnDimension)
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



        }
        void OnTriggerStay(Collider collider)
        {
            if (collider.CompareTag("PowerUp"))
            {
                Debug.Log("PowerUp");
                GameManager.Jinn.Power += Boot.JinnDimension ? Time.deltaTime * 0.5f : Time.deltaTime;
                if (GameManager.Jinn.Power > 100)
                    GameManager.Jinn.Power = 100;
                ParticleManager.Emit(transform.position, ParticleType.ShadowPower);
            }

            if (collider.CompareTag("RoomCollider"))
            {
                // Debug.Log("Hit Collider");
                //GameManager.Jinn.Stunned = 1;
                ParticleManager.Emit(transform.position, ParticleType.TakeDamage);
                //GameManager.Jinn.transform.localPosition = new Vector3(0, 0.5f, 0);
                GameManager.Jinn.Power -= Time.unscaledDeltaTime * 2;

                if (GameManager.Jinn.Power < 0)
                    GameManager.Jinn.Power = 0;
            }
        }
    }
}
