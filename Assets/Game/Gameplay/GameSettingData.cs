using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Funzilla
{
    [CreateAssetMenu(fileName = "GameSettingData", menuName = "Tools/GameSettingData")]
    internal class GameSettingData : ScriptableObject
    {
        private static GameSettingData _instance;
        [SerializeField] [Range(0, 5)] internal float startTaskRotateTime = 1;
        [SerializeField] [Range(0, 5)] internal float endTaskRotateTime = 1;
        [SerializeField] [Range(0, 5)] internal float rangeGetHit = 3;
        [SerializeField] [Range(0,10)] internal float moveSpeed = 8;
        public static GameSettingData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<GameSettingData>("GameSetting");

                return _instance;
            }

        }
    }
}
