//
// DebugUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public class DebugUI : BaseUI
	{
		public Text fps, version;

		string formatFPS;


		public void Setup(string p_version)
		{
			version.text = p_version;
		}

		void Update()
		{
			SetFPS(FPSCounter.FPS);
		}

		public void SetFPS(float p_fps)
		{
			formatFPS = string.Format("{0:F2} FPS", p_fps);
			fps.text = formatFPS;
		}

		public void SetVersion()
		{

		}

	}
}