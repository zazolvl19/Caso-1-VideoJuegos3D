using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask whatIsWalkable;

    [SerializeField]
    float gravityMultiplier;

    CharacterController _characterController;

    float _inputX;
    float _inputZ;

    float _gravityY;
    float _velocityY;

    bool _isJumpPressed;
    bool _isGrounded;

    Camera _mainCamera;

    Vector3 _direction;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _gravityY = Physics.gravity.y;
    }

    private void Start()
    {
        _isGrounded = IsGrounded();
        if (!_isGrounded)
        {
            StartCoroutine(WaitForGroundedCoroutine());
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            _inputZ = 1.0f; // Mover hacia arriba
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            _inputZ = -1.0f; // Mover hacia abajo
        }
    }


    private void HandleJump()
    {
        _isJumpPressed = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        HandleGravity();
        Rotate();
        Move();
    }

    private bool IsMove()
    {
        return (_inputX != 0.0F || _inputZ != 0.0F);
    }

    private void Move()
    {
        _direction.y = _velocityY;
        _characterController.Move(_direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        if (!IsMove())
        {
            _direction = Vector3.zero;
            return;
        }

        // Calculamos el ángulo de rotación basado en la entrada de movimiento
        float targetAngle = Mathf.Atan2(_inputX, _inputZ) * Mathf.Rad2Deg;

        // Calculamos la rotación actual del jugador
        Vector3 playerRotation = new Vector3(0f, _mainCamera.transform.eulerAngles.y, 0f);

        // Aplicamos la rotación al jugador
        transform.rotation = Quaternion.Euler(playerRotation);

        // Calculamos la rotación de la dirección de movimiento
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        // Rotamos la dirección de acuerdo al ángulo de rotación del jugador
        _direction = targetRotation * Vector3.forward;
    }

    private void Jump()
    {
        _velocityY = jumpForce;
        _isGrounded = false;
        StartCoroutine(WaitForGroundedCoroutine());
    }

    private bool IsGrounded()
    {
        return _characterController.isGrounded;
    }

    private IEnumerator WaitForGroundedCoroutine()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        _isGrounded = true;
    }

    private void HandleGravity()
    {
        if (_isGrounded)
        {
            if (_velocityY < -1.0F)
            {
                _velocityY = -1.0F;
            }

            HandleJump();
            if (_isJumpPressed)
            {
                Jump();
            }
        }
        else
        {
            _velocityY += _gravityY * gravityMultiplier * Time.deltaTime;
        }
    }
}
