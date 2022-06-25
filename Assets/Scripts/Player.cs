using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed;
    public float JumpForce;
    public Transform Sensor;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    void Start() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(moveX, 0.0f, 0.0f);
        _animator.SetBool("IsRunning", Mathf.Abs(Input.GetAxis("Horizontal")) > 0);
        _spriteRenderer.flipX = Mathf.Abs(Input.GetAxis("Horizontal")) > 0 ? Input.GetAxis("Horizontal") < 0 : _spriteRenderer.flipX;
        bool isJumping = !Physics2D.Linecast(transform.position, Sensor.position, 1 << LayerMask.NameToLayer("Ground"));
        _animator.SetBool("IsJumping", isJumping);

        if (Input.GetButton("Jump") && !isJumping) {
            _rigidbody2D.velocity = new Vector2(0.0f, JumpForce);
        }
    }
}
