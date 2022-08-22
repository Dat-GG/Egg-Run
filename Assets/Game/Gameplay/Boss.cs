using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
    internal class Boss : MonoBehaviour
    {
        internal enum BossStates
        {
            Init,
            Idle,
            Attack,
            Win,
            Die
        }

        private float AttackTime = 1f;
        private float AttackRange = 4.0f;
        private float attackTime;

        private PlayerMonster playerMonster;

        [SerializeField] Health Health;

        [SerializeField] private ParticleSystem skill;

        [SerializeField] private Transform skillpos;

        internal BossStates _state = BossStates.Init;
        [SerializeField] BossCharacter BossCharacter;
        [SerializeField] private Image hpImage;

        private void Awake()
        {
            
        }
        private void Update()
        {
            switch (_state)
            {
                case BossStates.Init:
                    if (playerMonster = FindObjectOfType<PlayerMonster>())
                    {
                        ChangeState(BossStates.Idle);
                    }
                    break;
                case BossStates.Idle:
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        ChangeState(BossStates.Attack);
                    });                   
                    break;
                case BossStates.Attack:
                    attackTime -= Time.deltaTime;
                    if (attackTime <= 0)
                    {
                        ChangeState(BossStates.Idle);
                    }
                    if (playerMonster._state == PlayerMonster.MonsterStates.Die)
                    {
                        ChangeState(BossStates.Win);
                        Gameplay.Instance.ChangeState(GameState.End);
                    }
                    break;
                case BossStates.Die:
                    break;
                case BossStates.Win:
                    break;
            }
        }

        private void EnterNewStates()
        {
            switch (_state)
            {
                case BossStates.Init:
                    BossCharacter.AnimateIdle();
                    break;
                case BossStates.Idle:
                    hpImage.gameObject.SetActive(true);

                    if (Health.currentBossHealth <= 0)
                    {
                        ChangeState(BossStates.Die);
                    }
                    Vector3 a = hpImage.gameObject.transform.position;
                    Vector3 b = ArenaCamUI.Instance.hpbosstarget.transform.position;
                    hpImage.gameObject.transform.position = Vector3.MoveTowards(a, b, 30f);
                    hpImage.gameObject.transform.rotation = ArenaCamUI.Instance.transform.localRotation;
                    BossCharacter.AnimateIdle();
                    break;
                case BossStates.Attack:
                    BossCharacter.AnimateAttack();
                    attackTime = AttackTime;
                    skill.gameObject.SetActive(true);
                    PlaySKill();
                    //skill.Play();
                    playerMonster.MinusHP();
                    break;
                case BossStates.Die:
                    BossCharacter.AnimateDie();
                    break;
                case BossStates.Win:
                    BossCharacter.AnimateWin();
                    break;
            }
        }

        private void ExitCurrentStates()
        {
            switch (_state)
            {
                case BossStates.Init:
                    break;
                case BossStates.Idle:
                    break;
                case BossStates.Attack:
                    break;
                case BossStates.Die:
                    break;
                case BossStates.Win:
                    break;
            }
        }
        private void ChangeState(BossStates newstate)
        {
            if (newstate == _state) return;
            ExitCurrentStates();
            _state = newstate;
            EnterNewStates();
        }

        private bool TryAttack()
        {
            if (Vector3.Distance(playerMonster.transform.position, transform.position) < AttackRange)
            {
                ChangeState(BossStates.Attack);
                return true;
            }
            return false;
        }

        internal void MinusHP()
        {
            if (playerMonster._state == PlayerMonster.MonsterStates.Attack)
            {
                Health.ModifyBossHealth(-10);
            }
        }
        private void PlaySKill()
        {
            var playskill = Instantiate(skill, skillpos.position, skillpos.rotation);
            playskill.transform.parent = this.transform;
            //DOVirtual.DelayedCall(0.3f, delegate
            //{
            //    Destroy(skill.gameObject);
            //});
        }
    }
}
