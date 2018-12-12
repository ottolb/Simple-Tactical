using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;

namespace Game.Util
{

    public class CursorController : MonoBehaviour
    {
        public List<CursorData> cursors;


        void Start()
        {
            //UI events
            EventUIManager.StartListening(NUI.Cursor.Normal, OnNormalCursor);
            EventUIManager.StartListening(NUI.Cursor.Attack, OnAttackCursor);
            EventUIManager.StartListening(NUI.Cursor.UnableAttack, OnUnableAttackCursor);
        }

        void OnNormalCursor(object p_data)
        {
            Cursor.SetCursor(cursors[0].cursor, cursors[0].offset, CursorMode.Auto);
        }

        void OnAttackCursor(object p_data)
        {
            Cursor.SetCursor(cursors[1].cursor, cursors[1].offset, CursorMode.Auto);
        }

        void OnUnableAttackCursor(object p_data)
        {
            Cursor.SetCursor(cursors[2].cursor, cursors[2].offset, CursorMode.Auto);
        }


        [System.Serializable]
        public class CursorData
        {
            public Texture2D cursor;
            public Vector2 offset;
        }
    }
}