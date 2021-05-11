using System;
using BaseState;
using DG.Tweening;
using Player.PlayerSM.PlayerStates;
using TMPro;
using UnityEngine;

namespace Player.PlayerSM
{
    public class PlayerSM : StateMachineMB
    {
        [SerializeField] private Transform bossTransform;
        [SerializeField] private ParticleSystem bossPunchParticle;
        [SerializeField] private Animator cinemachineAnimator;
        [SerializeField] private HealthBar playerHealthBar;

        [SerializeField] private PlayerController _playerController;
        
        [SerializeField] private HealthBar bossHealthBar;
        [SerializeField] private HealthBar tapBar;
        [SerializeField] private Animator bossAnimator;
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private TextMeshProUGUI fightText;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private TextMeshProUGUI loseText;
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerFightState FightState { get; private set; }
        public PlayerWinState WinState { get; private set; }

        public PlayerLoseState LoseState { get; private set; }

        public Transform BossTransform => bossTransform;

        public Animator BossAnimator => bossAnimator;

        public Animator CinemachineAnimator => cinemachineAnimator;

        public HealthBar PlayerHealthBar => playerHealthBar;

        public PlayerController PlayerController => _playerController;
        
        public HealthBar BossHealthBar => bossHealthBar;

        public HealthBar TapBar => tapBar;

        public TextMeshProUGUI StartText => startText;

        public TextMeshProUGUI FightText => fightText;

        public TextMeshProUGUI WinText => winText;

        public TextMeshProUGUI LoseText => loseText;

        public ParticleSystem BossPunchParticle => bossPunchParticle;

        private void Awake()
        {
            IdleState = new PlayerIdleState(this);
            RunState = new PlayerRunState(this);
            FightState = new PlayerFightState(this);
            WinState = new PlayerWinState(this);
            LoseState = new PlayerLoseState(this);
        }

        private void Start()
        {
            ChangeState(IdleState);
        }

        public void PlayPunchParticle()
        {
            bossPunchParticle.Play();
            bossAnimator.SetTrigger("hit");
            DecreaseBossHealth(10);
        }
        
        private void DecreaseBossHealth(int damage)
        {
            var currentHealth = bossHealthBar.GetHealth;
            bossHealthBar.SetHealth(currentHealth - damage);
        }

        
    }
}