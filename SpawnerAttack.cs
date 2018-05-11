using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class SpawnerAttack : MonoBehaviour 
	{
		Animator anim;                              // Reference to the animator component.
		float timer;                                    // Reference to the line renderer.
		float difference;
		int PlayerMask;
		EnemyMovement Mov;
		EnemyStat Stats;
		bool spawned=false;
		public GameObject Minionobj;

		void Awake ()
		{
			// Setting up the references.
			//PlayerMask = LayerMask.GetMask ("PlayerMask");
			Stats = GetComponentInParent<EnemyStat>();
			anim = gameObject.GetComponentInParent<Animator>();
			Mov = gameObject.GetComponentInParent<EnemyMovement>();
			//Debug.Log (anim.name);
		}

		void Update ()
		{
			timer += Time.deltaTime;

			if(timer >= Stats.AttackSpeed && Mov.BuildingInRange()  && Stats.CurrentHealth > 0 && !(Mov.BuildingDead()))
			{
				Spawn ();
				Attack ();
			}
		}

		void Spawn()
		{
			if (!spawned) {
				Vector3 spoint = new Vector3 (1, 0, 0);
				var minion = (GameObject)Instantiate (Minionobj, transform.position + spoint, transform.rotation);
				for (int i = 0; i < 3; i++) {
					switch (i) {
					case 1:
						{
							spoint.x = 0;
							spoint.z = 1;
							minion = (GameObject)Instantiate (Minionobj, transform.position + spoint, transform.rotation);
							break;
						}
					case 2:
						{
							spoint.x = 0;
							spoint.z = -1;
							minion = (GameObject)Instantiate (Minionobj, transform.position + spoint, transform.rotation);
							break;
						}
//					case 3:
//						{
//							spoint.z = -1;
//							spoint.x = 1;
//							minion = (GameObject)Instantiate (Minionobj, transform.position + spoint, transform.rotation);
//							break;
//						}
//					case 4:
//						{
//							spoint.z = 1;
//							spoint.x = -1;
//							minion = (GameObject)Instantiate (Minionobj, transform.position + spoint, transform.rotation);
//							break;
//						}
					}
				
				}
				spawned = true;
			}
		}

		void Attack ()
		{
			// Reset the timer.
			timer = 0f;
			anim.SetTrigger ("Attack");

			Vector3 allt = new Vector3 (0, 2, 0);
			var spell = (GameObject)Instantiate (Stats.ProjectilePrefab,transform.position+allt,transform.rotation);
			// Add velocity to the bullet
			spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * 10f;

			// Destroy the bullet after 2 seconds
			Destroy(spell, 3f);
		}

	}
}
