using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
	public class EnemyStat : MonoBehaviour {

		// Use this for initialization
		[HideInInspector]
		public int CurrentHealth = 100; 
		[HideInInspector]
		public bool Invinsible = false;
		public float AttackSpeed;     // The time in seconds between each attack.
		public float EnemySpeed;
		public int AttackDamage;                  // The damage inflicted by each bullet.
		public float Range;
		public int Type;
		//public AudioSource DeathClip;                 // The sound to play when the enemy dies.
		//public AudioSource Hurt;
		//public AudioSource Awake;
		public Slider healthslider;
		public GameObject ProjectilePrefab;
	}
}
