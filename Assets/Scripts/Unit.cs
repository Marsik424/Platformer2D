using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int lives { set; get; } = 1;
    public virtual void ReceiveDamage()
    {
        lives--;
        if (lives < 1) Destroy(gameObject);
    }
}
