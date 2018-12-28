using UnityEngine;

namespace Game
{
	public class GoActivator : MonoBehaviour
	{
		public GameObject [] goList;

		void OnEnable ()
		{
			Toggle (true);
		}

		void OnDisable ()
		{
			Toggle (false);
		}

		void Toggle (bool p_toggle)
		{
			foreach (var go in goList) {
				go.SetActive (p_toggle);
			}
		}
	}
}