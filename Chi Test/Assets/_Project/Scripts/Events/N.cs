//
// N.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


namespace Game.Event
{
    /// <summary>
    /// Class that contains notifications string for the whole application.
    /// </summary>
    public class N
    {

        /// <summary>
        /// App notifications
        /// </summary>
        public class App
        {
            /// <summary>
            /// When application is opened.
            /// </summary>
            public const string Opened = "App.Opened";
            public const string Loaded = "App.Loaded";
        }

        /// <summary>
        /// Game notifications
        /// </summary>
        public class Game
        {

            /// <summary>
            /// Dispacth to start game loading
            /// </summary>
            public const string Load = "Game.Load";

            /// <summary>
            /// Dispatch to setup other sub controllers
            /// </summary>
            public const string Setup = "Game.Setup";

            /// <summary>
            /// When game can be started.
            /// </summary>
            public const string Start = "Game.Started";


            public const string Over = "Game.Over";
            public const string Points = "Game.Points";

            public const string ChangeColors = "Game.ChangeColors";

            /// <summary>
            /// All blocks destroyed
            /// </summary>
            public const string AllBlocksDestroyed = "Game.AllBlocksDestroyed";
            /// <summary>
            /// Level Win: All blocks captured.
            /// </summary>
            //public const string AllBlocksCaptured = "Game.AllBlocksCaptured";

            public const string TurnChanged = "Game.TurnChanged";
            public const string TurnFinished = "Game.TurnFinished";
            public const string RegisterUnitController = "Game.RegisterUnitController";
        }

        public class Level
        {
            /// <summary>
            /// Request an available Spawn Point for Player units
            /// </summary>
            public const string RequestPlayerSP = "Level.RequestPlayerSP";

            /// <summary>
            /// When an available SP is found
            /// </summary>
            public const string SetPlayerSP = "Level.SetPlayerSP";


            /// <summary>
            /// Request an available Spawn Point for NPC units
            /// </summary>
            public const string RequestNPC_SP = "Level.RequestNPC_SP";

            /// <summary>
            /// When an available SP is found
            /// </summary>
            public const string SetNPC_SP = "Level.SetNPC_SP";

            public const string Load = "Level.Load";
            public const string Clean = "Level.Clean";
            public const string Passed = "Level.Passed";
            public const string NextLevel = "Level.NextLevel";

            public const string SetFinished = "Level.SetFinished";
            public const string Progress = "Level.Progress";

            public const string ChangePieceMaterial = "Level.ChangePieceMaterial";
            public const string ChangeTowerMaterial = "Level.ChangeTowerMaterial";
            public const string ChangeBackgroundMaterial = "Level.ChangeBackgroundMaterial";
        }

        /// <summary>
        /// Unit/Character events
        /// </summary>
        public class Unit
        {
            public const string Died = "Unit.Died";
            public const string Reset = "Unit.Reset";

            public const string SelectUnit = "Unit.SelectUnit";
            public const string HoverUnit = "Unit.HoverUnit";

            public const string CheckUnitMovement = "Unit.CheckUnitMove";
            public const string MoveUnit = "Unit.MoveUnit";

            public const string PlayerUnits = "Unit.PlayerUnits";
        }

        public class Score
        {
            public const string NewHighscore = "Score.New";
            public const string NewScore = "Score.NewScore";
            public const string ScoreAdded = "Score.ScoreAdded";
            public const string CoinPick = "Score.CoinPick";
        }
        public class Analytics
        {

            public const string GD_Taps = "A.GD_Taps";

        }


        public class GameBalance
        {
            public const string Updated = "GB.Updated";
        }


    }
}