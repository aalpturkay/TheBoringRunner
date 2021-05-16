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
            _playerSM.NextLevelBut.gameObject.SetActive(true);
            _playerSM.NextLevelBut.onClick.AddListener(NextLevel);
            _playerSM.PlayerHealthBar.gameObject.SetActive(false);
            _playerSM.BossHealthBar.gameObject.SetActive(false);
            _playerSM.TapBar.gameObject.SetActive(false);
            _playerSM.WinText.gameObject.SetActive(true);
            Debug.Log("State - WinState");
            _playerSM.PlayerController.PlayerAnimator.SetTrigger("victory");
            _playerSM.ConfettiParticle.Play();

            //_playerSM.PlayerController.PlayerTransform.GetComponent<Collider>().enabled = false;
            //_playerSM.BossTransform.GetComponent<Rigidbody>().AddForce(Vector3.up * 5);
        }

        void NextLevel()
        {
            LevelManager.Instance.LoadNextLevel();
        }
        public void Tick()
        {
            SpinBoss();
            
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }

        void Fizik(bool isOpen)
        {
            Rigidbody[] rb = _playerSM.BossTransform.GetComponentsInChildren<Rigidbody>();
            foreach (var childRb in rb)
            {
                childRb.isKinematic = isOpen;
            }
        }

        void Colli(bool isOpen)
        {
            Collider[] cl = _playerSM.BossTransform.GetComponentsInChildren<Collider>();
            foreach (var childCol in cl)
            {
                childCol.enabled = isOpen;
            }
        }

        private void SpinBoss()
        {
            //_playerSM.BossTransform.GetComponent<Collider>().isTrigger = false;
            //_playerSM.BossTransform.GetComponent<Rigidbody>().useGravity = true;
            //_playerSM.BossAnimator.enabled = false;
            //_playerSM.BossTransform.GetComponent<Rigidbody>().isKinematic = true;
            _playerSM.BossAnimator.enabled = false;
            Fizik(false);
            Colli(true);


            //_playerSM.BossTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 10));
            timer += Time.deltaTime;
            if (timer > 3)
            {
                //_playerSM.BossTransform.gameObject.SetActive(false);
            }
        }
    }
}