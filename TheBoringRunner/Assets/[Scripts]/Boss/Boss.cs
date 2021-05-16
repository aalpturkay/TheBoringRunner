using System;
using UnityEngine;

namespace Boss
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private ParticleSystem playerPunchParticle;
        [SerializeField] private HealthBar playerHealthBar;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private int bossDamage;

        private void Start()
        {
            SetRagdoll(true);
            Colli(false);
        }

        public void PlayPlayerPunchParticle()
        {
            if (playerHealthBar.GetHealth <= 0) return;
            playerPunchParticle.Play();
            print("hit run");
            _playerController.PlayerAnimator.SetTrigger("hit");
            PlayerTakeDamage(bossDamage);
        }

        private void PlayerTakeDamage(int damage)
        {
            var health = playerHealthBar.GetHealth;
            playerHealthBar.SetHealth(health - damage);
        }
        
        void SetRagdoll(bool isOpen)
        {
            Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
            foreach (var childRb in rb)
            {
                childRb.isKinematic = isOpen;
            }
        }

        void Colli(bool isOpen)
        {
            Collider[] cl = GetComponentsInChildren<Collider>();
            foreach (var childCol in cl)
            {
                childCol.enabled = isOpen;
            }
        }
    }
}