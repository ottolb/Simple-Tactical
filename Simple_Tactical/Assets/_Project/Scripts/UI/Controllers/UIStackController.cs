using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
	public class UIStackController : MonoBehaviour
	{
		public static UIStackController Instance;

		public BaseUI current;

		public Stack<BaseUI> active;
		public List<BaseUI> listUI;


		protected virtual void Awake()
		{
			Instance = this;
			ListenEvents();

			listUI = new List<BaseUI>();
			active = new Stack<BaseUI>();
		}

		protected void ListenEvents()
		{
		}


		public void UIShow(BaseUI p_ui)
		{
			if(!active.Contains(p_ui))
			{
				active.Push(p_ui);
				listUI.Add(p_ui);
				current = Current;
			}
			else
			{
				Debug.LogWarning("Screen " + p_ui.name + " already in stack");
				ResetList();
				if(!active.Contains(p_ui))
				{
					active.Push(p_ui);
					listUI.Add(p_ui);
				}
				current = Current;
			}
		}

		public void BackToPrevious(BaseUI p_ui)
		{
			p_ui.Hide(false);

			if(active.Contains(p_ui))
			{
				//active.Pop();
				listUI.Remove(active.Pop());
				if(Current)
					Current.Show(false);
			}
			else
			{
				Debug.LogError("Screen " + p_ui.name + " not in stack");
			}
			current = Current;
		}

		public void BackToPrevious()
		{
			if(Current)
				Current.Show(false);
		}

		public void ResetList()
		{
			BaseUI home = listUI[0];
			active.Clear();
			active.Push(home);
			listUI.Clear();
			listUI.Add(home);
		}

		public BaseUI Previous
		{
			get
			{
				active.Pop();
				return active.Peek();
			}
		}

		public BaseUI Current
		{
			get
			{
				if(active.Count > 0)
					return active.Peek();
				else return null;
			}
		}
	}
}