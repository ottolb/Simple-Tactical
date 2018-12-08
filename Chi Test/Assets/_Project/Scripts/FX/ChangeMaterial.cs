using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game
{
    public class ChangeMaterial : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        public enum Type
        {
            Piece,
            Tower,
            Background
        }

        public Type type;

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            switch (type)
            {
                case Type.Background:
                    EventManager.StartListening(N.Level.ChangeBackgroundMaterial, ChangeMtl);
                    break;
                case Type.Piece:
                    EventManager.StartListening(N.Level.ChangePieceMaterial, ChangeMtl);
                    break;
                case Type.Tower:
                    EventManager.StartListening(N.Level.ChangeTowerMaterial, ChangeMtl);
                    break;

            }

        }

        public void ChangeMtl(object p_mtl)
        {
            ChangeMtl((Material)p_mtl);
        }

        public void ChangeMtl(Material p_mtl)
        {
            meshRenderer.material = p_mtl;
        }
    }
}