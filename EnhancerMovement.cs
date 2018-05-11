using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class EnhancerMovement : MonoBehaviour 
	{
		[HideInInspector]
		public GameObject NextTarget;
		public string[] tags;
		float difference;
		Animator anim;
		EnemyStat Stats;
		GameObject C;
		int num_of_enemies;
		[HideInInspector]
		bool GameOver = false; //FARID DA MOGARAD TEMP 3shan A3rAF EL GAME KHELST
		float timer; 
		void Awake ()
		{
			C=GameObject.FindGameObjectWithTag("Castle");
			anim = GetComponent<Animator> ();
			Stats = GetComponent<EnemyStat>();
			MyNextTarget();
		}
		void Update()
		{
			timer += Time.deltaTime;
			if (timer>10f)
				MyNextTarget ();
			if (Stats.CurrentHealth > 0) 
			{
				//Debug.Log (Stats.CurrentHealth);
				if (NextTarget != null ) 
				{
					if (NextTarget != null) {
						difference = Vector3.Distance (transform.position, NextTarget.transform.position);
						//Debug.Log (difference);
						if (difference >= Stats.Range) {
							anim.SetBool ("PlayerInRange", false);
							transform.position = Vector3.MoveTowards (transform.position, NextTarget.transform.position, Time.deltaTime * Stats.EnemySpeed);
							transform.LookAt (NextTarget.transform);
						} else {
							transform.position = transform.position;
							transform.LookAt (NextTarget.transform.position);
							anim.SetBool ("PlayerInRange", true);
						}
					}
				} 
				else 
				{
					anim.SetBool ("PlayerInRange", true);
					transform.position = transform.position;
				}
			}
			if(GameOver)
				anim.SetBool ("PlayerDead", true);
		}
		public void MyNextTarget()
		{
			timer = 0f;
			GameObject[][] Enemies = new GameObject[tags.Length][];
			num_of_enemies = 0;
			if (num_of_enemies == 0) 
			{
				for (int j = 0; j < tags.Length; j++) 
				{
					Enemies [j] = GameObject.FindGameObjectsWithTag (tags [j]);
					num_of_enemies += Enemies [j].Length;
				}
			}
			if (num_of_enemies == 0)
				return;
			float Scor = 999999;
			GameObject trgt=null;
			for (int i = 0; i < tags.Length; i++) 
			{
				for (int p = 0; p < Enemies [i].Length; p++) 
				{
					if (this.tag == "Healer") 
					{
						float tempscore = Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().CurrentHealth * Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().Type * Vector3.Distance (transform.position, Enemies [i] [p].gameObject.transform.position);
						if (tempscore < Scor && Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().CurrentHealth < 100) {
							Scor = tempscore;
							trgt = Enemies [i] [p].gameObject;
						}
					} 
					else if (this.tag == "Shielder") 
					{
						float tempscore = Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().CurrentHealth * Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().Type * Vector3.Distance (transform.position, Enemies [i] [p].gameObject.transform.position * Vector3.Distance (C.transform.position, Enemies [i] [p].gameObject.transform.position) * (1/Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().AttackDamage));
						if (tempscore < Scor && Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().CurrentHealth < 100) 
						{
							Scor = tempscore;
							trgt = Enemies [i] [p].gameObject;
						}
					}
				}
			}
			NextTarget = trgt;
		}
		public bool BuildingInRange ()
		{
			if (NextTarget == null)
				return false;
			if (difference <= Stats.Range) 
				return true;
			else
				return false;
		}
	}
}
