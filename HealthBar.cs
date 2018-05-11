using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class HealthBar : MonoBehaviour
	{
		public Transform owner;
		CapsuleCollider col;
		// Update is called once per frame
		void Awake ()
		{
			col =GetComponentInParent<CapsuleCollider> ();
		}
		void Update ()
		{
			transform.eulerAngles = Camera.main.transform.eulerAngles;
			if (owner != null) 
			{
				Vector3 pos = new Vector3 (owner.position.x, owner.position.y + col.height, owner.position.z + col.radius);
				transform.position=pos;	
				
			}
		}
	}
}
