using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EntityController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;

    private Vector2 moveVector;
    private Vector2 targetMoveVector;

    [SerializeField] private float moveSpeed;
    [Tooltip("Angles per second")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float acceleration;
    [Space(5)] 
    [SerializeField] private InputReader inputReader;

    public Vector2 MoveVector
    {
        get => moveVector;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        moveVector = Vector2.zero;
        targetMoveVector = Vector2.zero;
    }

    private void OnEnable()
    {
        inputReader.OnShoot += ShootAction;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= ShootAction;
    }

    private void Update()
    {
        inputReader.Tick();
    }

    private void FixedUpdate()
    {
        ReadInputs();
        UpdateMoveVector();
        Move(moveVector * Time.fixedDeltaTime);
    }

    protected virtual void ShootAction()
    {
        Debug.Log("Bam!");
    }

    private void ReadInputs()
    {
        Thrust(inputReader.ThrustEngaged);
        Rotate(inputReader.RotationInput);
    }

    private void UpdateMoveVector()
    {
        moveVector = Vector2.Lerp(moveVector, targetMoveVector,acceleration*Time.fixedDeltaTime);
    }

    private void Move(Vector2 movementThisFrame)
    {
        rb.MovePosition(rb.position + movementThisFrame);
    }

    private void Thrust(bool on)
    {
        targetMoveVector = on ? (Vector2)transform.up * moveSpeed : Vector2.zero;
    }

    private void Rotate(float dir)
    {
        rb.MoveRotation(rb.rotation - dir*rotateSpeed*Time.fixedDeltaTime);
    }
}
