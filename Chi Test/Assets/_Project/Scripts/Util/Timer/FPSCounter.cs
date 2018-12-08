using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Game
{

    /// <summary>
    /// It calculates frames/second over each updateInterval,
    /// so the display does not keep changing wildly.
    /// It is also fairly accurate at very low FPS counts (<10).
    /// We do this not by simply counting frames per interval, but
    /// by accumulating FPS for each frame. This way we end up with
    /// correct overall FPS even if the interval renders something like
    /// 5.5 frames.
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        public float updateInterval = 0.5F;

        // FPS accumulated over the interval
        private float accum = 0;

        // Frames drawn over the interval
        private int frames = 0;

        // Left time for current interval
        private float timeleft;

        public static float FPS;

        public Text text;

        void Start()
        {
            timeleft = updateInterval;
        }

        void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                FPS = accum / frames;

                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;

                if (text)
                    text.text = FPS.ToString();

            }
        }
    }
}