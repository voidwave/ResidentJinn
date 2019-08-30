//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ResidentJinn
{
    public class Unit : MonoBehaviour
    {
        public UnitType type;
        public float Health, MaxHealth;
        public float Speed, CurrentSpeed;
        //-100 = Sad , 0 = Bored , 100 = Happy
        [Range(-100, 100)]
        public float Mood;
        public float Fear, MaxFear;
        public Vector3 destination;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public BoxCollider collider;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            collider = GetComponent<BoxCollider>();

        }

        void Update()
        {
            if (Health <= 0)
                return;
            CurrentSpeed = Speed + (Fear > 50 ? 20 : 0);
            agent.speed = CurrentSpeed;

            //Behaviour
            if (type == UnitType.Jinn)
                JinnUpdate();
            else if (type == UnitType.Man)
                ManUpdate();
            else
                WolfUpdate();
        }
        public void NavGoto(Vector3 Pos)
        {
            Pos.y = transform.localPosition.y;
            agent.SetDestination(Pos);
        }
        private void WolfUpdate()
        {
            NavGoto(GameManager.Jinn.transform.localPosition);
        }

        private void ManUpdate()
        {
            NavGoto(GameManager.Jinn.transform.localPosition);
        }

        private void JinnUpdate()
        {
            if (Vector3.Distance(transform.localPosition, destination) > 0.25f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime * CurrentSpeed);
            }
        }
    }

    public enum UnitType
    {
        Jinn = 0,
        Man = 1,
        Wolf = 2,
    }
}