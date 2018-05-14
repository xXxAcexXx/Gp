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
		RangedEnemyMovement Mov;
		EnemyStat Stats;
		float tempattspeed;
		public GameObject Minionobj;

		void Awake ()
		{
			Stats = GetComponentInParent<EnemyStat>();
			anim = gameObject.GetComponentInParent<Animator>();
			Mov = gameObject.GetComponentInParent<RangedEnemyMovement>();
			tempattspeed=Stats.AttackSpeed;
			Stats.AttackSpeed = 0;
			Spawn ();
			//Debug.Log (anim.name);
		}

		void Update ()
		{
			timer += Time.deltaTime;

			if(timer >= Stats.AttackSpeed  && Stats.CurrentHealth > 0 && !(Mov.BuildingDead()))//building in range deh asdy 3aleha ya2ema building f 7alet el enemies el ranged ya2ema enemies in range to apply effects 
			{
				Spawn ();

			}
		}

		void Spawn()
		{
			Stats.AttackSpeed = tempattspeed;
			
			timer = 0;
				anim.SetTrigger ("Attack");
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
					}
				
				}
		}
	}
}
