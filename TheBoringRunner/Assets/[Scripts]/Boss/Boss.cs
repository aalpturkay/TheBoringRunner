using UnityEngine;

namespace Boss
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private ParticleSystem playerPunchParticle;
        [SerializeField] private HealthBar playerHealthBar;
        [SerializeField] private PlayerController _playerController;

        public void PlayPlayerPunchParticle()
        {
            if (playerHealthBar.GetHealth <= 0) return;
            playerPunchParticle.Play();
            print("hit run");
            _playerController.PlayerAnimator.SetTrigger("hit");
            PlayerTakeDamage(2);
        }

        private void PlayerTakeDamage(int damage)
        {
            var health = playerHealthBar.GetHealth;
            playerHealthBar.SetHealth(health - damage);
        }
    }
}