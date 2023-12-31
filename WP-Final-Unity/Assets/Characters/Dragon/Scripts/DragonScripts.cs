using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Sleeping,
    StandUp,
    SideWalking,
    EnteringSideWalk,
    BasicAttacking,
    ClawAttacking,
    FlameAttacking,
    MagicAttacking,
    Wait,
    Dead
}
public class DragonScripts : Character
{
    [Header("Objects")]
    [SerializeField] GameObject magicAttackAnimation;
    [SerializeField] AttackScript attackScript;

    [Header("Stats")]
    [SerializeField] float maxHealth;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [Header("Detect Distance Parameters")]
    [SerializeField] float sideWalkRadius;
    [SerializeField] float meleeCombatRange;
    [SerializeField] float rangedCombatRange;
    [SerializeField] float detectRadius;
    [SerializeField] float aggroRadius;

    [Header("Attack Parameters")]
    [SerializeField] float basicAttackRange;
    [SerializeField] float basicAttackDamage;
    [SerializeField] float clawAttackRange;
    [SerializeField] float clawAttackDamage;
    [SerializeField] float flameAttackRange;
    [SerializeField] float flameAttackDamage;
    [SerializeField] float magicAttackRange;
    [SerializeField] float magicAttackDamage;
    [SerializeField] float magicAttackRadius;
    [SerializeField] float magicRadius;
    [SerializeField] int magicCount;

    // Private Variables
    Animator animator;
    GameObject player;
    Vector3 playerPos;
    Coroutine coroutine;
    float speed, health;
    float fireDamage, fireDuration, fireTimer;
    bool ignited = false;

    State state;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 0;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPos);
        switch(state)
        {
            case State.Sleeping:
                if(distance < detectRadius) // if player enters detect radius, stands up
                {
                    state = State.StandUp;
                    animator.SetBool("StandUp", true);
                }
                break;
            case State.StandUp:
                if(distance > detectRadius) // if player leaves detect radius, return to sleep
                {
                    state = State.Sleeping;
                    animator.SetBool("StandUp", false);
                }
                else if(distance < aggroRadius)// if player enters aggro radius, use magic attack and start combat
                {
                    state = State.Wait;
                    MagicAttackStart();
                    // next state : EnteringSideWalk
                }
                break;
            case State.EnteringSideWalk: // run to side walk radius
                if(Mathf.Abs(distance - sideWalkRadius) < 2f)
                {
                    state = State.SideWalking;
                    StopMoving();
                    float random = Random.Range(2f, 5f);
                    coroutine = StartCoroutine(Interrupt(random));
                }
                else if(distance < sideWalkRadius)
                {
                    RunAway(playerPos);
                }
                else if(distance > sideWalkRadius)
                {
                    RunToward(playerPos);
                }
                break;
            case State.SideWalking:
                SideWalk();
                if(distance < meleeCombatRange)
                {
                    StopMoving();
                    StopCoroutine(coroutine);
                    int random = Random.Range(0, 2);
                    if(random == 0) // 50%, basic attack
                    {
                        state = State.BasicAttacking;
                    }
                    else if(random == 1) // 50%, melee combo
                    {
                        state = State.ClawAttacking;
                    }
                }
                else if(distance > rangedCombatRange)
                {
                    StopMoving();
                    StopCoroutine(coroutine);
                    int random = Random.Range(0, 2);
                    if (random == 0) // 50%, flame attack
                    {
                        state = State.FlameAttacking;
                        // next state : EntringSideWalk
                    }
                    else if (random == 1) // 50%, magic attack
                    {
                        state = State.MagicAttacking;
                        // next state : EnteringSideWalk
                    }
                }
                // wait for random time, interrupt will be executed to terminate this state
                break;
            case State.BasicAttacking:
                if (distance > basicAttackRange)
                    RunToward(playerPos);
                else
                {
                    StopMoving();
                    state = State.Wait;
                    BasicAttackStart();
                    // next state : EnteringSideWalk
                }
                break;
            case State.ClawAttacking:
                if (distance > clawAttackRange)
                    RunToward(playerPos);
                else
                {
                    StopMoving();
                    state = State.Wait;
                    ClawAttackStart();
                    // next state : BasicAttacking
                }
                break;
            case State.FlameAttacking:
                if (distance < flameAttackRange)
                    RunAway(playerPos);
                else
                {
                    StopMoving();
                    state = State.Wait;
                    FlameAttackStart();
                    // next state : EnteringSideWalk
                }
                break;
            case State.MagicAttacking:
                if(distance < magicAttackRange)
                    RunAway(playerPos);
                else
                {
                    StopMoving();
                    state = State.Wait;
                    MagicAttackStart();
                    // next state : EnteringSideWalk
                }
                break;
            case State.Wait:
                break;
            default:
                break;
        }
        if(ignited)
        {
            FireDOT();
        }
    }

    // Flame Attack
    void FlameAttackStart()
    {
        FaceToward(playerPos);
        animator.SetTrigger("FlameAttack");
    }
    void FlameAttackEnd()
    {
        state = State.EnteringSideWalk;
    }

    // Magic Attack
    void MagicAttackStart()
    {
        FaceToward(playerPos);
        animator.SetTrigger("Scream");
    }
    void MagicAttack1()
    {
        Vector3[] pos = new Vector3[magicCount];
        for (int i = 0; i < magicCount; i++)
        {
            float range = ((float)i / magicCount) * magicAttackRadius;
            float x = Random.Range(-range, range);
            float y = 0;
            float z = Random.Range(-range, range);
            Vector3 newPos = new(x, y, z);
            bool overlap = false;
            for (int j = 0; j < i; j++)
            {
                if (Vector3.Distance(pos[j], newPos) < magicRadius)
                {
                    overlap = true;
                    break;
                }
            }
            if (overlap)
            {
                i--;
                continue;
            }
            else
            {
                pos[i] = newPos;
                GameObject instant = Instantiate(magicAttackAnimation);
                instant.transform.position = player.transform.position + pos[i];
            }
        }
    }
    void MagicAttackEnd()
    {
        state = State.EnteringSideWalk;
    }

    // Claw Attack
    void ClawAttackStart()
    {
        FaceToward(playerPos);
        animator.SetTrigger("ClawAttack");
        attackScript.StartAttack(clawAttackDamage);
    }
    void ClawAttackEnd()
    {
        state = State.BasicAttacking;
    }

    // Basic Attack
    void BasicAttackStart()
    {
        FaceToward(playerPos);
        animator.SetTrigger("BasicAttack");
        attackScript.StartAttack(basicAttackDamage);
    }
    void BasicAttackEnd()
    {
        state = State.EnteringSideWalk;
    }

    // Move Functions
    void SideWalk()
    {
        Vector3 directionToPlayer = playerPos - transform.position;
        Vector3 sideDirection = Quaternion.Euler(0, 90, 0) * directionToPlayer.normalized;
        FaceToward(transform.position + sideDirection);
        Walk();
    }
    void RunAway(Vector3 position)
    {
        Vector3 direction = transform.position - position;
        FaceToward(transform.position + direction);
        Run();
    }
    void RunToward(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        FaceToward(transform.position + direction);
        Run();
    }

    // Coroutine Functions
    IEnumerator Interrupt(float time)
    {
        yield return new WaitForSeconds(time);
        int random = Random.Range(0, 4);
        StopMoving();
        switch(random)
        {
            case 0:
                state = State.BasicAttacking;
                break;
            case 1:
                state = State.ClawAttacking;
                break;
            case 2:
                state = State.FlameAttacking;
                break;
            case 3:
                state = State.MagicAttacking;
                break;
        }
    }

    // Implement Abstract Functions
    public override void Hurt(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
    }
    public override void Fire_Hurt(float damage, float last_time)
    {
        fireDamage = damage;
        fireDuration = last_time;
        fireTimer = 0;
        ignited = true;
    }

    // Private Functions
    void FaceToward(Vector3 position)
    {
        transform.LookAt(position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    void StopMoving()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        speed = 0;
    }
    void Walk()
    {
        animator.SetBool("Walking", true);
        speed = walkSpeed;
        Move();
    }
    void Run()
    {
        animator.SetBool("Running", true);
        speed = runSpeed;
        Move();
    }
    void Move()
    {
        transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);
    }
    void Death()
    {
        animator.SetTrigger("Death");
        state = State.Dead;
    }
    void FireDOT()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer >= 1)
        {
            fireTimer--;
            Hurt(fireDamage);
            fireDuration -= 1;
            if (fireDuration <= 0)
                ignited = false;
        }
    }
}