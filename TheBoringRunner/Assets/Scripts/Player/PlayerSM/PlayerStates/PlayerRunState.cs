using BaseState;
using UnityEngine;

namespace Player.PlayerSM.PlayerStates
{
    public class PlayerRunState : IState
    {
        private PlayerSM _playerSM;

        public PlayerRunState(PlayerSM playerSm)
        {
            _playerSM = playerSm;
        }

        public void Enter()
        {
            Debug.Log("Changed State - Run");
            _playerSM.PlayerTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _playerSM.PlayerAnimator.SetBool("isRunning", true);
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
            MovePlayer();
        }

        public void Exit()
        {
        }

        private void MovePlayer()
        {
            float speed = 5f;
            var playerPos = _playerSM.PlayerTransform.position;
            playerPos.z = playerPos.z + speed * Time.fixedDeltaTime;
            _playerSM.PlayerTransform.position = playerPos;
        }
    }
}