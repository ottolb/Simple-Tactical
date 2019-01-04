using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.Gameplay;
using UnityEngine;

namespace Game.Level
{

    public class Level : MonoBehaviour
    {

        private List<Transform> playerSpawnPoints;
        private List<Transform> playerAvailablePoints;

        private List<Transform> enemySpawnPoints;
        private List<Transform> enemyAvailablePoints;


        void Start()
        {
            EventManager.StartListening(N.Level.RequestPlayerSP, OnRequestPlayerSpawnPoint);
            EventManager.StartListening(N.Level.RequestNPC_SP, OnRequestNPCSpawnPoint);
        }

        public void Load()
        {
            playerSpawnPoints = GameObject.Find("Player Spawn Points").transform.GetChilds();
            enemySpawnPoints = GameObject.Find("NPC Spawn Points").transform.GetChilds();

            Clean();
        }

        void OnRequestPlayerSpawnPoint(object p_data)
        {
            EventManager.TriggerEvent(N.Level.SetPlayerSP, GetSPTransform(playerAvailablePoints));
        }

        void OnRequestNPCSpawnPoint(object p_data)
        {
            EventManager.TriggerEvent(N.Level.SetNPC_SP, GetSPTransform(enemyAvailablePoints));
        }

        /// <summary>
        /// Get the first available spawn point
        /// </summary>
        /// <returns>The spawn point transform.</returns>
        Transform GetSPTransform(List<Transform> p_points)
        {
            Transform p = p_points[0];
            p_points.RemoveAt(0);
            return p;
        }

        void Clean()
        {
            //Random spawn points
            playerAvailablePoints = new List<Transform>(playerSpawnPoints);
            playerAvailablePoints = ConversorUtil.Randomize(playerAvailablePoints);

            enemyAvailablePoints = new List<Transform>(enemySpawnPoints);
            enemyAvailablePoints = ConversorUtil.Randomize(enemyAvailablePoints);
        }
    }
}