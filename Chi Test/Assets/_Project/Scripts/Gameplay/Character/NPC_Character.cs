using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;
using DG.Tweening;


namespace Game.Gameplay
{
    public class NPC_Character : BaseCharacter
    {


        protected override void Awake()
        {
            base.Awake();

        }

        public override void Init()
        {
            base.Init();
            canMove = true;

        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as Transform == transform)
            {


            }
            else
            {

            }
        }

        void OnHoverUnit(object p_data)
        {
            if (p_data as Transform == transform)
            {
                //Debug.Log("Color ");
            }
            else
            {
                //Debug.Log("Color normal");
            }
        }

        void OnMoveUnit(object p_data)
        {
            //if (canMove)
            //{
            //    if (hasMoveEnergy)
            //    {
            //        Vector3 position = (Vector3)p_data;
            //        _navMeshAgent.SetDestination(position);
            //        canMove = false;
            //        moveAreaParticle.SetActive(false);
            //    }
            //}
        }

        void OnCheckUnitMovement(object p_data)
        {
            Vector3 position = (Vector3)p_data;
            bool aux = HasMoveEnergy(position);

        }
    }
}