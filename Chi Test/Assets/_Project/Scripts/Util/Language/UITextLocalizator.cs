using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game
{

    public class UITextLocalizator : MonoBehaviour
    {

        /// <summary>
        /// Localization key.
        /// </summary>
        public string key;

        /// <summary>
        /// Manually change the value of whatever the localization component is attached to.
        /// </summary>
        public string value
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Text text = GetComponent<Text>();

                    if (text != null)
                    {

                        text.text = value;
#if UNITY_EDITOR
						if (!Application.isPlaying) EditorUtility.SetDirty (text);
#endif
                    }
                }
            }
        }

        bool mStarted = false;

        /// <summary>
        /// Localize the widget on enable, but only if it has been started already.
        /// </summary>

        void OnEnable()
        {
#if UNITY_EDITOR
			if (!Application.isPlaying) return;
#endif
            if (mStarted) OnLocalize();
        }

        /// <summary>
        /// Localize the widget on start.
        /// </summary>

        void Start()
        {
#if UNITY_EDITOR
			if (!Application.isPlaying) return;
#endif
            mStarted = true;
            OnLocalize();
        }

        /// <summary>
        /// This function is called by the Localization manager via a broadcast SendMessage.
        /// </summary>

        void OnLocalize()
        {
            // If no localization key has been specified, use the label's text as the key
            if (string.IsNullOrEmpty(key))
            {
                Text text = GetComponent<Text>();
                if (text != null) key = text.text;
            }

            // If we still don't have a key, leave the value as blank
            if (!string.IsNullOrEmpty(key)) value = Localization.Get(key);
        }

    }
}