using UnityEngine;

namespace Game.GameInput
{
    public class DesktopInput : BaseInput
    {

        bool pressing;

        public override void Init()
        {
            base.Init();

            type = Type.Desktop;
            touches = new Touch[1];
            touches[0] = new Touch();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Resume()
        {
            base.Resume();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        public override void Update()
        {
            base.Update();


            /*if(Input.mousePosition.x != position.x || Input.mousePosition.y != position.y)
            {
                if(state == TouchPhase.Canceled)
                    state = TouchPhase.Moved;
            }*/

            if (Input.GetMouseButtonDown(0))
            {
                touches[0].phase = TouchPhase.Began;
            }
            else if (Input.GetMouseButtonUp(0))
                touches[0].phase = TouchPhase.Ended;

            pos = Input.mousePosition;

            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftShift))
            {
                pressing = true;
                force += Time.deltaTime * forceSpeed;
                force = Mathf.Clamp(force, 0, maxmimumPressure);
            }
            else
            {
                pressing = false;
                force -= Time.deltaTime * forceDecreaseSpeed;
                force = Mathf.Clamp(force, 0, maxmimumPressure);
            }
        }


        public override bool IsClicking()
        {
            return Input.GetMouseButtonDown(0);
        }

        public override bool IsPressing()
        {
            return pressing;
        }
    }
}