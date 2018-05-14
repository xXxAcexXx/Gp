	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

namespace CompleteProject
{
	public class RangedEnemyAttack : MonoBehaviour 
	{
		//Transform Bulltrans; 
		Animator anim;                              // Reference to the animator component.
		float timer;                                       // A raycast hit to get information about what was hit.
		ParticleSystem gunParticles;                    // Reference to the particle system.                        // Reference to the line renderer.
		AudioSource gunAudio;                           // Reference to the audio source
		Light gunLight;               
		public Light faceLight;								// Duh
		float effectsDisplayTime = 0.2f; 
		RangedEnemyMovement Mov;
		float tempattspeed;
		EnemyStat Stats;

		void Awake ()
		{
			gunLight = GetComponent<Light> ();
			gunParticles = GetComponent<ParticleSystem> ();
			gunAudio = GetComponent<AudioSource> ();
			Stats = GetComponentInParent<EnemyStat>();
			anim = gameObject.GetComponentInParent<Animator>();
			Mov = gameObject.GetComponentInParent<RangedEnemyMovement>();
			tempattspeed=Stats.AttackSpeed;
			Stats.AttackSpeed = 0;
		//Debug.Log (anim.name);
		}

		void Update ()
		{
			timer += Time.deltaTime;
			//Debug.Log (Mov.BuildingInRange ());
			if(timer >= Stats.AttackSpeed && Mov.BuildingInRange()  && Stats.CurrentHealth > 0 && !(Mov.BuildingDead()))
			{
				Shoot ();
			}//Debug.Log (anim.tag)

			if(timer >= Stats.AttackSpeed * effectsDisplayTime)
			{
				DisableEffects ();
			}
		}

		public void DisableEffects ()
		{
			// Disable the line renderer and the light.
			faceLight.enabled = false;
			gunLight.enabled = false;
		}
		void Shoot ()
		{
			Stats.AttackSpeed = tempattspeed;
			// Reset the timer.
			timer = 0f;

			// Play the gun shot audioclip.
			gunAudio.Play ();

			// Enable the lights.
			gunLight.enabled = true;
			faceLight.enabled = true;

			// Stop the particles from playing if they were, then start the particles.
			gunParticles.Stop ();
			gunParticles.Play ();
			anim.SetTrigger ("Attack");

			var bullet = (GameObject)Instantiate (Stats.ProjectilePrefab,transform.position,transform.rotation);
			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30f;

			// Destroy the bullet after 2 seconds
			Destroy(bullet, 5f);
		}
	
	}
}