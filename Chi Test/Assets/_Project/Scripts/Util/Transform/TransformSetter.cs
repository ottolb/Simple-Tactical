//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games 
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
	public static class TransformSetter
	{

		#region Position

		static public void SetPositionX (this Transform p_tr, float x)
		{
			Vector3 pos = p_tr.position;
			pos.x = x;
			p_tr.position = pos;
		}

		static public void SetPositionY (this Transform p_tr, float y)
		{
			Vector3 pos = p_tr.position;
			pos.y = y;
			p_tr.position = pos;
		}

		static public void SetPositionZ (this Transform p_tr, float z)
		{
			Vector3 pos = p_tr.position;
			pos.z = z;
			p_tr.position = pos;
		}

		static public void SetLocalPositionX (this Transform p_tr, float x)
		{
			Vector3 pos = p_tr.localPosition;
			pos.x = x;
			p_tr.localPosition = pos;
		}

		static public void SetLocalPositionY (this Transform p_tr, float y)
		{
			Vector3 pos = p_tr.localPosition;
			pos.y = y;
			p_tr.localPosition = pos;
		}

		static public void SetLocalPositionZ (this Transform p_tr, float z)
		{
			Vector3 pos = p_tr.localPosition;
			pos.z = z;
			p_tr.localPosition = pos;
		}


		#endregion

		#region Rotation

		static public void SetRotationX (this Transform p_tr, float x)
		{
			Vector3 rot = p_tr.localEulerAngles;
			rot.x = x;
			p_tr.localEulerAngles = rot;
		}

		static public void SetRotationY (this Transform p_tr, float y)
		{
			Vector3 rot = p_tr.localEulerAngles;
			rot.y = y;
			p_tr.localEulerAngles = rot;
		}

		static public void SetRotationZ (this Transform p_tr, float z)
		{
			Vector3 rot = p_tr.localEulerAngles;
			rot.z = z;
			p_tr.localEulerAngles = rot;
		}

		#endregion

		static public void SetScaleX (this Transform p_tr, float x)
		{
			Vector3 scale = p_tr.localScale;
			scale.x = x;
			p_tr.localScale = scale;
		}

		static public void SetScaleY (this Transform p_tr, float y)
		{
			Vector3 scale = p_tr.localScale;
			scale.y = y;
			p_tr.localScale = scale;
		}

		static public void SetScaleZ (this Transform p_tr, float z)
		{
			Vector3 scale = p_tr.localScale;
			scale.z = z;
			p_tr.localScale = scale;
		}

		static public List<Transform> GetChilds (this Transform p_tr)
		{
			List<Transform> childs = new List<Transform> ();
			foreach (Transform child in p_tr) {
				/*Debug.Log(child.name);
				if(child != p_tr)*/
				childs.Add (child);
			}
			return childs;
		}

		static public List<GameObject> GetChilds (this GameObject p_tr)
		{
			List<GameObject> childs = new List<GameObject> ();
			for (int i = 0; i < p_tr.transform.childCount; i++) {
				childs.Add (p_tr.transform.GetChild (i).gameObject);
			}

			return childs;
		}

		static public void DestoyChilds (this GameObject p_tr)
		{
			while (p_tr.transform.childCount > 0) {
				UnityEngine.Object.Destroy (p_tr.transform.GetChild (0).gameObject);
			}
		}
	}
}