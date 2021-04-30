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
            PlayerController.Instance.PlayerTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            PlayerController.Instance.PlayerAnimator.SetBool("isRunning", true);
        }

        public void Tick()
        {
            MovePlayer();
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }

        private void MovePlayer()
        {
            float speed = PlayerController.Instance.PlayerSpeed;
            var playerPos = PlayerController.Instance.PlayerTransform.position;
            playerPos.z = playerPos.z + speed * Time.fixedDeltaTime;
            PlayerController.Instance.PlayerTransform.position = playerPos;
        }
    }
}