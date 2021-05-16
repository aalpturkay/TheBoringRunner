using System.Collections;
using BaseState;
using DG.Tweening;
using UnityEngine;

namespace Player.PlayerSM.PlayerStates
{
    public class PlayerFightState : IState
    {
        private PlayerSM _playerSM;
        private bool _isTopCamera = true;
        private float _playerPunchDelay = 1;
        private float _bossPunchDelay = 3;

        public PlayerFightState(PlayerSM playerSM)
        {
            _playerSM = playerSM;
        }

        public void Enter()
        {
            Debug.Log("State - Fight State");
            ActivateBars();
            SwitchCamera();
            //_playerSM.StartCoroutine(TakeDamageIE(10, damageDelay: _bossPunchDelay));
            _playerSM.StartCoroutine(DecreaseTapBarIE(5, delay: .5f));
            _playerSM.StartCoroutine(WaitPlayerPosAndSetAnimsIE());
            _playerSM.StartCoroutine(ActivateFightTextIE());
            PlayerPunch(punchDelay: _playerPunchDelay);
            BossPunch(punchDelay: _bossPunchDelay);
            DeactiveManCounterUI();
        }

        public void Tick()
        {
            MovePlayerToBattleStage();
            FreezeBossRot();
            if (Input.GetMouseButtonDown(0))
            {
                _playerSM.TapBar.SetHealth(_playerSM.TapBar.GetHealth + 7);
            }

            DeadSequence();
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
            _playerSM.StopAllCoroutines();
        }

        private void DeactiveManCounterUI()
        {
            _playerSM.PlayerController.ManCountImage.gameObject.SetActive(false);
            _playerSM.PlayerController.ManCountText.gameObject.SetActive(false);
        }

        private void MovePlayerToBattleStage()
        {
            var playerPos = _playerSM.PlayerController.PlayerTransform.position;
            var playerRot = _playerSM.PlayerController.PlayerTransform.rotation;
            var bossPos = _playerSM.BossTransform.position;
            playerPos = Vector3.MoveTowards(playerPos, new Vector3(bossPos.x, playerPos.y, bossPos.z - 2.5f),
                Time.deltaTime * _playerSM.PlayerController.PlayerSpeed);
            playerPos.y = Mathf.Clamp(playerPos.y, 0, 0);
            _playerSM.PlayerController.PlayerTransform.position = playerPos;
            playerRot.y = Mathf.Clamp(playerRot.y, 0f, 0f);
            _playerSM.PlayerController.PlayerTransform.rotation = playerRot;
        }

        IEnumerator WaitPlayerPosAndSetAnimsIE()
        {
            yield return new WaitUntil(() => PlayerPosControl());

            SetFightAnims();
        }

        bool PlayerPosControl()
        {
            var playerPos = _playerSM.PlayerController.PlayerTransform.position;
            var bossPos = _playerSM.BossTransform.position;


            return playerPos.z == bossPos.z - 2.5f;
        }

        private void SetFightAnims()
        {
            _playerSM.PlayerController.PlayerAnimator.SetBool("isRunning", false);
            _playerSM.PlayerController.PlayerAnimator.SetBool("isFightIdle", true);
        }

        private void SwitchCamera()
        {
            if (_isTopCamera)
            {
                _playerSM.CinemachineAnimator.Play("FightCamState");
            }
            else
            {
                _playerSM.CinemachineAnimator.Play("TopCamState");
            }

            _isTopCamera = !_isTopCamera;
        }

        private void ActivateBars()
        {
            _playerSM.PlayerHealthBar.gameObject.SetActive(true);
            _playerSM.BossHealthBar.gameObject.SetActive(true);
            _playerSM.TapBar.gameObject.SetActive(true);
        }

        IEnumerator TakeDamageIE(int damage, float damageDelay)
        {
            var health = _playerSM.PlayerHealthBar.GetHealth;
            while (_playerSM.PlayerHealthBar.GetHealth > 0)
            {
                health -= damage;
                _playerSM.PlayerHealthBar.SetHealth(health);
                yield return new WaitForSeconds(damageDelay);
            }

            _playerSM.StopCoroutine("TakeDamageIE");
        }

        IEnumerator DecreaseTapBarIE(int val, float delay)
        {
            while (_playerSM.TapBar.GetHealth >= 0)
            {
                var tapBarVal = _playerSM.TapBar.GetHealth;
                tapBarVal -= tapBarVal == 0 ? 0 : val;
                _playerSM.TapBar.SetHealth(tapBarVal);
                yield return new WaitForSeconds(delay);
            }
        }

        IEnumerator ActivateFightTextIE()
        {
            for (int i = 0; i < 3; i++)
            {
                _playerSM.FightText.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
            }

            _playerSM.FightText.gameObject.SetActive(false);
        }

        private void PlayerPunch(float punchDelay)
        {
            DOVirtual.DelayedCall(punchDelay, () =>
            {
                if (_playerSM.BossHealthBar.GetHealth == 0)
                {
                    return;
                }

                if (_playerSM.TapBar.GetHealth >= 70)
                {
                    _playerSM.PlayerController.PlayerAnimator.SetTrigger("punch");
                }


                PlayerPunch(punchDelay: _playerPunchDelay);
            });
        }


        private void BossPunch(float punchDelay)
        {
            DOVirtual.DelayedCall(punchDelay, () =>
            {
                if (_playerSM.PlayerHealthBar.GetHealth == 0 || _playerSM.BossHealthBar.GetHealth == 0)
                {
                    return;
                }

                if (_playerSM.PlayerHealthBar.GetHealth > 0)
                {
                    Debug.Log("za");
                    _playerSM.BossAnimator.SetTrigger("punch");
                }

                BossPunch(punchDelay: _bossPunchDelay);
            });
        }


        private void FreezeBossRot()
        {
            var bossRot = _playerSM.BossTransform.rotation;
            bossRot.y = Mathf.Clamp(bossRot.y, 180, 180);
            _playerSM.BossTransform.rotation = bossRot;
        }

        private void DeadSequence()
        {
            if (_playerSM.BossHealthBar.GetHealth <= 0)
            {
                _playerSM.ChangeState(_playerSM.WinState);
            }

            if (_playerSM.PlayerHealthBar.GetHealth <= 0)
            {
                _playerSM.ChangeState(_playerSM.LoseState);
            }
        }
    }
}