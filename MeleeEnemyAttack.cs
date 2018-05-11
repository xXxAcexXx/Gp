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
		bool BuildingType=false;  //false for tower,true for castle
		Vector3 temp;
		EnemyStat Stats;

        void Awake ()
        {
			Stats = GetComponent<EnemyStat>();
			anim = GetComponent<Animator> ();
        }

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
			//Debug.Log(other.gameObject);
			//Debug.Log(other.tag);
			if (other.tag == "Towers")
			{
				T = other.GetComponentInParent<Tower> ();
				BuildingInRange = true;
				BuildingDead = false;
				BuildingType = false;

			} 
			else if (other.tag == "Castle")
			{
				Debug.Log (other.tag);
				C = other.GetComponentInParent<Castle> ();
				BuildingInRange = true;
				BuildingDead = false;
				BuildingType = true;
				//anim.SetBool ("ToCastle", false);
			}
				
        }

        void Update ()
        {
			timer += Time.deltaTime;
			//Debug.Log (anim.tag);
			//Debug.Log(BuildingDead);
			//Debug.Log (BuildingInRange);
            if(timer >= Stats.AttackSpeed && BuildingInRange && Stats.CurrentHealth > 0 && !BuildingDead)
			{
				//temp = transform.position;
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
			timer = 0f;
			anim.SetTrigger ("Attack");
			if (BuildingType) 
			{
				if (C.Health > 0)
					C.TakeHit (Stats.AttackDamage);
				else
				{
					BuildingDead = true;
					anim.SetBool ("PlayerDead", true);
				}
			}
			else 
			{
				if (T.Health > 0)
					T.TakeHit (Stats.AttackDamage);
				else 
				{
					BuildingDead = true;
					//anim.SetBool ("ToCastle", true);
				}
			}
			//transform.position = temp;
		}
	}
}