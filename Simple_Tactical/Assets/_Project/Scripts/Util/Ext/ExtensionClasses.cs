//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Secret Weapon 
//

using UnityEngine;
using System.Collections;


namespace Game
{

    public static class MonoEx
    {

        public static Coroutine WaitForSecondsAndDo(this MonoBehaviour mono, float seconds, System.Action action)
        {
            return mono.StartCoroutine(IWaitForSecondsAndDo(seconds, action));
        }

        public static IEnumerator IWaitForSecondsAndDo(float seconds, System.Action action)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }

        public static void WaitForSecondsAndDo<T>(this MonoBehaviour mono, float seconds, System.Action<T> action, T paramater)
        {
            mono.StartCoroutine(IWaitForSecondsAndDo(seconds, action, paramater));
        }

        public static IEnumerator IWaitForSecondsAndDo<T>(float seconds, System.Action<T> action, T paramater)
        {
            yield return new WaitForSeconds(seconds);
            action(paramater);
        }



        public static void WaitForEndOfFrame(this MonoBehaviour mono, System.Action action)
        {
            mono.StartCoroutine(IWaitForFrame(action));
        }

        public static IEnumerator IWaitForFrame(System.Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
        }


        public static void StopCo(this MonoBehaviour mono)
        {
            mono.StopAllCoroutines();
        }

    }
}