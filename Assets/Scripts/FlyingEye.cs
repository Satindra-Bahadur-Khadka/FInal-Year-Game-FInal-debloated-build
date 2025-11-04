using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{

    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public Collider2D deathCollider;


    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;


    Transform nextWaypoint;
    int waypointNumber = 0;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }



    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }


    void Start()
    {
        nextWaypoint = waypoints[waypointNumber];
    }


    private void OnEnable()
    {
        damageable.damagableDeath.AddListener(OnDeath);
    }



    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }



    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //distance to waypoint

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        UpdateDirection();


        rb.linearVelocity = directionToWaypoint * flightSpeed;

        //switch to next waypoint if close enough

        if(distance <= waypointReachedDistance)
        {
            waypointNumber++;

            if(waypointNumber >= waypoints.Count)
            {
                waypointNumber = 0;
            }

            nextWaypoint = waypoints[waypointNumber];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;


        if (transform.localScale.x > 0)
        {
            //facing right
            if(rb.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            //facing left
            if (rb.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }


    public void OnDeath()
    {
        //death falls to the ground
        rb.gravityScale = 2f;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        deathCollider.enabled = true;
    }

}
