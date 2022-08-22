using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
    public class HPController : MonoBehaviour
    {
        [SerializeField] private Image foregroundImage;
        [SerializeField] private float updateSpeedSeconds = 0.5f;

        private void Awake()
        {
            GetComponentInParent<Health>().OnHealthBossPctChanged += HandleHealthBossChanged;

            GetComponentInParent<Health>().OnHealthMonsterPctChanged += HandleHealthMonsterChanged;
        }

        private void HandleHealthBossChanged(float pct)
        {
            StartCoroutine(ChangeToPctBoss(pct));
        }

        private void HandleHealthMonsterChanged(float pct)
        {
            StartCoroutine(ChangeToPctMonster(pct));
        }

        private IEnumerator ChangeToPctBoss(float pct)
        {
            float preChangePct = foregroundImage.fillAmount;
            float elapsed = 0f;

            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                yield return null;
            }

            foregroundImage.fillAmount = pct;
        }

        private IEnumerator ChangeToPctMonster(float pct)
        {
            float preChangePct = foregroundImage.fillAmount;
            float elapsed = 0f;

            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                yield return null;
            }

            foregroundImage.fillAmount = pct;
        }
    }
}


