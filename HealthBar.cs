using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class HealthBar : MonoBehaviour
	{
		public Transform owner;
		SphereCollider col;
		// Update is called once per frame
		void Awake ()
		{
			col =GetComponentInParent<SphereCollider> ();
		}
		void Update ()
		{
			transform.eulerAngles = Camera.main.transform.eulerAngles;
			if (owner != null) 
			{
				Vector3 pos;
				if(owner.tag== "Spawner" || owner.tag == "Shielder")
					pos = new Vector3 (owner.position.x, owner.position.y + col.radius/2, owner.position.z + col.radius);
				else
					pos = new Vector3 (owner.position.x, owner.position.y + col.radius + 1.2f, owner.position.z + col.radius);
				//Debug.Log (pos);
				transform.position=pos;	
				
			}
		}
	}
}
