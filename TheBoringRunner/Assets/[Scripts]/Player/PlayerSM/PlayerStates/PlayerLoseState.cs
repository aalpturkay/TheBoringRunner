using BaseState;
using UnityEngine;

namespace Player.PlayerSM.PlayerStates
{
    public class PlayerLoseState : IState
    {
        private PlayerSM _playerSM;

        public PlayerLoseState(PlayerSM playerSM)
        {
            _playerSM = playerSM;
        }

        public void Enter()
        {
            _playerSM.ReloadLevelBut.gameObject.SetActive(true);
            _playerSM.ReloadLevelBut.onClick.AddListener(ReloadLevel);
            _playerSM.PlayerHealthBar.gameObject.SetActive(false);
            _playerSM.BossHealthBar.gameObject.SetActive(false);
            _playerSM.TapBar.gameObject.SetActive(false);
            _playerSM.LoseText.gameObject.SetActive(true);
            _playerSM.BossAnimator.SetTrigger("victory");
            _playerSM.PlayerController.PlayerAnimator.SetTrigger("defeated");
        }

        void ReloadLevel()
        {
            LevelManager.Instance.ReloadLevel();
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }
    }
}