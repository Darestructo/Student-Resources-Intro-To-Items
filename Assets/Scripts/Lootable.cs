using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    [SerializeField] float _respawnTime = 2f;

    //This same script is used among 3 different prefabs to show how our scripts can be reused.

    AudioSource _pickupSound;

    BoxCollider _boxCollider;

    GameObject _childGameObject;

    //We then set fill those references at the start of our script so they will not be null.
    void Start()
    {
        _pickupSound = GetComponent<AudioSource>(); //Gets the audio source component off of itself.

        _boxCollider = GetComponent<BoxCollider>();

        _childGameObject = transform.GetChild(0).gameObject; //Gets its first child reference as a gameobject.
    }

    void OnTriggerEnter(Collider other)
    {
        //Checks for player script on the player
        //This prevents enemies or other gameobjects from accidentally picking these items up.
        if (other.GetComponent<Player>())
        {
            _boxCollider.enabled = false;   

            _pickupSound.Play();

            _childGameObject.SetActive(false);

            StartCoroutine(RespawnLootable()); //Happens after a delay determined by _respawnTime
        }
    }

    IEnumerator RespawnLootable()
    {
        yield return new WaitForSeconds(_respawnTime);
        _boxCollider.enabled = true;
        _childGameObject.SetActive(true);
    }
}
