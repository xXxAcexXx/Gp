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
				Vector3 pos = new Vector3 (owner.position.x, owner.position.y + col.radius+1f, owner.position.z + col.radius);
				transform.position=pos;	
				
			}
		}
	}
}
