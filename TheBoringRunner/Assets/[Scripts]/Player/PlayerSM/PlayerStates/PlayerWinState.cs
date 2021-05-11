using BaseState;
using UnityEngine;

namespace Player.PlayerSM.PlayerStates
{
    public class PlayerWinState : IState
    {
        private PlayerSM _playerSM;
        private float timer;

        public PlayerWinState(PlayerSM playerSM)
        {
            _playerSM = playerSM;
        }

        public void Enter()
        {
            _playerSM.PlayerHealthBar.gameObject.SetActive(false);
            _playerSM.BossHealthBar.gameObject.SetActive(false);
            _playerSM.TapBar.gameObject.SetActive(false);
            _playerSM.WinText.gameObject.SetActive(true);
            Debug.Log("State - WinState");
            _playerSM.PlayerController.PlayerAnimator.SetTrigger("victory");
        }

        public void Tick()
        {
            SpinBoss();
            if (Input.GetMouseButtonDown(0))
            {
                LevelManager.Instance.LoadNextLevel();
                //_playerSM.ChangeState(_playerSM.IdleState);
            }
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }

        private void SpinBoss()
        {
            _playerSM.BossTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1000));
            timer += Time.deltaTime;
            if (timer > 2)
                _playerSM.BossTransform.gameObject.SetActive(false);
        }
    }
}