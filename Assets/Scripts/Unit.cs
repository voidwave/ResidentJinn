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
        public float Sin, Power = 10;
        public float ScaredTimer = 0;
        public float Speed, CurrentSpeed;
        //-100 = Sad , 0 = Bored , 100 = Happy
        [Range(-100, 100)]
        public float Mood;
        [Range(0, 100)]
        public float Fear, MaxFear;
        public float UsingObject = 0;
        public int CurrentObject = -1;
        public bool Walking = false;
        public Vector3 destination;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public BoxCollider collider;
        [HideInInspector] public CharacterController controller;
        [HideInInspector] public ParticleSystem particle;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            particle = GetComponent<ParticleSystem>();
            if (type == UnitType.Jinn)
                controller = GetComponent<CharacterController>();
            else
                collider = GetComponent<BoxCollider>();

        }

        void Update()
        {
            if (!GameManager.GameActive)
                return;

            CurrentSpeed = Speed + (Fear > 50 ? 10 : 0);
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
            destination = Pos;
            agent.SetDestination(Pos);
        }
        private void WolfUpdate()
        {
            //NavGoto(GameManager.Jinn.transform.localPosition);
        }

        private void ManUpdate()
        {
            //NavGoto(GameManager.Jinn.transform.localPosition);
            GameManager.FearImage.fillAmount = Fear / MaxFear;
            //Jinn Wins
            if (Fear >= MaxFear)
            {
                GameManager.GameOver(true);
                return;
            }

            if (ScaredTimer > 0)
                ScaredTimer -= Time.deltaTime / (Fear + 1);

            //Stats Update
            StatsUpdate();
            if (UsingObject > 0)
            {
                UsingObject -= Time.deltaTime;
                return;
            }

            //not doing anything
            if (CurrentObject == -1)
            {
                //Find something to do
                List<int> possibilities = new List<int>();
                for (int i = 0; i < GameManager.HouseObjects.Count; i++)
                    for (int j = 0; j < GameManager.HouseObjects[i].CanUse.Count; j++)
                        if (GameManager.HouseObjects[i].CanUse[j] == UnitType.Man)
                            if (GameManager.HouseObjects[i].CurrentCD <= 0)
                                if (GameManager.HouseObjects[i].ConditionFearMax >= Fear && GameManager.HouseObjects[i].ConditionFearMin <= Fear
                                   && GameManager.HouseObjects[i].ConditionMoodMax >= Mood && GameManager.HouseObjects[i].ConditionMoodMin <= Mood)
                                    possibilities.Add(i);


                //pick something random to do
                if (possibilities.Count > 0)
                {
                    CurrentObject = possibilities[UnityEngine.Random.Range(0, possibilities.Count)];
                    NavGoto(GameManager.HouseObjects[CurrentObject].transform.localPosition);
                    Walking = true;
                }
            }
            //reached destination
            else if (Vector3.Distance(destination, transform.localPosition) <= 3)
            {
                destination = transform.localPosition;
                GameManager.HouseObjects[CurrentObject].UseObject(this);
                CurrentObject = -1;
                Walking = false;
            }

        }

        private void StatsUpdate()
        {
            Fear -= Time.deltaTime * 0.1f;
            Mood -= Time.deltaTime * 0.5f;
            if (Fear < 0)
                Fear = 0;
            if (Mood < -100)
                Mood = -100;
        }

        private void JinnUpdate()
        {
            if (Vector3.Distance(transform.localPosition, destination) > 0.25f)
            {
                Vector3 dir = destination - transform.localPosition;
                dir.y = 0;
                controller.Move(dir.normalized * Time.deltaTime * CurrentSpeed);
                //transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime * CurrentSpeed);
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