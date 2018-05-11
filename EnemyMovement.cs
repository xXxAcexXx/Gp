﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
		NavMeshAgent agent;
		GameObject Castle;
		GameObject [] MyTtowers;
		GameObject NextTarget;
		//bool playerInRange;  
		float difference;
		Animator anim;     // Whether player is within the trigger collider and can be attacked.
		EnemyStat Stats;
		int count = 0;
        void Awake ()
        {
            // Set up the references.
			//Debug.Log(tag);
			Stats = GetComponent<EnemyStat>();
			if (tag != "SuicidalSoldier") 
			{
				agent = GetComponent<NavMeshAgent> ();
				agent.speed = Stats.EnemySpeed;
				agent.stoppingDistance = Stats.Range - 1;
			}
			anim = GetComponent<Animator> ();
			MyTtowers = GameObject.FindGameObjectsWithTag("Towers");
			Castle = GameObject.FindGameObjectWithTag("Castle");
			MyNextTarget();
			transform.LookAt (NextTarget.transform);
			//Debug.Log (NextTarget.transform);
        }
			
        void Update ()
        {
			//Debug.Log (Stats.CurrentHealth);
			if (Stats.CurrentHealth > 0) 
			{
				//Debug.Log (Stats.CurrentHealth);
				if (NextTarget != null) 
				{
					difference = Vector3.Distance (transform.position, NextTarget.transform.position);
					//Debug.Log (difference);
					if (difference >= Stats.Range) 
					{
						if (this.tag == "Spawner") 
						{
							if (count >= Stats.AttackSpeed)
								StopMoving ();
							else 
							{
								anim.SetBool ("PlayerInRange", false);
								if (tag != "SuicidalSoldier")
									agent.destination = NextTarget.transform.position;
								else
									transform.position = Vector3.MoveTowards (transform.position, NextTarget.transform.position, Time.deltaTime * Stats.EnemySpeed);
							}
						}
						else
						{
							anim.SetBool ("PlayerInRange", false);
							if(tag != "SuicidalSoldier")
								agent.destination = NextTarget.transform.position;
							else
								transform.position= Vector3.MoveTowards (transform.position, NextTarget.transform.position, Time.deltaTime * Stats.EnemySpeed);
							//transform.LookAt (NextTarget.transform);
						}
						count++;
					} 
					else 
					{
						transform.position = transform.position;
						transform.LookAt (NextTarget.transform);
						anim.SetBool ("PlayerInRange", true);
					}
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

		void StopMoving()
		{
			count = 0;
			agent.destination = agent.destination;
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
			difference = 999;
		}
		public bool BuildingInRange ()
		{
			if (difference <= Stats.Range)
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