using BaseState;
using UnityEngine;

namespace Player.PlayerSM.PlayerStates
{
    public class PlayerIdleState : IState
    {
        private PlayerSM _playerSM;


        public PlayerIdleState(PlayerSM playerSM)
        {
            _playerSM = playerSM;
        }

        public void Enter()
        {
            Debug.Log("Changed State - Idle");
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _playerSM.ChangeState(_playerSM.RunState);
            }
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }
    }
}