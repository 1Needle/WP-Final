using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class SkeletonScript : Character
{
    // GameObjects
    [SerializeField] GameObject onFireAnimation;
    [SerializeField] new AudioHandler audio;
    [SerializeField] Healthbar healthbar; 
    // components
    Rigidbody rb;
    Animator animator;
    new Collider collider;
    GameObject player;
    PatrollScript patroll;
    NPC2Controller nPC2Controller;
    // configurable variables
    [SerializeField] float maxHealth;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float detectRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float aggroRadius;
    [SerializeField] float activityRange;
    [SerializeField] float attackCooldown;
    [SerializeField] float clearCorpseTime;
    [SerializeField] float lookAroundAnimationTime;
    [SerializeField] float warCryAnimationTime;
    [SerializeField] float hurtAnimationTime;
    // fixed variables
    float health;
    Vector3 spawnPosition;
    float speed = 0;
    float fireDamage, fireDuration, fireTimer;
    bool alive = true, canMove = true, giveUp = false, combating = false, ignited = false;
    Coroutine playingAnimation;

    // main logics
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        patroll = GetComponent<PatrollScript>();
        nPC2Controller = GameObject.FindGameObjectWithTag("NPC2").GetComponent<NPC2Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPosition = transform.position;
        health = maxHealth;
    }
    private void Update()
    {
        if (alive)
        {
            if (ignited)
            {
                FireDOT();
            }
            if (canMove)
            {
                float playerDistance = Vector3.Distance(player.transform.position, transform.position);
                float spawnDistance = Vector3.Distance(spawnPosition, transform.position);
                if (speed != 0) // 移動
                {
                    if (speed == runSpeed)
                        audio.Run();
                    else if(speed == walkSpeed)
                        audio.Walk();
                    transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
                }
                if(combating) // 戰鬥中
                {
                    if (playerDistance <= attackRadius) // 攻擊
                    {
                            Attack();
                    }
                    else if (spawnDistance > activityRange || playerDistance > aggroRadius) // 脫離仇恨
                    {
                        if (!giveUp)
                            GiveUpChasing();
                    }
                    else if (playerDistance <= aggroRadius) // 接近玩家
                    {
                        Run();
                        FaceDirection(player.transform.position);
                    }
                }
                else // 遊走中
                {
                    patroll.Patroll();
                    if (playerDistance <= detectRadius) // 發現玩家
                    {
                        FoundPlayer();
                    }
                }
            }
        }
    }

    // implementation of abstract functions
    public override void Hurt(float damage)
    {
        FaceDirection(player.transform.position);
        Stop();
        patroll.StopPatroll();
        if(playingAnimation != null)
            StopCoroutine(playingAnimation);
        giveUp = false; // cancel give up animation
        combating = true;
        canMove = false;
        health -= damage;
        healthbar.UpdateHealthbar(health / maxHealth);
        audio.Damaged();
        if (health <= 0)
        {
            Death();
        }
        else
        {
            playingAnimation = StartCoroutine(HurtAnimation());
        }
    }
    public override void Fire_Hurt(float damage, float last_time)
    {
        fireDamage = damage;
        fireDuration = last_time;
        fireTimer = 0;
        ignited = true;
        onFireAnimation.SetActive(ignited);
    }
    IEnumerator HurtAnimation()
    {
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(hurtAnimationTime);
        canMove = true;
    }
    // public functions
    public void Walk()
    {
        animator.SetBool("Walk", true);
        speed = walkSpeed;
    }
    public void Stop()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        speed = 0;
    }
    public void FaceDirection(Vector3 position)
    {
        transform.LookAt(position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    public void PlayAnimation(string name)
    {
        animator.SetTrigger(name);
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    // private functions
    void Attack()
    {
        playingAnimation = StartCoroutine(AttackAnimation());
    }
    IEnumerator AttackAnimation()
    {
        if(playingAnimation != null)
            StopCoroutine(playingAnimation);
        giveUp = false;
        Stop();
        FaceDirection(player.transform.position);
        audio.Attack();
        animator.SetTrigger("Attack");
        canMove = false;
        yield return new WaitForSeconds(attackCooldown);
        canMove = true;
    }
    void GiveUpChasing()
    {
        playingAnimation = StartCoroutine(GiveUpAnimation());
    }
    IEnumerator GiveUpAnimation()
    {
        Stop();
        giveUp = true;
        animator.SetTrigger("LookAround");
        yield return new WaitForSeconds(lookAroundAnimationTime);
        combating = false;
        giveUp = false;
    }
    void FoundPlayer()
    {
        playingAnimation = StartCoroutine(FoundPlayerAnimation());
    }
    IEnumerator FoundPlayerAnimation()
    {
        canMove = false;
        combating = true;
        FaceDirection(player.transform.position);
        patroll.StopPatroll();
        Stop();
        audio.Roar();
        animator.SetTrigger("WarCry");
        yield return new WaitForSeconds(warCryAnimationTime);
        canMove = true;
    }
    void Run()
    {
        animator.SetBool("Run", true);
        speed = runSpeed;
    }
    void Death()
    {
        nPC2Controller.EnemyDied();
        audio.Death();
        animator.SetTrigger("Hurt");
        animator.SetTrigger("Death");
        rb.isKinematic = true;
        collider.enabled = false;
        alive = false;
        ignited = false;
        healthbar.Destroy();
        onFireAnimation.SetActive(false);
        playingAnimation = StartCoroutine(ClearCorpse());
    }
    void FireDOT()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer >= 1)
        {
            fireTimer--;
            fireDuration--;
            health -= fireDamage;
            healthbar.UpdateHealthbar(health / maxHealth);
            if(health <= 0)
            {
                Death();
            }
            else if(fireDuration <= 0)
            {
                ignited = false;
                onFireAnimation.SetActive(false);
            }
        }
    }
    IEnumerator ClearCorpse()
    {
        yield return new WaitForSeconds(clearCorpseTime);
        Destroy(gameObject);
    }
}
