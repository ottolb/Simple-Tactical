
using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;

namespace Game.Util
{
    /// <summary>
    /// Handle game cursor textures depending on the action
    /// </summary>
    public class CursorController : MonoBehaviour
    {
        public List<CursorData> cursors;
        public enum CursorType
        {
            Normal,
            Attack,
            UnableAttack
        }

        void Start()
        {
            //UI events
            EventUIManager.StartListening(NUI.Cursor.Normal, OnNormalCursor);
            EventUIManager.StartListening(NUI.Cursor.Attack, OnAttackCursor);
            EventUIManager.StartListening(NUI.Cursor.UnableAttack, OnUnableAttackCursor);
        }

        void OnNormalCursor(object p_data)
        {
            ChangeCursor(CursorType.Normal);
        }

        void OnAttackCursor(object p_data)
        {
            ChangeCursor(CursorType.Attack);
        }

        void OnUnableAttackCursor(object p_data)
        {
            ChangeCursor(CursorType.UnableAttack);
        }

        void ChangeCursor(CursorType p_id)
        {
            int index = (int)p_id;
            Cursor.SetCursor(cursors[index].cursor, cursors[index].offset, CursorMode.Auto);
        }


        [System.Serializable]
        public class CursorData
        {
            public Texture2D cursor;
            public Vector2 offset;
        }
    }
}