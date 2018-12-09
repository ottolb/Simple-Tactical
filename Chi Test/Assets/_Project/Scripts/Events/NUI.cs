//
// NUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//

namespace Game.Event
{

    /// <summary>
    /// Class that contains UI notifications string for the whole application.
    /// </summary>
    public class NUI
    {
        public class Home
        {
            public const string Close = "Home.Back";
            public const string Play = "Home.Play";
            public const string Continue = "Home.Continue";
            public const string ShowTutorial = "Home.ShowTutorial";
            public const string ShowLeaderboards = "Home.ShowLeaderboards";

        }

        public class EndGame
        {
            public const string Restart = "EndGame.Restart";
            public const string LevelSlider = "EndGame.LevelSlider";
        }

        public class HUD
        {
            public const string PlayerTurn = "HUD.PlayerTurn";
            public const string Blocks = "HUD.Blocks";
            public const string MoreBlocks = "HUD.MoreBlocks";
            public const string SetActionButton = "HUD.SetActionButton";
            public const string EndTurn = "HUD.EndTurn";
            public const string WaitAction = "HUD.WaitAction";
        }

        /// <summary>
        /// Loading notifications
        /// </summary>
        public class Loading
        {
            public const string Show = "Loading.Show";
            public const string Hide = "Loading.Hide";
            public const string UploadProgress = "Loading.UploadProgress";
        }

        /// <summary>
        /// Error notifications
        /// </summary>
        public class Error
        {
            public const string Show = "Error.Show";
            public const string Hide = "Error.Hide";
        }

        /// <summary>
        /// Message notifications
        /// </summary>
        public class Message
        {
            public const string Show = "Message.Show";
            public const string Hide = "Message.Hide";
        }

        public class Debug
        {
            public const string Show = "Debug.Show";
            public const string Hide = "Debug.Hide";
        }


    }
}