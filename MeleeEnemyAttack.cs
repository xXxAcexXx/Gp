using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class MeleeEnemyAttack : MonoBehaviour
    {               // The amount of health taken away per attack.

		Animation MyAnimator;
		Animator anim;                              // Reference to the animator component.
        EnemyHealth enemyHealth;                    // Reference to this enemy's health.
        bool BuildingInRange;                         // Whether player is within the trigger collider and can be attacked.
        float timer;                                // Timer for counting up to the next attack.
		bool BuildingDead;
		Castle C;
		Tower T;
		float tempattspeed;
		MeleeEnemyMovement enmov;
		DasherEnemyMovement dmov;
		bool BuildingType=false;  //false for tower,true for castle
		//Vector3 temp;
		EnemyStat Stats;

        void Awake ()
        {
			Stats = GetComponent<EnemyStat>();
			anim = GetComponent<Animator> ();
			if(gameObject.tag=="Dasher")
				dmov = GetComponent<DasherEnemyMovement> ();
			else
				enmov = GetComponent<MeleeEnemyMovement> ();
			tempattspeed=Stats.AttackSpeed;
			Stats.AttackSpeed = 0;
        }

        void OnTriggerEnter (Collider other)
        {
			if (other.tag == "Towers")
			{
				if (gameObject.tag == "Dasher")
					dmov.Stop = true;
				else
					enmov.Stop = true;
				T = other.GetComponentInParent<Tower> ();
				BuildingInRange = true;
				BuildingDead = false;
				BuildingType = false;

			} 
			else if (other.tag == "Castle")
			{
				if(gameObject.tag=="Dasher")
					dmov.Stop = true;
				else
					enmov.Stop = true;
				C = other.GetComponentInParent<Castle> ();
				BuildingInRange = true;
				BuildingDead = false;
				BuildingType = true;
			}
				
        }
       	void Update ()
        {
			timer += Time.deltaTime;
            if(timer >= Stats.AttackSpeed && BuildingInRange && Stats.CurrentHealth > 0 && !BuildingDead)
			{
				Attack ();
				if (BuildingDead) 
				{
					BuildingInRange = false;
				}
            }
        }

        void Attack ()
        {
			// Reset the timer.
			Stats.AttackSpeed = tempattspeed;
			timer = 0f;
			if (BuildingType) 
			{
				if (C.Health > 0) 
				{
					anim.SetTrigger ("Attack");
					C.TakeHit (Stats.AttackDamage);
				}
				else
				{
					BuildingDead = true;
					anim.SetBool ("PlayerDead", true);
					if(gameObject.tag=="Dasher")
						dmov.Stop = false;
					else
						enmov.Stop = false;
				}
			}
			else 
			{
				if (T.Health > 0) 
				{
					anim.SetTrigger ("Attack");
					T.TakeHit (Stats.AttackDamage);
				}
				else 
				{
					BuildingDead = true;
					if(gameObject.tag=="Dasher")
						dmov.Stop = false;
					else
						enmov.Stop = false;
				}
			}
		}
	}
}