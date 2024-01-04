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

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class DragonScripts : Character
{
    [Header("Objects")]
    [SerializeField] GameObject magicAttackAnimation;
    [SerializeField] GameObject onFireAnimation;
    [SerializeField] BasicAttackScript basicAttackScript;
    [SerializeField] ClawAttackScript clawAttackScript;
    [SerializeField] FlameAttack flameAttackScript;
    [SerializeField] MagicAttackScript magicAttackScript;
    [SerializeField] new DragonAudioHandler audio;
    [SerializeField] Healthbar healthbar;

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

    [Header("Basic Attack Parameters")]
    [SerializeField] float basicAttackRange;
    [SerializeField] float basicAttackDamage;

    [Header("Claw Attack Parameters")]
    [SerializeField] float clawAttackRange;
    [SerializeField] float clawAttackDamage;

    [Header("Flame Attack Parameters")]
    [SerializeField] float flameAttackRange;
    [SerializeField] float flameAttackDamage;
    [SerializeField] float flameDuration;

    [Header("Magic Attack Parameters")]
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
    float sideWalkDegree;
    bool ignited = false, invincible = false;

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
                audio.Sleep();
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
                    float random = Random.Range(2f, 5f); // determine side walk end time
                    coroutine = StartCoroutine(Interrupt(random));
                    int direction = Random.Range(0, 2);
                    if(direction == 0)
                    {
                        sideWalkDegree = Random.Range(-90f, -45f);
                    }
                    else if(direction == 1)
                    {
                        sideWalkDegree = Random.Range(45f, 90f);
                    }
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
                SideWalk(sideWalkDegree);
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
        audio.FlameAttack();
        animator.SetTrigger("FlameAttack");
        flameAttackScript.StartFlame(flameAttackDamage, flameDuration);
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
        audio.Roar();
        magicAttackScript.StartMagicAttack(magicAttackDamage);
    }
    void MagicAttack1()
    {
        audio.MagicAttack();
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
        audio.ClawAttack();
        animator.SetTrigger("ClawAttack");
        clawAttackScript.StartClawAttack(clawAttackDamage);
    }
    void ClawAttackEnd()
    {
        state = State.BasicAttacking;
    }

    // Basic Attack
    void BasicAttackStart()
    {
        FaceToward(playerPos);
        audio.BasicAttack();
        animator.SetTrigger("BasicAttack");
        basicAttackScript.StartBasicAttack(basicAttackDamage);
    }
    void BasicAttackEnd()
    {
        state = State.EnteringSideWalk;
    }

    // Move Functions
    void SideWalk(float degree)
    {
        Vector3 directionToPlayer = playerPos - transform.position;
        Vector3 sideDirection = Quaternion.Euler(0, degree, 0) * directionToPlayer.normalized;
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
        if(!invincible)
        {
            invincible = true;
            Invoke(nameof(DisableInvincible), 0.1f);
            audio.Hurt();
            health -= damage;
            healthbar.UpdateHealthbar(health / maxHealth);
            if (health <= 0)
            {
                Death();
            }
        }
    }
    public override void Fire_Hurt(float damage, float last_time)
    {
        fireDamage = damage;
        fireDuration = last_time;
        fireTimer = 0;
        ignited = true;
        onFireAnimation.SetActive(true);
    }

    // Public Functions
    public void PartDamage(float damage, string tag)
    {
        float multiplier = 0f;
        if (tag == "DragonHead")
            multiplier = 1.5f;
        else if (tag == "DragonNeck")
            multiplier = 1.25f;
        else if (tag == "DragonTorso")
            multiplier = 1f;
        else if (tag == "DragonWing")
            multiplier = 0.75f;
        else if (tag == "DragonFeet")
            multiplier = 1f;
        else if(tag == "DragonTail")
            multiplier = 1.25f;
        Hurt(damage * multiplier);
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
        audio.Walk();
        animator.SetBool("Walking", true);
        speed = walkSpeed;
        Move();
    }
    void Run()
    {
        audio.Run();
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
        audio.Death();
        animator.SetTrigger("Death");
        ignited = false;
        onFireAnimation.SetActive(false);
        healthbar.Destroy();
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
            {
                ignited = false;
                onFireAnimation.SetActive(false);
            }
        }
    }
    void DisableInvincible()
    {
        invincible = false;
    }
}
