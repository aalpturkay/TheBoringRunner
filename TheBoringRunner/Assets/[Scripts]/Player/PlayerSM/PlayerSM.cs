using System;
using BaseState;
using DG.Tweening;
using Player.PlayerSM.PlayerStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.PlayerSM
{
    public class PlayerSM : StateMachineMB
    {
        [SerializeField] private Transform bossTransform;
        [SerializeField] private ParticleSystem bossPunchParticle;
        [SerializeField] private ParticleSystem confettiParticle;
        [SerializeField] private Animator cinemachineAnimator;
        [SerializeField] private HealthBar playerHealthBar;
        [SerializeField] private int playerDamage;


        [SerializeField] private PlayerController _playerController;

        [SerializeField] private HealthBar bossHealthBar;
        [SerializeField] private HealthBar tapBar;
        [SerializeField] private Animator bossAnimator;
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private TextMeshProUGUI fightText;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private TextMeshProUGUI loseText;
        [SerializeField] private Button nextLevelBut;
        [SerializeField] private Button reloadLevelBut;
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerFightState FightState { get; private set; }
        public PlayerWinState WinState { get; private set; }

        public PlayerLoseState LoseState { get; private set; }

        public Button NextLevelBut => nextLevelBut;

        public Button ReloadLevelBut => reloadLevelBut;
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

        public ParticleSystem ConfettiParticle => confettiParticle;

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
            var playerDamageFactor = PlayerController.CollectedManCount * 0.02;
            bossPunchParticle.Play();
            bossAnimator.SetTrigger("hit");
            tapBar.SetHealth(tapBar.GetHealth - 25);
            DecreaseBossHealth((int) (playerDamage * playerDamageFactor));
            
        }

        private void DecreaseBossHealth(int damage)
        {
            var currentHealth = bossHealthBar.GetHealth;
            bossHealthBar.SetHealth(currentHealth - damage);
        }
    }
}