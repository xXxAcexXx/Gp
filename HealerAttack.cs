using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class HealerAttack : MonoBehaviour 
	{

		Animator anim;
		float timer;            
		float difference;
		EnemyStat Stats;
		EnhancerMovement EnMov;
		float tempattspeed;
		void Awake ()
		{
			Stats = GetComponent<EnemyStat>();
			anim = gameObject.GetComponent<Animator>();
			EnMov = GetComponent<EnhancerMovement> ();
			tempattspeed=Stats.AttackSpeed;
			Stats.AttackSpeed = 0;
		}
		void Update()
		{
			timer += Time.deltaTime;
			if (EnMov.NextTarget != null) 
			{
				if (timer >= Stats.AttackSpeed && EnMov.BuildingInRange () && Stats.CurrentHealth > 0) 
				{
					ApplyAura (EnMov.NextTarget);
				}
			}
		}
		public void ApplyAura (GameObject OB)
		{
			Stats.AttackSpeed = tempattspeed;
			// Reset the timer.
			timer = 0f;
			if (OB.GetComponent<EnemyStat> ().CurrentHealth > 0) {
				anim.SetTrigger ("Attack");
				var Aura = (GameObject)Instantiate (Stats.ProjectilePrefab, OB.transform.position, transform.rotation);
				Aura.GetComponent<AuraFollow> ().SetTarget (OB);
				OB.GetComponent<EnemyStat> ().CurrentHealth = 100;
				Destroy (Aura, 3f);
				EnMov.NextTarget = null;
			}
		}
	}
}