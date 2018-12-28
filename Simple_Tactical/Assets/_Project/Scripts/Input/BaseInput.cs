using UnityEngine;

namespace Game.GameInput
{
    /// <summary>
    /// Base Input class to handle diffenret kinds of devices and platforms
    /// </summary>
    public class BaseInput : MonoBehaviour
    {
        #region Common Input Variables

        public Touch[] touches;

        public enum Type
        {
            Desktop,
            Mobile,
            Console
        }

        public Type type;

        public Vector2 pos;

        public float force;

        public float maxmimumPressure;

        public float forceSpeed, forceDecreaseSpeed;

        #endregion

        /// <summary>
        /// Enable input checking
        /// </summary>
        public virtual void Init()
        {
            enabled = true;
            UnityEngine.Input.simulateMouseWithTouches = true;
            touches = UnityEngine.Input.touches;
        }

        /// <summary>
        /// Auto disable from Awake
        /// </summary>
        public virtual void Awake()
        {
            enabled = false;
        }

        /// <summary>
        /// Disable input checking
        /// </summary>
        public virtual void Stop()
        {
            enabled = false;
        }

        /// <summary>
        /// Restart input checking
        /// </summary>
        public virtual void Resume()
        {
            enabled = true;
        }

        /// <summary>
        /// Perform input checking
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Reset Input variables
        /// </summary>
        public virtual void Reset()
        {

        }

        public static BaseInput baseInput;

        /// <summ<summary>
        //ummary>
        /// Select correct input method based on platform
        /// </summary>
        public static BaseInput SelectInput(GameObject player)
        {
            MobileInput mobile;
            if (Application.isEditor)
            {
                mobile = player.GetComponentInChildren<MobileInput>();
                if (mobile.testingRemote)
                    baseInput = mobile;
                else
                    baseInput = player.GetComponentInChildren<DesktopInput>();
            }
            else if (Application.isMobilePlatform)
            {
                baseInput = player.GetComponentInChildren<MobileInput>();
            }
            else
            {
                baseInput = player.GetComponentInChildren<DesktopInput>();
            }
            if (baseInput != null)
                baseInput.Init();
            return baseInput;
        }

        public virtual bool WasClicked()
        {
            return false;
        }

        public virtual bool IsPressing()
        {
            return false;
        }

        public virtual float ClampedForce()
        {
            return force / maxmimumPressure;
        }
    }
}