//
// BaseController.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{

	public class BaseController : MonoBehaviour
	{
		
		protected virtual void Awake()
		{
			ListenEvents();
		}

		protected virtual void ListenEvents()
		{
		}
	}
}