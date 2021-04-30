using System;
using BaseState;
using Player.PlayerSM.PlayerStates;
using UnityEngine;

namespace Player.PlayerSM
{
    public class PlayerSM : StateMachineMB
    {
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }

        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _playerTransform;

        public Animator PlayerAnimator
        {
            get => _playerAnimator;
            set => _playerAnimator = value;
        }

        public Transform PlayerTransform
        {
            get => _playerTransform;
            set => _playerTransform = value;
        }

        private void Awake()
        {
            IdleState = new PlayerIdleState(this);
            RunState = new PlayerRunState(this);
        }

        private void Start()
        {
            ChangeState(IdleState);
        }
    }
}