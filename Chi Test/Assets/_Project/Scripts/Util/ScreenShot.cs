//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games 
//
using UnityEngine;

namespace Game
{
	public class ScreenShot : MonoBehaviour
	{
		// The folder we place all screenshots inside.
		// If the folder exists we will append numbers to create an empty folder.
		public string folder = "Screenshots";

		public int frameRate = 30;

		public int size = 1;

		public bool startRecording = false;

		private string realFolder = "";

		bool recording = false;

		int frameCount;

		public KeyCode keyCode;

		void Start()
		{
			frameCount = PlayerPrefs.GetInt("frameCount", 0);
			if(startRecording)
				StartRecording();
		}

		void StartRecording()
		{
			// Set the playback framerate!
			// (real time doesn't influence time anymore)
			Time.captureFramerate = frameRate;

			// Find a folder that doesn't exist yet by appending numbers!
			realFolder = folder;

			// Create the folder
			if(!System.IO.Directory.Exists(realFolder))
			{
				System.IO.Directory.CreateDirectory(realFolder);
				frameCount = 0;
			}

			recording = true;
		}

		void StopRecording()
		{
			recording = false;
			Time.captureFramerate = 0;
		}

		void Update()
		{
			if(Input.GetKeyDown(keyCode))
			{
				if(!recording)
					StartRecording();
				else
					StopRecording();
			}
			if(recording)
			{
				// name is "realFolder/0005 shot.png"
				var name = string.Format("{0}/{1:D04} shot.png", realFolder, frameCount++);

				PlayerPrefs.SetInt("frameCount", frameCount);
#if UNITY_EDITOR
				// Capture the screenshot
				ScreenCapture.CaptureScreenshot(name, size);
#endif
				StopRecording();
			}
		}
	}

}