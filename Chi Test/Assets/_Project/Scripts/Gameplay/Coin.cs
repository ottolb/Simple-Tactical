using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game
{
    public class Coin : MonoBehaviour
    {

        public GameObject pickFX;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter()
        {
            EventManager.TriggerEvent(N.Score.CoinPick, transform);
            this.gameObject.SetActive(false);


            GameObject go = CFX_SpawnSystem.GetNextObject(pickFX);
            go.transform.position = transform.position;


            AudioController.Play("star");
        }

    }
}