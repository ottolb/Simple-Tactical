//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Secret Weapon 
//

using UnityEngine;
using System.Collections;
using System.IO;

namespace Game
{

    public class CacheManager : MonoBehaviour
    {

        static private CacheManager _Instance;

        static public CacheManager Instance
        {
            get
            {
                return _Instance;
            }
        }

        private string appDataPath;


        // Use this for initialization
        void Awake()
        {
            _Instance = this;
            appDataPath = Application.persistentDataPath + "/";
            CreateDirectory();
        }

        public string LoadTextFile(string p_file, bool p_secure = false)
        {
            string filePath = appDataPath + p_file;
            if (!File.Exists(filePath))
                return null;
            string text;

            if (p_secure)
            {
                string encryptedValue = File.ReadAllText(filePath);
                var desEncryption = new DESEncryption();
                desEncryption.TryDecrypt(encryptedValue, "ksjhbdwier123&*(", out text);
            }
            else
                text = File.ReadAllText(filePath);

            return text;
        }

        public static string GetResourcesJson(string p_file, bool p_secure = false)
        {
            string text;
            TextAsset asset = Resources.Load<TextAsset>(p_file);
            if (asset == null)
                return null;
            if (p_secure)
            {
                string encryptedValue = asset.text;
                var desEncryption = new DESEncryption();
                desEncryption.TryDecrypt(encryptedValue, "ksjhbdwier123&*(", out text);
            }
            else
                text = asset.text;

            return text;
        }

        public void SaveTextFile(string p_file, string p_json, bool p_secure = false)
        {
            string filePath = appDataPath + p_file;
            if (p_secure)
            {
                var desEncryption = new DESEncryption();
                string encryptedValue = desEncryption.Encrypt(p_json, "ksjhbdwier123&*(");
                File.WriteAllText(filePath, encryptedValue);
            }
            else
                File.WriteAllText(filePath, p_json);
        }

        public static void SaveJsonResource(string file, string json, bool noSecure = true)
        {
            string filePath = "Assets/Resources/" + file;
            if (!noSecure)
            {
                var desEncryption = new DESEncryption();
                string encryptedValue = desEncryption.Encrypt(json, "ksjhbdwier123&*(");
                File.WriteAllText(filePath, encryptedValue);
            }
            else
                File.WriteAllText(filePath, json);

            //Debug.Log("Cache " + filePath);
        }

        /*public Texture2D GetImage(string file)
		{

			string filePath = appDataPath + file;
			if(!File.Exists(filePath))
				return null;

			byte[] bytes = File.ReadAllBytes(filePath);

			Texture2D tex = new Texture2D(2, 2, TextureFormat.RGB24, false);
			if(!tex.LoadImage(bytes))
				return null;

			return tex;
		}*/

        public void SaveImage(string file, byte[] bytes)
        {
            string filePath = appDataPath + file;
            File.WriteAllBytes(filePath, bytes);
        }

        public void CreateDirectory()
        {
            string filePath = appDataPath;
            //Debug.LogWarning(filePath);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        public void Clean()
        {
            string path = appDataPath;
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}