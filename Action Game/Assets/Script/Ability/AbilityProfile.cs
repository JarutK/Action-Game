using GameInsideGame.Enum;
using UnityEngine;

namespace GameInsideGame.Ability
{
    [CreateAssetMenu(fileName = "AbilityProfile", menuName = "Asset Data/Ability/AbilityProfile")]
    public class AbilityProfile : ScriptableObject
    {
        public AbilityBehavior behavior;
    }
}