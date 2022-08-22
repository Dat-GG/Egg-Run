using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Funzilla
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 50;

        [SerializeField] private int maxMonsterHealth = 100;

        internal int currentBossHealth;

        internal int currentMonsterHealth;

        public event Action<float> OnHealthBossPctChanged = delegate { };

        public event Action<float> OnHealthMonsterPctChanged = delegate { };

        private void OnEnable()
        {
            currentBossHealth = maxHealth;
            currentMonsterHealth = maxMonsterHealth;
        }

        public void ModifyBossHealth(int amount)
        {
            currentBossHealth += amount;

            float currentHealthPct = (float)currentBossHealth / (float)maxHealth;
            OnHealthBossPctChanged(currentHealthPct);
        }

        public void ModifyMonsterHealth(int amount)
        {
            currentMonsterHealth += amount;

            float currentHealthPct = (float)currentMonsterHealth / (float)maxMonsterHealth;
            OnHealthMonsterPctChanged(currentHealthPct);
        }

        private void Update()
        {

        }
    }
}
