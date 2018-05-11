using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class SuicidalEnemyAttack : MonoBehaviour
    {	
        //GameObject exp;                          // Reference to the player GameObject.
		public GameObject explosion;
		bool BuildingType;
		//AudioSource audio;
		Tower T;
		Castle C;
		EnemyStat Stats;

		void Awake ()
		{
			// Setting up the references.
			//exp = GameObject.FindGameObjectWithTag ("SuicidalExplosion");
			//explosion=exp.GetComponent<ParticleSystem>();
			//audio = explosion.GetComponent<AudioSource> ();
			Stats = GetComponent<EnemyStat>();
		}
		void OnTriggerEnter (Collider other)
		{
			//Debug.Log (other.tag);
			if (other.tag == "Towers") 
			{
				T = other.GetComponentInParent<Tower> ();
				BuildingType = false;
			} 
			else if (other.tag == "Castle") 
			{
				C = other.GetComponentInParent<Castle> ();
				BuildingType = true;
				//anim.SetBool ("ToCastle", false);
			}
			if (BuildingType) 
			{
				if (C.HitPoints > 0)
					C.TakeHit (Stats.AttackDamage);
				else
				{
					Destroy(C.gameObject);
				}
			}
			else 
			{
				if (T.HitPoints > 0)
					T.TakeHit (Stats.AttackDamage);
				else 
				{
					Destroy (T.gameObject);
				}
			}
			//Debug.Log (gameObject.name);
			var exp = Instantiate (explosion,transform.position, transform.rotation);
		//	exp.transform.position = transform.position;
		//	exp.transform.rotation = transform.rotation;
			Destroy (gameObject, 1f);
			Destroy (exp, 3f);
        }
			
    }
}