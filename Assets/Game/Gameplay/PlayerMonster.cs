using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Funzilla
{
    internal class PlayerMonster : MonoBehaviour
    {
        internal enum MonsterStates
        {
            Idle,
            Run,
            Attack,
            Win,
            Die
        }

        private float AttackTime = 0.7f;
        private float AttackRange = 4.0f;
        private Boss boss;
        [SerializeField] Health Health;
        private float attackTime;
        internal MonsterStates _state = MonsterStates.Idle;
        [SerializeField] MonsterCharacter MonsterCharacter;
        [SerializeField] private Image hpImage;
        [SerializeField] private ParticleSystem effAttack;
        [SerializeField] private Transform effPos;

        private Gameplay Gameplay;

        private void Start()
        {
            boss = FindObjectOfType<Boss>();
            Vector3 a = hpImage.gameObject.transform.position;
            Vector3 b = ArenaCamUI.Instance.hpmonstertarget.transform.position;
            hpImage.gameObject.transform.position = Vector3.MoveTowards(a, b, 30f);
            hpImage.gameObject.transform.rotation = ArenaCamUI.Instance.transform.rotation;
        }

        private void Update()
        {                      
            switch (_state)
            {
                case MonsterStates.Idle:
                    if (Input.GetMouseButton(0))
                    {
                        ChangeState(MonsterStates.Attack);
                    }
                    if (Health.currentMonsterHealth <= 0)
                    {
                        ChangeState(MonsterStates.Die);
                        DOVirtual.DelayedCall(1f, delegate
                        {
                            SceneManager.Instance.OpenScene(SceneID.Lose);
                        });
                    } 
                    break;
                case MonsterStates.Run:
                    //Vector3 a = transform.position;
                    //Vector3 b = boss.transform.position;
                    //transform.position = Vector3.MoveTowards(a, b, 0.01f);
                    //TryAttack();
                    break;
                case MonsterStates.Attack:
                    attackTime -= Time.deltaTime;
                    if (attackTime <= 0)
                    {
                        ChangeState(MonsterStates.Idle);
                    }
                    break;
                case MonsterStates.Win:
                    break;
                case MonsterStates.Die:
                    break;
            }
        }

        private void ChangeState(MonsterStates newstate)
        {
            if (newstate == _state) return;
            ExitCurrentStates();
            _state = newstate;
            EnterNewStates();
        }

        private void EnterNewStates()
        {
            switch (_state)
            {
                case MonsterStates.Idle:
                    if (boss._state == Boss.BossStates.Die)
                    {
                        ChangeState(MonsterStates.Win);
                    }
                    MonsterCharacter.AnimateIdle();
                    break;
                case MonsterStates.Run:
                    MonsterCharacter.AnimateRun();
                    break;
                case MonsterStates.Attack:
                    attackTime = AttackTime;
                    MonsterCharacter.AnimateAttack();
                    PlayEff();
                    boss.MinusHP();
                    break;
                case MonsterStates.Win:
                    MonsterCharacter.AnimateIdle();
                    DOVirtual.DelayedCall(2.5f, delegate
                    {
                        ArenaCamUI.Instance.winBG.gameObject.SetActive(true);
                        Gameplay.Instance.ChangeState(GameState.Win);
                    });

                    break;
                case MonsterStates.Die:
                    MonsterCharacter.AnimateDie();
                    break;
            }
        }

        private void ExitCurrentStates()
        {
            switch (_state)
            {
                case MonsterStates.Idle:
                    break;
                case MonsterStates.Run:
                    break;
                case MonsterStates.Attack:
                    break;
                case MonsterStates.Win:
                    break;
                case MonsterStates.Die:
                    break;
            }
        }

        private bool TryAttack()
        {
            if (Vector3.Distance(boss.transform.position, transform.position) < AttackRange)
            {
               ChangeState(MonsterStates.Attack);
               return true;
            }
            return false;
        }
        //internal void Spawn(Vector3 a, Quaternion b)
        //{
        //    Instantiate(this, a, b);
        //}

        internal void MinusHP()
        {
            if (boss._state == Boss.BossStates.Attack)
            {
                Health.ModifyMonsterHealth(-20);
            }
        }

        private void PlayEff()
        {
            var effatt = Instantiate(effAttack, effPos.position, effPos.rotation);
            effatt.transform.parent = this.transform;
            //DOVirtual.DelayedCall(0.3f, delegate
            //{
            //    Destroy(skill.gameObject);
            //});
        }

    }
}

