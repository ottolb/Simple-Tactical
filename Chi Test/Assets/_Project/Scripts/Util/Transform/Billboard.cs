//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games 
//

using UnityEngine;
using System.Collections;

namespace Game
{

    /// <summary>
    /// Always face camera
    /// </summary>
    [ExecuteInEditMode]
    public class Billboard : MonoBehaviour
    {

        public enum AimVector
        {
            FORWARD,
            TOP,
            LEFT,
        }


        #region Public Data

        public AimVector aimVector;
        public Camera cameraToLook;

        #endregion

        #region Private Data

        private Transform _transform;

        #endregion

        void Start()
        {
            _transform = transform;
        }

        public virtual void Awake()
        {
            if (cameraToLook == null)
                cameraToLook = Camera.main;
        }

        void OnEnable()
        {
            if (cameraToLook == null)
                cameraToLook = Camera.main;
        }

        public virtual void Update()
        {
            if (cameraToLook == null)
            {
                return;
            }
            switch (aimVector)
            {
                case AimVector.FORWARD:
                    _transform.forward = cameraToLook.transform.forward;
                    break;
                case AimVector.TOP:
                    _transform.up = cameraToLook.transform.position - _transform.position;
                    break;
                case AimVector.LEFT:
                    _transform.right = -(cameraToLook.transform.position - _transform.position);
                    break;
                default:
                    break;
            }

        }
    }
}