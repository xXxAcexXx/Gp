using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class InvinsibilityAttack : MonoBehaviour 
	{

		//Transform Bulltrans; 
		Animator anim;                              // Reference to the animator component.
		float timer;            
		float difference;
		EnemyStat Stats;
		float tempattspeed;
		EnhancerMovement EnMov;
		void Awake ()
		{
			Stats = GetComponent<EnemyStat>();
			anim = gameObject.GetComponent<Animator>();
			EnMov = GetComponent<EnhancerMovement> ();//Debug.Log (anim.name);
			tempattspeed=Stats.AttackSpeed;
			Stats.AttackSpeed = 0;
		}
		void Update()
		{
			//Debug.Log(EnMov.BuildingInRange());
			timer += Time.deltaTime;
			//			Debug.Log (EnMov.BuildingInRange());
			//			Debug.Log (EnMov.NextTarget.Applied);
			//			Debug.Log (EnMov.NextTarget.obj);
			//Debug.Log(EnMov.BuildingInRange ());
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
			if (OB.GetComponent<EnemyStat> ().CurrentHealth > 0) 
			{
				anim.SetTrigger ("Attack");
				var Aura = (GameObject)Instantiate (Stats.ProjectilePrefab, OB.transform.position, transform.rotation);
				Aura.GetComponent<AuraFollow> ().SetTarget (OB);
				//StartCoroutine("WaitFor3Sec");
				//OB.GetComponent<EnemyStat>().effects[3] = true;
				OB.GetComponent<EnemyStat>().effects[0] = true;
				//Debug.Log (OB.tag);
				//Debug.Log (OB.GetComponent<EnemyStat> ().effects [3]);
				// Destroy the bullet after 2 seconds
				Destroy (Aura, 10f);
				//EnMov.enemy = EnMov.TargetEnemy (EnMov.enemy);
			}
		}
		//		IEnumerator WaitFor3Sec()
		//		{
		//			yield return new WaitForSeconds (3);
		//		}
	}
}
