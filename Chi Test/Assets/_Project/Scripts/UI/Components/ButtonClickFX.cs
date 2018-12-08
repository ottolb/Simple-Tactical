using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    public class ButtonClickFX : MonoBehaviour
    {
        Button _button;
        //public PlayFXTween fx;

        // Use this for initialization
        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked);

            //if (fx == null)
            //fx = GetComponentInChildren<PlayFXTween> ();
        }

        void OnClicked()
        {
            //fx.PlayFX();
        }

    }
}
