using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WalkingMonster : Monster
{
   [SerializeField] private float speed = 1.0f;

   private Unit _unit;
   private SpriteRenderer _sprite;
   private Vector3 direction; 


   private void Awake()
   {
      direction = transform.right;
      _sprite = GetComponentInChildren<SpriteRenderer>();
   }

   private void Update()
   {
      Movement();
   }

   protected override void OnTriggerEnter2D(Collider2D collider)
   {
      _unit = collider.GetComponent<Unit>();
      if (_unit && _unit is Character)
      {
         if (Mathf.Abs(_unit.transform.position.x - transform.position.x) < 0.4f) ReceiveDamage();
         else _unit.ReceiveDamage();
      }
   }

   protected virtual void Movement()
   {
      Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position + transform.right * direction.x, 0.1f);
      if (collider2D.Length > 0 && collider2D.All(x => !x.GetComponent<Character>())) direction *= -1; 
      
      transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
   }
}
