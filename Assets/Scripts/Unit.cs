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
        public float Fear;
        public float UsingObject = 0;
        public int CurrentObject, LastObjectUsed = -1;
        public bool Walking = false;
        public float Stunned = 0;

        public float[] AbilityCurrentCD = { 0, 0, 0, 0 };
        public float[] AbilityCoolDown = { 10, 10, 15, 25 };
        public Vector3 destination, scarePosition;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public BoxCollider collider;
        [HideInInspector] public CharacterController controller;
        [HideInInspector] public ParticleSystem particle;
        [HideInInspector] public Animator anim;

        private Vector3 InitialPosition;
        void Start()
        {

            InitialPosition = transform.localPosition;
            destination = InitialPosition;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
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
        public void NavGoto(Vector3 Pos, float stoppingDistance = 1)
        {
            Pos.y = transform.localPosition.y;
            destination = Pos;
            agent.stoppingDistance = stoppingDistance;
            agent.SetDestination(Pos);
        }
        private float BiteCoolDown = 1;
        private void WolfUpdate()
        {
            if (UsingObject > 0)
                UsingObject -= Time.deltaTime;
            if (BiteCoolDown > 0)
                BiteCoolDown -= Time.deltaTime;
            if (Stunned > 0)
            {
                Stunned -= Time.deltaTime;
                return;
            }
            //Can See
            if (!Boot.JinnDimension)
            {
                float distance = Vector3.Distance(GameManager.Jinn.transform.localPosition, transform.localPosition);
                //Chase
                if (distance < 20)
                    NavGoto(GameManager.Jinn.transform.localPosition, 1);
                else
                {
                    if (UsingObject > 0)
                        return;

                    if (destination != GameManager.Man.transform.localPosition)
                        NavGoto(GameManager.Man.transform.localPosition, 8);
                }
                //Bite
                if (distance < 2 && BiteCoolDown <= 0)
                {
                    //TakeDamage
                    ParticleManager.Emit(GameManager.Jinn.transform.localPosition, ParticleType.TakeDamage);
                    if (Vector3.Distance(transform.localPosition, GameManager.Jinn.transform.localPosition) < 2)
                        GameManager.Jinn.Power -= 15;

                    if (GameManager.Jinn.Power < 0)
                        GameManager.GameOver(false, 1);
                    BiteCoolDown = 2;
                }
                UsingObject = 5;
            }
            else
            {
                if (UsingObject > 0)
                    return;

                if (destination != GameManager.Man.transform.localPosition)
                    NavGoto(GameManager.Man.transform.localPosition, 8);

            }

        }

        private void ManUpdate()
        {
            //NavGoto(GameManager.Jinn.transform.localPosition);
            GameManager.FearImage.fillAmount = Fear / 100;
            //Jinn Wins
            if (Fear >= 100)
            {
                GameManager.GameOver(true, 0);
                return;
            }

            //Stats Update
            StatsUpdate();

            if (Stunned > 0)
            {
                anim.SetFloat("Stunned", 1);
                agent.isStopped = true;
                return;
            }
            else
            {
                anim.SetFloat("Stunned", 0);
                agent.isStopped = false;
            }

            if (ScaredTimer > 0)
            {
                UsingObject = 0;
                CurrentObject = -1;
                NavGoto(Vector3.Normalize(transform.localPosition - scarePosition) * 10);
                return;
            }
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
                            if (i != LastObjectUsed)
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
                LastObjectUsed = CurrentObject;
                CurrentObject = -1;
                Walking = false;
            }

        }

        private void StatsUpdate()
        {
            Stunned -= Time.deltaTime;
            Fear -= Time.deltaTime * 0.1f;
            Mood -= Time.deltaTime * 0.5f;
            if (Stunned < 0)
                Stunned = 0;
            if (Fear < 0)
                Fear = 0;
            if (Mood < -100)
                Mood = -100;

            if (ScaredTimer > 0)
            {
                anim.SetFloat("Fear", 1);
                ScaredTimer -= Time.deltaTime;// / (Fear + 1);
            }
            else
            {
                anim.SetFloat("Fear", 0);
            }
        }

        private void JinnUpdate()
        {
            if (Power <= 0)
                GameManager.GameOver(false, 2);

            if (Boot.JinnDimension)
                Power -= Time.deltaTime;
            else
                Power += Time.deltaTime * 0.1f;
            if (Power < 0)
                Power = 0;
            if (Power > 25)
                Power = 25;

            for (int i = 0; i < AbilityCurrentCD.Length; i++)
                if (AbilityCurrentCD[i] > 0)
                    AbilityCurrentCD[i] -= Time.deltaTime;

            if (Stunned > 0)
            {
                //Animation JinnStunned
                if (Boot.UseAnimations)
                {
                    anim.SetFloat("Stunned", 1);
                }
                Stunned -= Time.deltaTime;
                return;
            }

            if (Vector3.Distance(transform.localPosition, destination) > 0.25f)
            {
                //Animation JinnWalk
                if (Boot.UseAnimations)
                {
                    anim.SetFloat("Walk", 1);
                }
                Vector3 dir = destination - transform.localPosition;
                dir.y = 0;
                controller.Move(dir.normalized * Time.unscaledDeltaTime * CurrentSpeed);
                //transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime * CurrentSpeed);
            }
            else
            {
                anim.SetFloat("Walk", 0);
            }
        }

        public void Scared(float FearAmount, Vector3 Position)
        {
            //Animation HumanAfraid
            if (Boot.UseAnimations)
            {
                anim.SetFloat("Fear", 1);
            }

            Fear += FearAmount;
            Mood -= 25;
            //Sin -= 5;
            ScaredTimer += FearAmount;
            if (ScaredTimer > 10)
                ScaredTimer = 10;
            scarePosition = Position;
        }

        public void ResetValues()
        {

            audioSource.Stop();
            transform.localPosition = InitialPosition;
            agent.nextPosition = InitialPosition;
            Walking = false;

            CurrentObject = -1;
            LastObjectUsed = -1;
            Fear = 0;
            Mood = 20;
            Sin = 10;
            ScaredTimer = 0;
            Power = 10;
            UsingObject = 0;
            destination = InitialPosition;

            if (type == UnitType.Jinn)
            {
                for (int i = 0; i < AbilityCurrentCD.Length; i++)
                    if (AbilityCurrentCD[i] > 0)
                        AbilityCurrentCD[i] = 0;
            }
            else
            {
                Stunned = 1f;

                agent.Warp(InitialPosition);
                agent.isStopped = false;
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