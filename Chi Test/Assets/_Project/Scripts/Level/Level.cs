using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.Gameplay;
using UnityEngine;

namespace Game.Level
{

    public class Level : MonoBehaviour
    {

        private List<Transform> blockSpawnPoints;
        private List<Transform> blockAvailablePoints;

        private List<Transform> hazardSpawnPoints;
        private List<Transform> hazardAvailablePoints;



        public GameObject block, hazard, moreBlocksPW;

        float minHazardScale, maxHazardScale;

        public GameObject extraBlockSpawnPrefab;

        // Use this for initialization
        void Start()
        {
            blockSpawnPoints = GameObject.Find("Spawn Points").transform.GetChilds();
            hazardSpawnPoints = GameObject.Find("Scenario/Hazard Spawn Points").transform.GetChilds();
        }

        public void Load(float p_blocks, float p_hazards)
        {
            Debug.LogFormat("#Level# Blocks {0}   Hazards {1}", p_blocks, p_hazards);
            Clean();

            FillBlocks(Mathf.RoundToInt(p_blocks));
            FillHazards(Mathf.RoundToInt(p_hazards));
            //EventManager.TriggerEvent(N.Level.ChangeBackgroundMaterial, setup.backgroundMtl);
        }

        void FillBlocks(int p_amount)
        {
            for (int i = 0; i < p_amount; i++)
            {
                var go = Spawn(block, blockAvailablePoints[0].position);
                go.transform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

                blockAvailablePoints.RemoveAt(0);
            }
        }

        void FillHazards(int p_amount)
        {
            for (int i = 0; i < p_amount; i++)
            {
                var go = Spawn(hazard, hazardAvailablePoints[0].position);
                go.transform.parent = GameObject.Find("Scenario").transform;
                go.transform.SetPositionY(1.6011f);
                go.transform.rotation = hazardAvailablePoints[0].transform.rotation;
                go.transform.SetScaleZ(minHazardScale + Random.Range(minHazardScale, maxHazardScale));

                hazardAvailablePoints.RemoveAt(0);
            }
        }

        public void SpawnPowerups(float p_hazards)
        {
            Debug.LogFormat("#Level# SpawnPowerups {0} ", p_hazards);
            for (int i = 0; i < p_hazards; i++)
            {
                var go = Spawn(moreBlocksPW, blockAvailablePoints[0].position);
                go.transform.SetPositionY(2.0f);
                blockAvailablePoints.RemoveAt(0);
            }
        }

        public void SpawnExraBlock()
        {
            var go = Spawn(block, blockAvailablePoints[0].position);
            go.transform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            go.GetComponent<BaseBlock>().CreatedOnMatch(1f);


            GameObject particle = CFX_SpawnSystem.GetNextObject(extraBlockSpawnPrefab);
            particle.transform.position = blockAvailablePoints[0].position;

            blockAvailablePoints.RemoveAt(0);
        }

        GameObject Spawn(GameObject prefab, Vector3 position)
        {
            GameObject go = CFX_SpawnSystem.GetNextObject(prefab);
            go.transform.position = position;
            return go;
        }

        public void SetHazardScale(float p_minHazardScale, float p_maxHazardScale)
        {
            minHazardScale = p_minHazardScale;
            maxHazardScale = p_maxHazardScale;
        }

        void Clean()
        {
            blockAvailablePoints = new List<Transform>(blockSpawnPoints);
            blockAvailablePoints = ConversorUtil.Randomize(blockAvailablePoints);

            hazardAvailablePoints = new List<Transform>(hazardSpawnPoints);
            hazardAvailablePoints = ConversorUtil.Randomize(hazardAvailablePoints);
        }
    }
}