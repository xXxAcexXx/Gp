using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        //int startingHealth = 100;            // The amount of health the enemy starts the game with.
		EnemyStat Stats;// The current health the enemy has.
        Animator anim;      // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;    // Whether the enemy is dead.
		float timer;
		float temptimeforInvinsible=0;
		float temptimeforDamageOverTime=0;
		int countfordamageovertime=0;
		bool invinsible=false;

        void Awake ()
        {
            anim = GetComponent <Animator> ();
			Stats = GetComponent<EnemyStat>();
			//Stats.Awake.Play ();
            enemyAudio = GetComponent <AudioSource> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            Stats.CurrentHealth = Stats.StartingHealth;
        }

        public void TakeDamage (int amount)
        {
			//Debug.Log (Stats.Invinsible);
			if(invinsible)
                return;
			if(isDead)
				return;
			//Stats.Hurt.Play ();
			//Debug.Log (Stats.CurrentHealth);
            Stats.CurrentHealth -= amount;

			Stats.healthslider.value = Stats.CurrentHealth;

        }

		void Update()
		{
			//Debug.Log (Stats.CurrentHealth);
			timer += Time.deltaTime;
			Stats.healthslider.value = Stats.CurrentHealth;
			if (Stats.CurrentHealth <= 0) {
				Death ();
			} 
			else 
			{
				if (Stats.effects [1] == true && countfordamageovertime < 4)
					DamageOverTime (timer);
				//Debug.Log (effects [3]);
				if (Stats.effects [3] == true)
					SetInvinsible (timer);
			}
			//Debug.Log (Stats.effects [3]);
		}
		void SetInvinsible (float t)
		{
			if (temptimeforInvinsible == 0) 
			{
				temptimeforInvinsible = t;
				invinsible = true;
			}
			//Debug.Log (invinsible);
			//Debug.Log (t - temptimeforInvinsible);
			if (t - temptimeforInvinsible >= 10f) 
			{
				invinsible = false;
				temptimeforInvinsible = 0;
				Stats.effects [3] = false;
			}
			
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
		void DamageOverTime(float time)
		{	
			if(invinsible)
				return;
			if(isDead)
				return;
			if (temptimeforDamageOverTime == 0) 
			{
				temptimeforDamageOverTime = time;
				countfordamageovertime++;
				Stats.CurrentHealth -= 5;
			}
			if (time - temptimeforDamageOverTime >= 5)
			{
				countfordamageovertime++;
				Stats.CurrentHealth -= 5;
			}
			if(countfordamageovertime>=4)
			{	
				countfordamageovertime=0;
				Stats.effects[1]=false;
			}
			Stats.healthslider.value = Stats.CurrentHealth;
		}
    }
}