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
    [SerializeField] private InputReader[] inputReaders;

    public bool AllowedToMove;

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
        AllowedToMove = true;
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        foreach (var ir in inputReaders)
            ir.Enter();
        foreach(var ir in inputReaders)
            ir.OnShoot += ShootAction;
    }

    private void OnDisable()
    {
        foreach (var ir in inputReaders)
            ir.Exit();
        foreach(var ir in inputReaders)
            ir.OnShoot -= ShootAction;
    }

    private void Update()
    {
        foreach(var ir in inputReaders)
            ir.Tick();
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
        Thrust(inputReaders[0].ThrustEngaged);
        Rotate(inputReaders[0].RotationInput);
    }

    private void UpdateMoveVector()
    {
        moveVector = AllowedToMove?Vector2.Lerp(moveVector, targetMoveVector,acceleration*Time.fixedDeltaTime):Vector2.zero;
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
        if (!AllowedToMove) return;
        rb.MoveRotation(rb.rotation - dir*rotateSpeed*Time.fixedDeltaTime);
    }
}
