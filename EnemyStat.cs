using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
	public class EnemyStat : MonoBehaviour {

		//[HideInInspector]
		//public GameController gameControlGameObject.FindGameObjectWithTag("gameController");;
		// Use this for initialization
		[HideInInspector]
		public int CurrentHealth; 
		//public bool Invinsible = false;
		public float AttackSpeed;     // The time in seconds between each attack.
		public float EnemySpeed;
		public int AttackDamage;                  // The damage inflicted by each bullet.
		public float Range;
		public int Type;
		public int StartingHealth;
		//public AudioSource DeathClip;                 // The sound to play when the enemy dies.
		//public AudioSource Hurt;
		//public AudioSource Awake;
		public Slider healthslider;
		public GameObject ProjectilePrefab;
		[HideInInspector]
		public bool[] effects={false,false,false,false};//0.lure,1.damageovertime,2.slow speed down 3.Invinsible

	}
	//void Awake ()
	//{
		//gamecontrol=GameObject.FindGameObjectWithTag("gameController");
		//Debug.Log (anim.name);
	//}
}
