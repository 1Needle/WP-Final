using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    // components
    Rigidbody rb;
    Animator animator;
    Collider collider;
    // variables
    [SerializeField] float maxHealth;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float speed;
    float health;
    bool alive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        health = maxHealth;
        alive = true;
    }

    // functions
    public void Move()
    {
        
    }
    public void Attack()
    {

    }
    public void Hurt()
    {

    }
}
