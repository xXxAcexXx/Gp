using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        int startingHealth = 100;            // The amount of health the enemy starts the game with.
		EnemyStat Stats;// The current health the enemy has.
        Animator anim;      // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;    // Whether the enemy is dead.
		float timer;   

        void Awake ()
        {
            anim = GetComponent <Animator> ();
			Stats = GetComponent<EnemyStat>();
			//Stats.Awake.Play ();
            enemyAudio = GetComponent <AudioSource> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
        }

        public void TakeDamage (int amount)
        {
            if(isDead)
                return;
			if (Stats.Invinsible)
				return;
			//Stats.Hurt.Play ();
			Debug.Log (Stats.CurrentHealth);
            Stats.CurrentHealth -= amount;

			Stats.healthslider.value = Stats.CurrentHealth;

			Debug.Log (Stats.CurrentHealth);
			if(Stats.CurrentHealth <= 0)
            {
                Death ();
            }
        }

		void Update()
		{
			timer += Time.deltaTime;
			Stats.healthslider.value = Stats.CurrentHealth;
			if (timer > 20f)
				ResetInvinsible();
		}
		void ResetInvinsible ()
		{
			timer = 0;
			Stats.Invinsible = false;
		}

        void Death ()
        {
			//Stats.DeathClip.Play ();
            isDead = true;
			//Gmlogic.Experience += 10;//mohem neeeek el kos neeek
            capsuleCollider.isTrigger = true;

            anim.SetTrigger ("Dead");

			//enemyAudio.clip = Stats.DeathClip;
            enemyAudio.Play ();
			Destroy (gameObject, 3f);
        }
    }
}