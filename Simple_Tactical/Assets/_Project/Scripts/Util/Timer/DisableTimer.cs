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
	/// Simple component to disable game object after some time
	/// </summary>
	public class DisableTimer : MonoBehaviour
	{

		/// <summary>
		/// The time to destroy object 
		/// </summary>
		public float time;

		private float timer;

		public void OnEnable ()
		{
			timer = 0;
		}

		public void StartTimer (float p_time)
		{
			time = p_time;
			StartTimer ();
		}

		public void StartTimer ()
		{
			timer = 0;
			enabled = true;
		}

		void Update ()
		{
			timer += Time.deltaTime;

			if (timer > time) {
				gameObject.SetActive (false);
			}
		}
	}
}