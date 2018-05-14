using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CompleteProject
{
    public class MeleeEnemyMovement : MonoBehaviour
    {
		NavMeshAgent agent;
		GameObject Castle;
		GameObject [] MyTtowers;
		GameObject NextTarget;
		GameObject temptarget;
		float timer; 
		//bool playerInRange;  
		//float difference;
		Animator anim;     // Whether player is within the trigger collider and can be attacked.
		EnemyStat Stats;
		//int count = 0;
		public GameObject test;
		float temptimeforSlowdown=0;
		float temptimeforLure=0;
		public bool Stop=false;
        void Awake ()
        {
            // Set up the references.
			//Debug.Log(tag);
			Stats = GetComponent<EnemyStat>();
			if (tag != "SuicidalSoldier") 
			{
				agent = GetComponent<NavMeshAgent> ();
				agent.speed = Stats.EnemySpeed;
				//agent.stoppingDistance = Stats.Range;
			}
			anim = GetComponent<Animator> ();
			MyTtowers = GameObject.FindGameObjectsWithTag("Towers");
			Castle = GameObject.FindGameObjectWithTag("Castle");
			MyNextTarget();
			transform.LookAt (NextTarget.transform);
			Debug.Log(Stop);
			//Debug.Log (difference);
			//Stats.effects [0]=true;
			//Debug.Log (NextTarget.transform);
        }
			
        void Update ()
        {
			//Debug.Log (difference);
			//Debug.Log (Stats.CurrentHealth);
			timer += Time.deltaTime;
			if (Stats.CurrentHealth > 0) 
			{
				if (Stats.effects [2] == true)
					Slowdown (timer);
				//Debug.Log (Stats.CurrentHealth);
				if (NextTarget != null) 
				{
					if (!Stop) {
						agent.destination = NextTarget.transform.position;
						anim.SetBool ("PlayerInRange", false);
					} 
					else
						StopMoving ();
				}
				else 
				{
					AttackCastle ();
					if (NextTarget == null) 
					{
						transform.position = transform.position;
						anim.SetBool ("PlayerDead", true);
					}
				}
	    	}
		}
		void Lure (float t,GameObject tempagent)
		{
			temptarget = NextTarget;
			if (temptimeforLure == 0) 
			{
				temptimeforLure = t;
				agent.destination = tempagent.transform.position;
				//Debug.Log (agent.destination);
				//Debug.Log (NextTarget.transform.position);
			}
			if (t - temptimeforLure >= 10f) 
			{
				NextTarget = temptarget;
				agent.SetDestination(NextTarget.transform.position);
				temptimeforLure = 0;
				Stats.effects [0] = false;
				MyNextTarget();
				transform.LookAt (NextTarget.transform);
			}
		}
		void Slowdown (float t)
		{
			if (temptimeforSlowdown == 0) 
			{
				temptimeforSlowdown = t;
				Stats.EnemySpeed = Stats.EnemySpeed / 2;;
				agent.speed = Stats.EnemySpeed;
			}
			//Debug.Log (invinsible);
			//Debug.Log (t - temptimeforInvinsible);
			if (t - temptimeforSlowdown >= 10f) 
			{
				Stats.EnemySpeed+=1;
				agent.speed = Stats.EnemySpeed;
				temptimeforSlowdown = 0;
				Stats.effects [2] = false;
			}
		}
		void StopMoving()
		{
			//count = 0;
			agent.destination = transform.position;
			transform.position = transform.position;
			//Debug.Log (agent.destination);
			transform.LookAt (NextTarget.transform);
			anim.SetBool ("PlayerInRange", true);
		}
		void MyNextTarget()
		{
			if (MyTtowers.Length == 0) {
				if (Castle != null)
				{
					AttackCastle ();
					return;
				}
			}
			float Dist = 99999;
			if (this.tag == "Spawner") 
			{
				float Score = 99999999;
				foreach (var item in MyTtowers)
				{
					if (Vector3.Distance (transform.position, item.transform.position) * item.GetComponent<Tower> ().Health < Score)
					{
						Score = Vector3.Distance (transform.position, item.transform.position) * item.GetComponent<Tower> ().Health;
						NextTarget = item;
					}
				}
			}
			else
			{
				foreach (var item in MyTtowers)
				{
					if (Vector3.Distance(transform.position, item.transform.position) < Dist)
					{
						Dist = Vector3.Distance(transform.position, item.transform.position);
						NextTarget = item;
					}
				}
			}
			if(Vector3.Distance(transform.position, Castle.transform.position) < Dist)
			{
				Dist = Vector3.Distance(transform.position, Castle.transform.position);
				NextTarget = Castle;
			}
		}
		void AttackCastle()
		{
			NextTarget = Castle;
			if(NextTarget!=null)
				transform.LookAt (NextTarget.transform);
			//difference = 999;
		}
		public bool BuildingInRange ()
		{
			if (Stop)
				return true;
			else
				return false;
		}
		public bool BuildingDead ()
		{
			if (NextTarget != null) 
			{
				if (NextTarget.tag == "Castle") {
					if (NextTarget.GetComponent<Castle> ().Health <= 0)
						return true;
					else
						return false;
				} else if (NextTarget.tag == "Towers") {
					if (NextTarget.GetComponent<Tower> ().Health <= 0)
						return true;
					else
						return false;
				} else
					return true;
			} 
			else
				return true;
		}
	}
	}