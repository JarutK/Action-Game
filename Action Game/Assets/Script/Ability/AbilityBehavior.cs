using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameInsideGame.Ability
{
    [CreateAssetMenu(menuName = "Asset Data/Ability/AbilityBehavior")]
    public class AbilityBehavior : ScriptableObject
    {
        public Sprite icon;
        public AudioClip sound;
        public GameObject abilityPrefab;
        public AbilityBaseStat stat;
        private GameObject _prefabRef;

        public void Activate()
        {
            if (abilityPrefab != null)
            {
                _prefabRef = Instantiate(abilityPrefab, PlayerController.playerInstance.projectileRoot.transform.position, Quaternion.identity);
                _prefabRef.GetComponent<Rigidbody>().velocity = PlayerController.playerInstance.transform.rotation * Vector3.forward * stat.speed;
            }   
               
        }
        //for reset buff stat
        public void Deactivate(GameObject abilityPrefab) { }
    }
}