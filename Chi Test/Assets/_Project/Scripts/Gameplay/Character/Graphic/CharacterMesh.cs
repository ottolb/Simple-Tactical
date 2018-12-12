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
        private Animator _animator;
        public Locomotion locomotion;

        private int attackType;

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
            locomotion = new Locomotion(_animator);
        }

        public void SetupCharacter(int p_index, int p_attackAnimationType)
        {
            attackType = p_attackAnimationType;
            //Character types start from 1, then decrease it
            p_index--;

            //Destroy other character meshes
            SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                if (i != p_index)
                    Destroy(skinnedMeshRenderers[i].gameObject);
            }

            skinnedMeshRenderers[p_index].gameObject.SetActive(true);
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
            //Set attack type based on character type, starting from 1
            _animator.SetInteger("AttackType", attackType);
            AudioController.Play("Attack");
        }

        public void TakeHit()
        {
            _animator.SetTrigger("GetHit");
            AudioController.Play("TakeHit");
        }

        public void Die()
        {
            _animator.SetTrigger("Die");
            AudioController.Play("Die");
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

        public Animator Animator
        {
            get { return _animator; }
        }
    }
}