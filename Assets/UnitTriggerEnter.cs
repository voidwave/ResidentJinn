//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;

namespace ResidentJinn
{
    public class UnitTriggerEnter : MonoBehaviour
    {

        void OnTriggerEnter(Collider collider)
        {
            if (!collider.CompareTag("Unit"))
                return;

            Unit unit = collider.GetComponent<Unit>();
            if (unit.type != UnitType.Man)
                return;

            Debug.Log("Human Recited Dua");

            if (transform.CompareTag("BathRoomEntrance"))
            {
                //Only do this if the human isn't sinful
                if (unit.Sin > 25)
                    return;

                //Human Entering Bathroom
                if (unit.CurrentObject == (int)ObjectType.Toilet)
                {
                    unit.Sin -= 5;
                    if (unit.Sin < 0)
                        unit.Sin = 0;

                    unit.Stunned = 4;
                    AudioManager.Play(transform.position, AudioClipName.EnterDua);
                    GameManager.HouseObjects[(int)ObjectType.Toilet].roomColliderTimer.CurrentCD = GameManager.HouseObjects[(int)ObjectType.Toilet].roomColliderTimer.CoolDown;
                    GameManager.HouseObjects[(int)ObjectType.Toilet].roomColliderTimer.gameObject.SetActive(true);
                }
                //Human Exiting Bathroom
                else if (unit.ScaredTimer <= 0)
                {
                    unit.Sin -= 5;
                    if (unit.Sin < 0)
                        unit.Sin = 0;

                    unit.Stunned = 1;
                    AudioManager.Play(transform.position, AudioClipName.ExitDua);

                }
            }

        }
    }
}
