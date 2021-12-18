using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : Unit
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float jumpForce = 15;
    [SerializeField] private int lives = 5;
    [SerializeField] private int money = 0;
    [SerializeField] private bool isGround = false;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private HealthBar _healthBar;
    public Text _text;

    
    public int Lives
    {
        get => lives;
        set
        {
            if (value < 1) {Destroy(gameObject); Application.Quit();}
            lives = value;
            _healthBar.Refresh();
        }
    }

    public int Money{ get => money;
        set
        {
            money = value;
            _text.text = money.ToString();
        }
    }

    private CharState State
    {
        get => (CharState) _animator.GetInteger("State");
        set => _animator.SetInteger("State", (int) value);
    }

    private void Awake()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isGround) State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGround && Input.GetButtonDown("Jump")) Jump();
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        _spriteRenderer.flipX = direction.x < 0.0f;
        if (isGround) State = CharState.Run;
    }


    private void Jump()
    {
        _rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = CharState.Jump;
    }

    private void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            State = CharState.Jump;
        }
    }

    private void OnCollisionStay2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Ground")) isGround = true;
    }

    public override void ReceiveDamage()
    {
        Lives--;
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.AddForce(transform.up * 11.0f, ForceMode2D.Impulse);
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump
}