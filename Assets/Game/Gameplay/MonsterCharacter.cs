using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Funzilla
{
    public class MonsterCharacter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        internal void AnimateIdle()
        {
            animator.SetTrigger("Idle");
        }
        internal void AnimateRun()
        {
            animator.SetTrigger("Fly");
        }
        internal void AnimateAttack()
        {
            animator.SetTrigger("Attack");
            //DOVirtual.DelayedCall(1.5f, delegate
            //{
            //    animator.SetTrigger("Attack1");
            //    DOVirtual.DelayedCall(1.5f, delegate
            //    {
            //        animator.SetTrigger("Attack2");
            //    });
            //});
            
            
        }

        internal void AnimateWin()
        {
            animator.SetTrigger("Win");
        }
        internal void AnimateDie()
        {
            animator.SetTrigger("Die");
        }
    }
}
