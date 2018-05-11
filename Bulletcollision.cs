using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CompleteProject
{
	public class Bulletcollision : MonoBehaviour 
	{

		Castle c;
		Tower t;
		// Use this for initialization
		public int Damage=2;
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {

		}
		void OnTriggerEnter (Collider other)
		{
			Debug.Log (other.tag);
			if (other.tag == "Towers") {
				t = other.GetComponent<Tower> ();
				t.TakeHit (Damage);
			}
			else if( other.tag == "Castle")
			{
				c = other.GetComponent<Castle> ();
					c.TakeHit (Damage);
			}
			Destroy (gameObject);//destroy the bullet
		}
	}
}
