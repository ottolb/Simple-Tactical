using System;
using OutlineFX;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class CharacterMesh : MonoBehaviour
    {
        public Outline outline;
        public Color hoverColor;

        private NavMeshAgent _navMeshAgent;
        public Animator _animator;
        public Locomotion locomotion;

        private int characterType;

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
            locomotion = new Locomotion(_animator);
        }

        public void SetupCharacter(int p_index)
        {
            characterType = p_index;
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

        public void Attack()
        {
            _animator.SetInteger("AttackType", characterType + 1);
        }

        public void TakeHit()
        {
            _animator.SetTrigger("GetHit");
        }

        public void Die()
        {

        }

        private void OnAnimatorMove()
        {
            _navMeshAgent.velocity = _animator.deltaPosition / Time.deltaTime;
            //transform.rotation = animator.rootRotation;
        }

        public void SetNavMeshAgent(NavMeshAgent p_navMeshAgent)
        {
            _navMeshAgent = p_navMeshAgent;
        }

        public Quaternion GetRotation()
        {
            return _animator.rootRotation;
        }
    }
}