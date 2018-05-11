using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class Tower : MonoBehaviour {

	    // Use this for initialization
	    public int HitPoints = 5;
		EnemyMovement Enemy;
		void Start ()
	    {
			
		}
		void Update()
		{
			if (HitPoints <= 0)
				Destroy (gameObject);
		}
		// Update is called once per fram
		public void TakeHit(int Damage)
	    {
			Debug.Log (HitPoints);
	        HitPoints -= Damage;
	    }
		void OnTriggerEnter (Collider other)
		{
			Debug.Log (other.tag);
		//	Debug.Log(other.tag);
			if (other.tag == "Bullet")
				TakeHit (other.gameObject.GetComponent<Bulletcollision>().Damage);
		}
	}
}
