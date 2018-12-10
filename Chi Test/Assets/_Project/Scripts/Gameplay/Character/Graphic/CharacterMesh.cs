using OutlineFX;
using UnityEngine;

namespace Game.Gameplay
{
    public class CharacterMesh : MonoBehaviour
    {
        public Outline outline;
        public Color hoverColor;

        public void SetupCharacter(int p_index)
        {
            SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                if (i != p_index)
                    Destroy(skinnedMeshRenderers[i].gameObject);
            }

            outline = skinnedMeshRenderers[p_index].GetComponent<Outline>();
        }

        public void Hover()
        {
            outline.enabled = true;
            outline.OutlineColor = hoverColor;
        }

        public void StopOutline()
        {
            outline.enabled = false;
        }
    }
}