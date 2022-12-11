using Service.SystemBase;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GameInsideGame.Component.Character
{

    //Calculate Behavior
    public class CharacterBehavior : MonoBehaviour
    {
        //Reference
        [SerializeField] protected CharacterController _characterController;
        [SerializeField] protected Animator _animator;

        //Movement
        protected float _targetSpeed;
        protected Vector3 _velocityInput;
        protected Vector3 _velocity;
        private const float _gravity = 9.81f;

        //Combat
        protected int _attackIndex;
        protected const int _attackMaxIndex = 3;
        protected float _NormalAttackCooldown;
        protected const float _NormalAttackMinDelayPercent = 0.5f;
        protected float _NormalAttackMinDelay;
        protected bool _isAttacking = false;
        protected float _RollingCooldown;
        protected bool _isRolling = false;

        //AnimationClip
        [SerializeField] private AnimationClip[] _normalAttackAnimClip = new AnimationClip[0];
        [SerializeField] private AnimationClip _rollingAnimClip;
        

        protected virtual void Update()
        {
            ExecuteMovement();
            ExecuteAnimation();
        }

        private void OnAnimatorMove()
        {
            _velocity.x = _animator.velocity.x;
            _velocity.z = _animator.velocity.z;
        }

        private void ExecuteMovement()
        {
            //Add gravity
            _velocity.y -= _gravity * SystemTime.DeltaTime;

            if (_isAttacking)
                return;
            _characterController.Move(_velocity * SystemTime.DeltaTime);
        }
        
        private void ExecuteAnimation()
        {
            _animator.SetFloat("Horizontal", Mathf.Clamp01(_velocityInput.magnitude) * _targetSpeed, 0.1f, SystemTime.DeltaTime);
            _animator.SetFloat("Vertical", Mathf.Clamp(_velocity.y, -0.05f, 1f), 0.05f, SystemTime.DeltaTime);
            if (_RollingCooldown > 0)
                _RollingCooldown -= SystemTime.DeltaTime;

            if (_RollingCooldown < 0)
                _isRolling = false;

            if (_NormalAttackCooldown > 0)
            {
                _NormalAttackCooldown -= SystemTime.DeltaTime;

                if (_NormalAttackCooldown < _NormalAttackMinDelay)
                {
                    _isAttacking = false;
                }

                if (_NormalAttackCooldown <= 0)
                {
                    _attackIndex = 0;
                    _animator.SetInteger("AttackIndex", _attackIndex);
                }
            }
        }
        
        protected void Combat()
        {
            if (_attackIndex >= _attackMaxIndex || _isAttacking)
                return;
            _NormalAttackCooldown = _normalAttackAnimClip[_attackIndex].length;
            _NormalAttackMinDelay = _NormalAttackCooldown * _NormalAttackMinDelayPercent;  
            _isAttacking = true;
            _animator.SetInteger("AttackIndex", _attackIndex + 1);
            if (_attackIndex < _normalAttackAnimClip.Length - 1)
                _attackIndex++;
            else
                _attackIndex = 0;
        }

        protected void Rolling()
        {
            _isAttacking = false;
            _attackIndex = 0;
            _animator.SetTrigger("Dodge");
            _isRolling = true;
            _RollingCooldown = _rollingAnimClip.length;
        }
    }

}
