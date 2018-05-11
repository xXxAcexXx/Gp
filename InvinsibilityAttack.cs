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
		InvinsibilityMovement EnMov;
		void Awake ()
		{
			Stats = GetComponent<EnemyStat>();
			anim = gameObject.GetComponent<Animator>();
			EnMov = GetComponent<InvinsibilityMovement> ();//Debug.Log (anim.name);
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
				if (timer >= Stats.AttackSpeed && EnMov.BuildingInRange () && Stats.CurrentHealth > 0 && EnMov.NextTarget.Applied == false) 
				{
					ApplyAura (EnMov.NextTarget.obj);
				}
			}
		}
		public void ApplyAura (GameObject OB)
		{
			// Reset the timer.
			timer = 0f;
			if (OB.GetComponent<EnemyStat> ().CurrentHealth > 0) {
				anim.SetTrigger ("Attack");
				var Aura = (GameObject)Instantiate (Stats.ProjectilePrefab, OB.transform.position, transform.rotation);
				Aura.GetComponent<AuraFollow> ().SetTarget (OB);
				//StartCoroutine("WaitFor3Sec");
				OB.GetComponent<EnemyStat> ().Invinsible = true;
				// Destroy the bullet after 2 seconds
				EnMov.NextTarget.Applied = true;
				Destroy (Aura, 10f);
				EnMov.enemy = EnMov.TargetEnemy (EnMov.enemy);
			}
		}
		//		IEnumerator WaitFor3Sec()
		//		{
		//			yield return new WaitForSeconds (3);
		//		}
	}
}
