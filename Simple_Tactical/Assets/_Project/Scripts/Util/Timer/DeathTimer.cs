//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games 
//

using UnityEngine;
using System.Collections;

namespace Game
{
	/// <summary>
	/// Simple component to destroy game object after some time
	/// </summary>
	public class DeathTimer : MonoBehaviour
	{

		/// <summary>
		/// The time to destroy object 
		/// </summary>
		public float time;

		private float timer;


		void Update ()
		{
			timer += Time.deltaTime;

			if (timer > time) {
				Destroy (gameObject);
			}
		}
	}
}