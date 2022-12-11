using GameInsideGame.Ability;
using GameInsideGame.Component.Character;
using Service.SystemBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    private PlayerController _playerRef;
    [SerializeField] private GameObject _explotionPref;


    private void OnCollisionEnter(Collision collision)
    {
        _playerRef = collision.collider.GetComponent<PlayerController>();

        if(!_playerRef)
        {
            _explotionPref = Instantiate(_explotionPref, this.transform.position, Quaternion.identity);
            Destroy(_explotionPref, 1);
            Destroy(gameObject);
        }
    }
}
