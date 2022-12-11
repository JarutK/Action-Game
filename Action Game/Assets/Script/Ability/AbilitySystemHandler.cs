using GameInsideGame.Ability;
using GameInsideGame.Enum;
using Service.SystemBase;
using UnityEngine;

namespace GameInsideGame.Ability
{
    public class AbilitySystemHandler : MonoBehaviour
    {
        public static AbilitySystemHandler instance { get; private set; }
        public AbilityProfile abilityData;
        public ScriptableObject abilityBehavior;
        private float _cooldownTime;
        private float _activeTime;
        private AbilityState _state = AbilityState.ready;

        public void Initialize()
        {
            if(instance != null)
                Destroy(instance);
            instance = this;
        }

        void Update()
        {
            switch (_state)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        abilityData.behavior.Activate();
                        _state = AbilityState.active;
                        _activeTime = abilityData.behavior.stat.duration;
                    }
                    break;
                case AbilityState.active:
                    if (_activeTime > 0)
                        _activeTime -= SystemTime.DeltaTime;
                    else
                    {
                        _state = AbilityState.cooldown;
                        _cooldownTime = abilityData.behavior.stat.cooldown;
                    }
                    break;
                case AbilityState.cooldown:
                    if (_cooldownTime > 0)
                        _cooldownTime -= SystemTime.DeltaTime;
                    else
                        _state = AbilityState.ready;
                    break;
            }
        }
    }
}