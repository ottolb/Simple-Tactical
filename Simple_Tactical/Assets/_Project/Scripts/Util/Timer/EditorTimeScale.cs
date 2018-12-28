//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//

using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	/// <summary>
	/// Control editor time scale
	/// </summary>
	public class EditorTimeScale : MonoBehaviour
	{


		// Update is called once per frame
		void Update ()
		{

			if (Application.isEditor)
				ChangeTime ();

		}

		void ChangeTime ()
		{
			if (!Input.anyKey)
				return;
			if (Input.GetKeyDown (KeyCode.Alpha1))
				Time.timeScale = 0.1f;
			if (Input.GetKeyDown (KeyCode.Alpha2))
				Time.timeScale = 0.3f;
			if (Input.GetKeyDown (KeyCode.Alpha3))
				Time.timeScale = 0.45f;
			if (Input.GetKeyDown (KeyCode.Alpha4))
				Time.timeScale = 0.65f;
			if (Input.GetKeyDown (KeyCode.Alpha5))
				Time.timeScale = 1.0f;
			if (Input.GetKeyDown (KeyCode.Alpha6))
				Time.timeScale = 1.5f;
			if (Input.GetKeyDown (KeyCode.Alpha7))
				Time.timeScale = 2.0f;
			if (Input.GetKeyDown (KeyCode.Alpha8))
				Time.timeScale = 3.0f;
			if (Input.GetKeyDown (KeyCode.Alpha9))
				Time.timeScale = 5.0f;
		}

		public void Increase ()
		{
			Time.timeScale += 0.05f;
			Time.timeScale = Mathf.Clamp (Time.timeScale, 0.01f, 5);
		}

		public void Decrease ()
		{
			Time.timeScale -= 0.05f;
			Time.timeScale = Mathf.Clamp (Time.timeScale, 0.01f, 5);
		}


	}
}