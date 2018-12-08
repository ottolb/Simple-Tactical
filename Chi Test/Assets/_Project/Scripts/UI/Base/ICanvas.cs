//
// ICanvas.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


namespace Game.UI
{
	public interface ICanvas
	{
		/// Show canvas
		void Show(bool p_animated = true);

		/// Hide canvas
		void Hide(bool p_animated = true);
	}
}