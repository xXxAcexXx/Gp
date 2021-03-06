﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CompleteProject
{
	public class DasherEnemyMovement : MonoBehaviour
	{
		NavMeshAgent agent;
		GameObject Castle;
		GameObject [] MyTtowers;
		GameObject NextTarget;
		//bool playerInRange;  
		float difference;
		Animator anim;     // Whether player is within the trigger collider and can be attacked.
		EnemyStat Stats;
		int count;
		int iter=0;
		public SkinnedMeshRenderer SR1;
		public SkinnedMeshRenderer SR2;
		float temptimeforSlowdown=0;
		float temptimeforLure=0;
		[HideInInspector]
		public bool Stop=false;
		GameObject temptarget;
		float timer; 
		void Awake ()
		{
			// Set up the references.
			agent = GetComponent<NavMeshAgent> ();
			Stats = GetComponent<EnemyStat>();
			agent.speed = Stats.EnemySpeed;
			anim = GetComponent<Animator> ();
			MyTtowers = GameObject.FindGameObjectsWithTag("Towers");
			Castle = GameObject.FindGameObjectWithTag("Castle");
			MyNextTarget();
			Debug.Log (Stats.CurrentHealth);
			transform.LookAt (NextTarget.transform);
		}
		void Update ()
		{
			//Debug.Log (Stats.CurrentHealth);
			if (Stats.CurrentHealth > 0) {
				//Debug.Log (Stats.CurrentHealth);
				if (NextTarget != null) {
					//difference = Vector3.Distance (transform.position, NextTarget.transform.position);
					//Debug.Log (difference);
					if (!Stop) 
					{
						iter++;
						count = Random.Range (0, 400);
						//Debug.Log (count);
						anim.SetBool ("PlayerInRange", false);
						agent.destination = NextTarget.transform.position;
						//transform.position = Vector3.MoveTowards (transform.position, NextTarget.transform.position, Time.deltaTime * Stats.EnemySpeed);
						SR1.enabled = true;
						SR2.enabled = true;
						gameObject.GetComponentInChildren<Canvas> ().enabled = true;
						if (iter > 50) 
						{
							if (iter == 51) 
							{
								var exp = Instantiate (Stats.ProjectilePrefab, transform.position, transform.rotation);
								Destroy (exp, 2f);
							}
							SR1.enabled = false;
							SR2.enabled = false;
							gameObject.GetComponentInChildren<Canvas> ().enabled = false;
							if (count < 30) {
								//gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
								transform.position = new Vector3 (transform.position.x + 0.3f, transform.position.y, transform.position.z);
							} else if (count > 100 && count < 130) {
								//gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
								transform.position = new Vector3 (transform.position.x - 0.3f, transform.position.y, transform.position.z);
							} else if (count > 200 && count < 230) {
								//gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
								transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 0.3f);
							} else if (count > 300 && count < 330) {
								//gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
								transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 0.3f);
							}
						}
						if (iter > 100)
							iter = 0;
						//transform.LookAt (NextTarget.transform);
					} 
					else
						StopMoving ();
				} 
				else
				{
					AttackCastle ();
					if (NextTarget == null)
						StopMoving();
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
			SR1.enabled = true;
			SR2.enabled = true;
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
			float Dist = 99999999;
			foreach (var item in MyTtowers)
			{
				if (Vector3.Distance(transform.position, item.transform.position) < Dist)
				{
					Dist = Vector3.Distance(transform.position, item.transform.position);
					NextTarget = item;
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
			if (NextTarget.tag == "Castle") 
			{
				if (NextTarget.GetComponent<Castle> ().Health <= 0)
					return true;
				else
					return false;
			} 
			else if (NextTarget.tag == "Towers") 
			{
				if (NextTarget.GetComponent<Tower> ().Health <= 0)
					return true;
				else
					return false;
			}
			else 
				return true;
		}
	}
}