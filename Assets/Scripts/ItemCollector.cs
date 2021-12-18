using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character _character = collider.GetComponent<Character>();
        if (_character && gameObject.CompareTag("Heart")) { _character.Lives++; Destroy(gameObject);}
        if (_character && gameObject.CompareTag("Coin")) { _character.Money++; Destroy(gameObject);}
    }
}
