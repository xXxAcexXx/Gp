using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class AuraFollow : MonoBehaviour 
	{

		// Use this for initialization
		GameObject target;
		// Update is called once per frame
		void Update () {
			if(target!=null)
				transform.position = target.transform.position;
			if (target.GetComponent<EnemyStat> ().CurrentHealth <= 0)
				Destroy (gameObject);
		}
		public void SetTarget(GameObject GO)
		{
			target = GO;
	
		}
	}
}
