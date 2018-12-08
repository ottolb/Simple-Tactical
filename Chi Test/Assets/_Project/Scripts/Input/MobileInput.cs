using UnityEngine;

namespace Game.GameInput
{
    /// <summary>
    /// Handle Mobile input for iOS and Android
    /// </summary>/
    public class MobileInput : BaseInput
    {
        public bool testingRemote;
        bool pressureSupported;
        bool pressing;

        public override void Init()
        {
            base.Init();

            type = Type.Mobile;

            touches = Input.touches;

            Debug.Log("Input.touchPressureSupported  " + Input.touchPressureSupported);
            pressureSupported = Input.touchPressureSupported;
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Resume()
        {
            base.Resume();
        }

        public override void Update()
        {
            base.Update();

            touches = Input.touches;
            if (Input.touchCount > 0)
            {
                pos = touches[0].position;

                if (pressureSupported)
                {
                    force = touches[0].pressure;
                    force = Mathf.Clamp(force, 0, maxmimumPressure);
                    //Debug.Log("force " + force);
                    pressing = touches[0].phase == TouchPhase.Stationary ||
                             touches[0].phase == TouchPhase.Moved;
                }
                else
                {
                    pressing = CheckPress();
                }
            }
            else
                pressing = CheckPress();
        }

        public override bool IsClicking()
        {
            if (touches.Length == 0)
                return false;

            return touches[0].phase == TouchPhase.Began;
        }

        public override bool IsPressing()
        {
            return pressing;
        }

        bool CheckPress()
        {
            if (touches.Length == 0)
            {
                force -= Time.deltaTime * forceDecreaseSpeed;
                force = Mathf.Clamp(force, 0, maxmimumPressure);

                return false;
            }
            else
            {
                force += Time.deltaTime * forceSpeed;
                force = Mathf.Clamp(force, 0, maxmimumPressure);
            }

            return touches[0].phase == TouchPhase.Stationary ||
                             touches[0].phase == TouchPhase.Moved;
        }
    }
}