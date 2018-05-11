using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class InvinsibilityMovement : MonoBehaviour 
	{
		public class Unit
		{
			public GameObject obj;
			public float scor;
			public bool Applied;
			public float dist;
		}
		//GameObject [] MyEnemies;
		public Unit NextTarget;
		Unit [] EnemiesToApplyAbility;
		public string[] tags;
		float difference;
		Animator anim;     // Whether player is within the trigger collider and can be attacked.
		EnemyStat Stats;
		//EnhancerAttack EnAtt;
		int num_of_enemies;
		bool Done;
		[HideInInspector]
		public int enemy;
		bool GameOver = false; //FARID DA MOGARAD TEMP 3shan A3rAF EL GAME KHELST
		float timer; 
		//		public int Iter()
		//		{
		//			return enemy;
		//		}
		void Awake ()
		{
			anim = GetComponent<Animator> ();
			Stats = GetComponent<EnemyStat>();
			//EnAtt = GetComponent<EnhancerAttack> ();
			MyNextTarget();
			//Debug.Log (Stats.CurrentHealth);
		}
		void Update()
		{
			timer += Time.deltaTime;
			if (enemy>=num_of_enemies || timer>10f)
				MyNextTarget ();
			if (Stats.CurrentHealth > 0) 
			{
				//Debug.Log (Stats.CurrentHealth);
				if (NextTarget != null ) 
				{
					//Debug.Log (NextTarget.Applied);
					if (NextTarget.obj != null) {
						difference = Vector3.Distance (transform.position, NextTarget.obj.transform.position);
						//Debug.Log (difference);
						if (difference >= Stats.Range) {
							anim.SetBool ("PlayerInRange", false);
							transform.position = Vector3.MoveTowards (transform.position, NextTarget.obj.transform.position, Time.deltaTime * Stats.EnemySpeed);
							transform.LookAt (NextTarget.obj.transform);
						} else {
							transform.position = transform.position;
							transform.LookAt (NextTarget.obj.transform);
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
		public int TargetEnemy(int o)
		{
			//Debug.Log (o);
			//Debug.Log (num_of_enemies);
			if (o >= num_of_enemies)
			{
				//MyNextTarget ();
				return 999;
			}
			//Done = false;

			//Debug.Log (EnemiesToApplyAbility.Length);
			if (Stats.CurrentHealth > 0 && EnemiesToApplyAbility [o].Applied == false && o < EnemiesToApplyAbility.Length) 
			{
				NextTarget = EnemiesToApplyAbility [o];
				//Debug.Log (EnemiesToApplyAbility [o].obj);
				//Debug.Log (NextTarget.obj);
				return o + 1;
			}
			return o + 1;
		}
		void MyNextTarget()
		{
			enemy = 0;
			timer = 0f;
			//Debug.Log (tags.Length);
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
			//Debug.Log (num_of_enemies);
			if (num_of_enemies == 0)
				return;
			EnemiesToApplyAbility=new Unit[num_of_enemies];
			//int index;
			int count = 0;
			//Debug.Log (EnemiesToApplyAbility.Length);
			for (int i = 0; i < tags.Length; i++) 
			{
				//Debug.Log (i);
				//Debug.Log (Enemies [i].Length);
				for (int p = 0; p < Enemies [i].Length; p++) 
				{
					//Debug.Log (p);
					//index = p + i;
					//Debug.Log (count);
					//Debug.Log (num_of_enemies);
					if (num_of_enemies < count)
						return;
					Unit temp = new Unit ();
					Unit temptemp = new Unit ();
					//if (Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().CurrentHealth < 100) 
					//Debug.Log (Enemies [i] [p].gameObject);
					//Debug.Log(Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().CurrentHealth);
					//Debug.Log (Enemies [i] [p].gameObject.tag);
					if (Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().Invinsible)
						temp.Applied = true;
					else
						temp.Applied = false;

					Debug.Log (temp.Applied);
					temp.obj = Enemies [i] [p].gameObject;

					temp.dist = Vector3.Distance (transform.position, Enemies [i] [p].gameObject.transform.position);
					//Debug.Log (temp.dist);
					temp.scor = Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().Type * temp.dist * Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().AttackDamage * Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().EnemySpeed * Enemies [i] [p].gameObject.GetComponent<EnemyStat> ().AttackSpeed;
					//Debug.Log (index);
					//Debug.Log (num_of_enemies);
					//Debug.Log (temp.scor);
					if (EnemiesToApplyAbility [0] == null) {
						//Debug.Log (temp.obj);
						EnemiesToApplyAbility [0] = temp;
						Debug.Log (EnemiesToApplyAbility [0].obj);
					} 
					else if (EnemiesToApplyAbility [count - 1].scor > temp.scor)
					{	
						EnemiesToApplyAbility [count] = temp;
						Debug.Log (EnemiesToApplyAbility [1].obj);
					}
					else if (EnemiesToApplyAbility [count - 1].scor < temp.scor) 
					{
						temptemp = EnemiesToApplyAbility [count - 1]; 
						EnemiesToApplyAbility [count - 1] = temp;
						EnemiesToApplyAbility [count] = temptemp;
						int y = 1;
						if (count - 1 - y >= 0)
						{
							while (EnemiesToApplyAbility [count - 1 - y].scor < temp.scor) 
							{
								Unit tmp = new Unit ();
								tmp = EnemiesToApplyAbility [count - 1 - y]; 
								EnemiesToApplyAbility [count - 1 - y] = temp;
								EnemiesToApplyAbility [count - 1] = tmp;
								if (count - 1 - y == 0)
									break;
								else
									y++;
							}
						}
						Debug.Log (EnemiesToApplyAbility[1].obj);
					}
					//Debug.Log (EnemiesToApplyAbility[0].obj);//Debug.Log (EnemiesToApplyAbility.Length);
					count++;
				}
			}
			//Debug.Log (EnemiesToApplyAbility[0].obj);
			//Debug.Log (EnemiesToApplyAbility[1].obj);
			//Debug.Log (EnemiesToApplyAbility[2].obj);
			enemy=TargetEnemy (0);
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
