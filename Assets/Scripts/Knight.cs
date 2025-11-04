using System;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public float walkStopRate = 0.05f;

    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;

    public enum WalkabaleDirection { Right, Left }

    private WalkabaleDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkabaleDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {

            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkabaleDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkabaleDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }

                _walkDirection = value;
            }
        }
    }


    public bool _hasTarget = false;
    public bool HasTarget { get
        {
            return _hasTarget;
        } private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }


    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        

    }


    private void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded) 
            {

                float xVelocity = Mathf.Clamp(rb.linearVelocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed);

                rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
            }
        }
        
        
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkabaleDirection.Right)
        {
            WalkDirection = WalkabaleDirection.Left;
        }
        else if (WalkDirection == WalkabaleDirection.Left)
        {
            WalkDirection = WalkabaleDirection.Right;
        }
        else
        {
            Debug.LogError("WalkDirection is not set to either Right or Left");
        }
    }


    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }


    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
