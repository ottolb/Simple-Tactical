using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class SpawnOnInit : MonoBehaviour
	{

		public GameObject prefab;

		// Use this for initialization
		void Start ()
		{
			Instantiate (prefab);
		}
	}
}