using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Level
{

    public class LevelSetup : ScriptableObject
    {
        public List<GameObject> prefabs;

        public Material cylinderMtl;

        public Material piecesMtl;

        public Material backgroundMtl;
    }
}
