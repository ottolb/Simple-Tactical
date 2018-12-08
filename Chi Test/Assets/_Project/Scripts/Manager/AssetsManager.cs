//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games 
//
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Controls access to the assets that have been loaded in this scene and all previous scene.
    /// </summary>
    public class AssetsManager : MonoBehaviour
    {
        #region Static Data

        /// <summary>
        /// Dictionary that holds all assets that have been loaded.
        /// </summary>
        static private Dictionary<string, Texture2D> _textures;

        static private Dictionary<string, AudioClip> _audios;

        static private Dictionary<string, GameObject> _prefabs;

        static private Dictionary<string, Material> _materials;

        static private Dictionary<string, Sprite> _sprites;

        public static AssetsManager Instance;

        #endregion

        #region Public Data

        /// <summary>
        /// Inspector arrays.
        /// </summary>
        public Texture2D[] textures;

        public AudioClip[] audios;

        public GameObject[] prefabs;

        public Material[] materials;

        public Sprite[] sprites;

        public Color[] colors;

        #endregion


        #region Initialize Dictionarys

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            InitializeTextures();
            InitializeAudios();
            InitializeParticles();
            InitializeSprites();
            InitializeMaterials();
            Instance = this;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTextures()
        {
            if (textures == null)
                return;

            _textures = new Dictionary<string, Texture2D>();
            string assetName;
            for (int i = 0; i < textures.Length; i++)
            {
                if (textures[i] == null)
                    continue;

                assetName = textures[i].name.ToLower();

                if (_textures.ContainsKey(assetName))
                    continue;

                _textures.Add(assetName, textures[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeAudios()
        {
            if (audios == null)
                return;

            _audios = new Dictionary<string, AudioClip>();
            string audioName;
            for (int i = 0; i < audios.Length; i++)
            {
                if (audios[i] == null)
                    continue;

                audioName = audios[i].name.ToLower();

                if (_audios.ContainsKey(audioName))
                    continue;

                _audios.Add(audioName, audios[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeParticles()
        {
            if (prefabs == null)
                return;

            _prefabs = new Dictionary<string, GameObject>();
            string fontName;
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i] == null)
                    continue;

                fontName = prefabs[i].name.ToLower();

                if (_prefabs.ContainsKey(fontName))
                    continue;

                _prefabs.Add(fontName, prefabs[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSprites()
        {
            if (sprites == null)
                return;

            _sprites = new Dictionary<string, Sprite>();
            string assetName;
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] == null)
                    continue;

                assetName = sprites[i].name.ToLower();

                if (_sprites.ContainsKey(assetName))
                    continue;

                _sprites.Add(assetName, sprites[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeMaterials()
        {
            if (materials == null)
                return;

            _materials = new Dictionary<string, Material>();
            string assetName;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == null)
                    continue;

                assetName = materials[i].name.ToLower();

                if (_materials.ContainsKey(assetName))
                    continue;

                _materials.Add(assetName, materials[i]);
            }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        static public Texture2D GetTexture(string p_assetName)
        {
            p_assetName = p_assetName.ToLower();

            if (_textures == null || !_textures.ContainsKey(p_assetName))
            {
                Debug.LogError("Texture \"" + p_assetName + "\" not found!");
                return new Texture2D(2, 2);
            }

            return _textures[p_assetName];
        }

        /// <summary>
        /// 
        /// </summary>
        static public Texture2D CreateTexture(Vector2 p_size, Color p_color)
        {
            Texture2D texture = new Texture2D((int)p_size.x, (int)p_size.y);

            Color[] colorTexture = new Color[texture.width * texture.height];
            for (int i = 0; i < colorTexture.Length; i++)
                colorTexture[i] = p_color;

            texture.SetPixels(colorTexture);
            texture.Apply();

            return texture;
        }


        /// <summary>
        /// 
        /// </summary>
        static public AudioClip GetAudioClip(string p_audioName)
        {
            p_audioName = p_audioName.ToLower();

            if (_audios == null || !_audios.ContainsKey(p_audioName))
            {
                Debug.LogError("AudioClip \"" + p_audioName + "\" not found!");
                //return new AudioClip();
            }

            return _audios[p_audioName];
        }

        /// <summary>
        /// 
        /// </summary>
        static public GameObject GetPrefabs(string p_prefabName)
        {
            p_prefabName = p_prefabName.ToLower();

            if (_prefabs == null || !_prefabs.ContainsKey(p_prefabName))
            {
                //Debug.LogError("Particle \"" + p_particleName + "\" not found!");
                return null;
            }

            return _prefabs[p_prefabName];
        }

        /// <summary>
        /// 
        /// </summary>
        static public Sprite GetSprite(string p_spriteName)
        {
            p_spriteName = p_spriteName.ToLower();

            if (_sprites == null || !_sprites.ContainsKey(p_spriteName))
            {
                Debug.LogError("Sprite \"" + p_spriteName + "\" not found!");
                return null;
            }

            return _sprites[p_spriteName];
        }

        /// <summary>
        /// 
        /// </summary>
        static public Material GetMaterial(string p_materialName)
        {
            p_materialName = p_materialName.ToLower();

            if (_materials == null || !_materials.ContainsKey(p_materialName))
            {
                Debug.LogError("Sprite \"" + p_materialName + "\" not found!");
                return null;
            }

            return _materials[p_materialName];
        }
    }
}