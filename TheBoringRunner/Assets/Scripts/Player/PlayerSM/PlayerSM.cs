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