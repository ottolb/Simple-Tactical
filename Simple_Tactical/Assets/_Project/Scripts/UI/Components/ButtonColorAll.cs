using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Game.UI
{
	public class ButtonColorAll : Button
	{

		Graphic[] graphics;

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			Color color;
			switch(state)
			{
			case Selectable.SelectionState.Normal:
				color = this.colors.normalColor;
				break;
			case Selectable.SelectionState.Highlighted:
				color = this.colors.highlightedColor;
				break;
			case Selectable.SelectionState.Pressed:
				color = this.colors.pressedColor;
				break;
			case Selectable.SelectionState.Disabled:
				color = this.colors.disabledColor;
				break;
			default:
				color = Color.black;
				break;
			}
			if(base.gameObject.activeInHierarchy)
			{
				switch(this.transition)
				{
				case Selectable.Transition.ColorTint:
					ColorTween(color * this.colors.colorMultiplier, instant);
					break;
				}
			}
		}

		private void ColorTween(Color targetColor, bool instant)
		{
			if(this.targetGraphic == null)
			{
				this.targetGraphic = this.image;
			}
			if(graphics == null)
			{
				graphics = GetComponentsInChildren<Graphic>();
			}
			if(graphics == null || graphics.Length == 0)
				return;

			foreach(var graphic in graphics)
			{
				graphic.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
			}
			//base.image.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);


		}
	}
}