using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class ButtonView : MonoBehaviour
    {
        [Tooltip("The method name of the Base Canvas script that contains this panel")]
        public string methodName;

        [HideInInspector]
        public UIButtonViewEvent evt;


        void Awake()
        {
            if (string.IsNullOrEmpty(methodName))
                return;

            Button button = GetComponent<Button>();

            UnityAction action = new UnityAction(OnClicked);

            //add to button event listener
            button.onClick.AddListener(action);
        }


        public void OnClicked()
        {
            if (!enabled)
                return;
            if (Debug.isDebugBuild)
                Debug.Log("#UI# OnClicked: " + this.name);
            evt.Invoke(this);

            AudioController.Play("Click");
        }
    }

    [System.Serializable]
    public class UIButtonViewEvent : UnityEvent<ButtonView>
    {
    }
}