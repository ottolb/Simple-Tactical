using UnityEngine;

namespace Game.Gameplay
{

    public class Spawner : MonoBehaviour
    {
        public float spawnRate;

        public GameObject prefab;

        public Transform spawnPoint;

        float timer;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer < spawnRate)
                return;
            timer = 0;

            Spawn();
        }

        void Spawn()
        {
            GameObject go = CFX_SpawnSystem.GetNextObject(prefab);
            go.transform.position = spawnPoint.position;
        }
    }
}