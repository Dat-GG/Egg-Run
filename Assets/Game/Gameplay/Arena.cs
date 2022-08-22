using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Funzilla
{
    public class Arena : MonoBehaviour
    {
        private Collider _collider;
        internal Collider Collider => _collider;

        private PlayerEgg PlayerEgg;
        [SerializeField] private PlayerMonster PlayerMonster;
        [SerializeField] private ParticleSystem smoke;
        public GameObject targetpos;
        [SerializeField] Transform Monster;

        //public GameObject hpBossTarget;

        //public GameObject hpMonsterTarget;

        public static Arena Instance;

        internal enum BossType
        {
            CharizardBoss,
            HuggyBoss,
            ShadowBoss
        }

        [SerializeField] private BossType bossType;

        internal BossType Type => bossType;

        [SerializeField] private GameObject[] listBossPic;


        private void Awake()
        {
            //SetPic(monsterType.ToString());
            Instance = this;
            ReferenceToComponent();
            
        }

        private void OnValidate()
        {
            SetPic(bossType.ToString());
        }

        private void ReferenceToComponent()
        {
            _collider = GetComponent<Collider>();
            PlayerEgg = GetComponent<PlayerEgg>();

        }
        private void OnTriggerEnter(Collider other)
        {
            var PlayerEgg = other.transform.gameObject.GetComponent<PlayerEgg>();
            PlayerEgg.gameObject.SetActive(false);
            DOVirtual.DelayedCall(0.15f, delegate
            {
                smoke.Play();
            });

            Transform playerMonster = Instantiate(Monster, targetpos.transform.position, Quaternion.identity);
            playerMonster.parent = transform;
            //PlayerMonster.Spawn(targetpos.transform.position, Quaternion.identity);

        }

        private void SetPic(string bossName)
        {
            foreach (var pic in listBossPic)
            {
                pic.SetActive(pic.name == bossName);
            }
        }
    }
}

