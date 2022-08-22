using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    public class BossCharacter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        internal void AnimateIdle()
        {
            animator.SetTrigger("Idle");
        }
        internal void AnimateAttack()
        {
            animator.SetTrigger("Attack");           
        }

        internal void AnimateWin()
        {
            animator.SetTrigger("Idle");
        }
        internal void AnimateDie()
        {
            animator.SetTrigger("Die");
        }
    }
}
