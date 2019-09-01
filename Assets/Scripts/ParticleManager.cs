//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;
using System.Collections.Generic;

namespace ResidentJinn
{
    public static class ParticleManager
    {

        public static Transform ParticleParent;
        public static List<Transform> ParticleTransforms;

        public static void Initilaize(Transform particleParent)
        {

            ParticleParent = particleParent;
            ParticleTransforms = new List<Transform>();
            for (int i = 0; i < ParticleParent.childCount; i++)
                ParticleTransforms.Add(ParticleParent.GetChild(i));
        }
        public static void Emit(Vector3 position, ParticleType type)
        {
            ParticleTransforms[(int)type].localPosition = position;
            ParticleTransforms[(int)type].GetComponent<ParticleSystem>().Play();
        }
    }

    public enum ParticleType
    {
        //to Scare human
        ShadowBomb = 0,
        //to distract wolf
        ShadowFire = 1,
        ShadowPower = 2,
        TakeDamage = 3
    }
}
