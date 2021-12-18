using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    protected virtual void OnTriggerEnter2D(Collider2D _collider)
    {
        Unit _unit = _collider.GetComponent<Unit>();
        if (_unit && _unit is Character) _unit.ReceiveDamage();
    }
}
