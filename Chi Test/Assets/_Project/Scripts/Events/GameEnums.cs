//
// GameEnums.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//

using UnityEngine;
using System.Collections;

namespace Game.Event
{
    public class GameEnums
    {

    }

    public enum PlayerType
    {
        Competitor = 1,
        Audience = 2,
        None = 1000,
    }

    public enum StartAppType
    {
        Default = 1,
        Contestant = 2,
        Audience = 3,
        Login = 4,
        None = 1000,
    }

    public enum SideMenuState
    {
        NotInCompetition = 1,
        Competition = 2,
        CompetitionRunning = 3
        /*Audience = 3,
		Login = 4,
		None = 1000,*/
    }

}