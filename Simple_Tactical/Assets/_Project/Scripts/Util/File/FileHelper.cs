//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games 
//
using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace Game
{

	public class FileHelper : MonoBehaviour
	{

#if !UNITY_WINRT

		public static byte [] ReadAllBytes (string p_filename)
		{
			byte [] bytes = null;
			try {
				using (FileStream fs = File.OpenRead (p_filename)) {
					bytes = new byte [fs.Length];

					int numBytesToRead = (int)fs.Length;
					int numBytesRead = 0;
					while (numBytesToRead > 0) {
						// Read may return anything from 0 to numBytesToRead.
						int n = fs.Read (bytes, numBytesRead, numBytesToRead);

						// Break when the end of the file is reached.
						if (n == 0)
							break;

						numBytesRead += n;
						numBytesToRead -= n;
					}
				}
			} catch (FileNotFoundException ex) {
				Debug.Log (ex.Message);
			}
			return bytes;
		}


		public static bool WriteAllBytes (string p_filename, byte [] bytes)
		{
			try {
				// Write the byte array to the other FileStream.
				using (FileStream fsNew = new FileStream (p_filename, FileMode.Create, FileAccess.Write)) {
					fsNew.Write (bytes, 0, bytes.Length);
				}
				return true;
			} catch (FileNotFoundException ex) {
				Debug.Log (ex.Message);
				return false;
			}
		}


		public static string LoadText (string path)
		{
			string stateStr = null;
			if (File.Exists (path)) {
				using (FileStream fs = File.Open (path, FileMode.Open, FileAccess.Read, FileShare.None)) {
					using (StreamReader fsr = new StreamReader (fs)) {
						stateStr = fsr.ReadToEnd ();
					}


				}
			}
			return stateStr;
		}

		public static bool SaveText (string path, string text)
		{
			try {
				using (FileStream fs = File.Open (path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)) {
					using (StreamWriter fsw = new StreamWriter (fs)) {
						fsw.AutoFlush = true;
						fsw.Write (text);
					}

					return true;
				}
			} catch (Exception ex) {
				Debug.Log ("'" + path + "' failed to save; " + ex.Message);
			}

			return false;
		}
#endif
	}
}