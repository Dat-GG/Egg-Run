using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Funzilla
{
    internal enum GameState
    {
        None, Initializing, Initialized, Play, End, Win
    }
    internal class Gameplay : Scene
    {
        public static Gameplay Instance;
        [SerializeField] private Level currentlevel;
        [SerializeField] private PlayerModel playerModel;
        internal Level CurrentLevel => currentlevel;
        internal PlayerModel PlayerModel => playerModel;
        private GameState _gameState = GameState.None;
        internal GameState GameState => _gameState;

        private void Awake()
        {
            _gameState = GameState.Initializing;
            Instance = this;
            SceneManager.Instance.HideLoading();
            SceneManager.Instance.HideSplash();
            GenerateLevel();
            _gameState = GameState.Initialized;
        }

        private void GenerateLevel()
        {
            
        }

        private void Start()
        {
            playerModel.ChangeState(CharacterState.Idle);
            //SoundManager.Instance.PlayMusic("BGM", true, 0);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_gameState == GameState.Initialized)
                {
                    _gameState = GameState.Play;
                    playerModel.ChangeState(CharacterState.Walking);
                }
            }
        }

        internal void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.None:
                    _gameState = GameState.None;
                    break;
                case GameState.Initializing:
                    _gameState = GameState.Initializing;
                    break;
                case GameState.Initialized:
                    _gameState = GameState.Initialized;
                    break;
                case GameState.Play:
                    _gameState = GameState.Play;
                    break;
                case GameState.End:
                    _gameState = GameState.End;
                    playerModel.Player.SplineFollower.follow = false;
                    DOVirtual.DelayedCall(1f, delegate
                    {
                        SceneManager.Instance.OpenScene(SceneID.Lose);
                    });
                    break;
                case GameState.Win:
                    _gameState = GameState.Win;
                    DOVirtual.DelayedCall(2f, delegate
                    {
                        SceneManager.Instance.ReloadScenes();
                    });
                    break;
            }
        }
    }
}
