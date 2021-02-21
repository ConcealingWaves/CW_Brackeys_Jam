using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EntityController : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    private Vector2 moveVector;
    private Vector2 targetMoveVector;

    [SerializeField] private float moveSpeed;
    [HideInInspector] public float MoveSpeedFactor;
    [Tooltip("Angles per second")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float acceleration;
    [Space(5)] 
    [SerializeField] private List<InputReader> inputReaders;

    [HideInInspector] public float RotationalInput;
    [HideInInspector] public bool MovementInput;
    [HideInInspector] public bool BackwardsInput;

    public bool AllowedToMove;
    public bool AllowedToShoot;

    public float InternalTimer;

    public Vector2 MoveVector
    {
        get => moveVector;
    }

    [HideInInspector] public Vector2 ExternalMoveVector;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        moveVector = Vector2.zero;
        targetMoveVector = Vector2.zero;
        ExternalMoveVector = Vector2.zero;
        MoveSpeedFactor = 1;
        AllowedToMove = true;
        AllowedToShoot = true;
    }

    private void Start()
    {
        foreach (var ir in inputReaders)
            ir.Init(this);
    }

    private void OnEnable()
    {
        foreach (var ir in inputReaders)
            ir.Enter(this);
    }

    private void OnDisable()
    {
        foreach (var ir in inputReaders)
            ir.Exit(this);
    }

    private void Update()
    {
        foreach(var ir in inputReaders)
            ir.Tick(this);
    }

    private void FixedUpdate()
    {
        timeSinceLastRotateStart += Time.fixedDeltaTime;
        ReadInputs();
        UpdateMoveVector();
        InternalTimer += Time.fixedDeltaTime;
        Move(moveVector * Time.fixedDeltaTime);
    }

    public virtual void ShootAction()
    {
        if (!AllowedToShoot) return;
        print("bam!");
    }

    private void ReadInputs()
    {
        Thrust(MovementInput, BackwardsInput);
        Rotate(RotationalInput);
    }

    private void UpdateMoveVector()
    {
        moveVector = AllowedToMove?Vector2.Lerp(moveVector, targetMoveVector,acceleration*Time.fixedDeltaTime):Vector2.zero;
    }

    private void Move(Vector2 movementThisFrame)
    {
        if (rb == null) return;
        rb.MovePosition(rb.position + movementThisFrame);
    }

    private void Thrust(bool on, bool back)
    {
        targetMoveVector = on ? (Vector2)transform.up * (moveSpeed * MoveSpeedFactor) : Vector2.zero;
        targetMoveVector -= back ? (Vector2) transform.up * (moveSpeed * MoveSpeedFactor) / 2 : Vector2.zero;
        targetMoveVector += ExternalMoveVector;
    }

    private void Rotate(float dir)
    {
        if (!AllowedToMove || rb == null) return;
        rb.MoveRotation(rb.rotation - dir*rotateSpeed*Time.fixedDeltaTime);
    }

    public string GetRhythmPart()
    {
        foreach (var v in inputReaders)
        {
            if (v is RhythmInputReader)
            {
                RhythmInputReader rhythm = (RhythmInputReader) v;
                return rhythm.Part;
            }
        }
        Debug.LogWarning("Didn't find a rhythm part on object", this);
        return "";
    }

    public void RotateForSeconds(float dir, float s)
    {
        StartCoroutine(RotateSequence(dir, s));
    }

    [HideInInspector] public float timeSinceLastRotateStart;
    [HideInInspector] public float rotateRestTime;
    IEnumerator RotateSequence(float dir, float s)
    {
        timeSinceLastRotateStart = 0;
        RotationalInput = dir;
        yield return new WaitForSeconds(s);
        RotationalInput = 0;
        
    }

    public virtual void InvokeShootAction(){}
}
