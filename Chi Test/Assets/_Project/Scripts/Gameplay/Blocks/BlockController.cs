using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;


namespace Game.Gameplay
{

    public class BlockController : MonoBehaviour
    {
        public List<BaseBlock> blocks;


        private void Awake()
        {
            blocks = new List<BaseBlock>();
        }

        void Start()
        {
            EventManager.StartListening(N.Block.Created, BlockCreated);
            EventManager.StartListening(N.Block.Disabled, BlockDisabled);
            EventManager.StartListening(N.Block.Died, BlockDied);
            EventManager.StartListening(N.Block.Captured, BlockCaptured);
        }

        void BlockCreated(object p_block)
        {
            blocks.Add(p_block as BaseBlock);
        }

        void BlockDisabled(object p_block)
        {
            Remove(p_block);
        }

        void BlockDied(object p_block)
        {
            Remove(p_block);
            if (blocks.Count == 0)
                EventManager.TriggerEvent(N.Game.AllBlocksDestroyed);
        }

        void BlockCaptured(object p_block)
        {
            Remove(p_block);
            if (blocks.Count == 0)
                EventManager.TriggerEvent(N.Game.AllBlocksDestroyed);
        }

        void Remove(object p_block)
        {
            BaseBlock b = p_block as BaseBlock;
            if (blocks.Contains(b))
                blocks.Remove(p_block as BaseBlock);
        }
    }
}