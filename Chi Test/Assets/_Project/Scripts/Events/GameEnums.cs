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

namespace Game.Data
{
    public class GameEnums
    {

    }

    public enum PlayerCharacterType
    {
        None = 0,
        SwordMale = 1,
        SwordFemale = 2,
    }

    public enum EnemyCharacterType
    {
        None = 0,
        Ghost1 = 1,
        Ghost2 = 2,
        GoblinFemale = 3,
        GoblinMale = 4,
        GoblinShaman = 5,
        GoblinWarChief = 6,
        GoblinWarriorFemale = 7,
        GoblinWarriorMale = 8,
        RockGolem = 9,
        SkeletonKnight = 10,
        SkeletonSlave = 11,
        SkeletonSoldier = 12,
        SkeletonSoldier2 = 13,
        TourmentedSoul = 14,
    }
}