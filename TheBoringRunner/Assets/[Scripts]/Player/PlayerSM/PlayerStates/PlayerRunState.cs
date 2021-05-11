using BaseState;
using UnityEngine;

namespace Player.PlayerSM.PlayerStates
{
    public class PlayerRunState : IState
    {
        private PlayerSM _playerSM;
        private Vector3 _desiredPos;

        public PlayerRunState(PlayerSM playerSm)
        {
            _playerSM = playerSm;
        }

        public void Enter()
        {
            Debug.Log("Changed State - Run");
            _playerSM.PlayerController.PlayerAnimator.SetBool("isRunning", true);
            _playerSM.PlayerController.PlayerTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }

        public void Tick()
        {
            CheckFinalLine();
            MovePlayer();
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }

        private void CheckFinalLine()
        {
            if (_playerSM.PlayerController.IsFinalLine)
                _playerSM.ChangeState(_playerSM.FightState);
        }

        private void MovePlayer()
        {
            _playerSM.PlayerController.MovePlayer();
        }
    }
}