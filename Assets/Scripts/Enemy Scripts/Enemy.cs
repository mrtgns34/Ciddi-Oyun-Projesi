using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public EnemyFeatures enemyFeatures;
    #region sounds
    [SerializeField]
    AudioClip damageSoundEffect;
    AudioSource source;
    #endregion
    #region features
    private float health;
    private float stoppingDistance;
    private Color color;
    #endregion

    [HideInInspector]
    public float maxSpeed;

    private NavMeshAgent nma;
    private Animator animator;

    private Transform player;
    private bool isDead;

    [SerializeField]
    Material mainMaterial;
    void Start()
    {
        SetFeatures();
        SetColor();
        SetAnimator();
        SetNavMeshAgent();
        SetPlayer();
        SetStoppingDistance();
        MoveToPlayer();
        SetAudioSource();
    }
    void Update()
    {
        StopControl();
    }
    //Enemy hit function with damage from laser weapon script.
    public void HitEnemy(float damage)
    {
        if (!isDead)
        {
            source.PlayOneShot(damageSoundEffect);
            if (health + damage <= 0)
            {
                Kill();
            }
            else
            {
                health += damage;
            }
        }
    }
    //Kill this object. Distance is  being sent to calculate kill distances.
    private void Kill()
    {
        isDead = true;
        float distance = Vector3.Distance(player.position, transform.position);
        ActionManager.instance.OnEnemyDead(distance);
        Destroy(gameObject);
    }
    //Set player for navigation mesh agent destination
    private void SetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //Set stopping distance for navigation mesh agent
    private void SetStoppingDistance()
    {
        nma.stoppingDistance = stoppingDistance;
    }
    //Set destination for navigation mesh agent
    private void MoveToPlayer()
    {
        nma.destination = player.position;
    }
    //Set navigation mesh agent and speed
    private void SetNavMeshAgent()
    {
        nma = GetComponent<NavMeshAgent>();
        nma.speed = Random.Range(0.3f, maxSpeed);
    }
    //Set animator
    private void SetAnimator()
    {
        animator = GetComponent<Animator>();
    }
    //Controlling stopping position and action
    private void StopControl()
    {
        if (Vector3.Distance(transform.position, player.position) <= nma.stoppingDistance)
        {
            nma.isStopped = true;
            animator.SetTrigger("stop");
            ActionManager.instance.OnPlayerDead();
        }
    }
    //Set enemy features with scriptable object
    private void SetFeatures()
    {
        health = enemyFeatures.health;
        stoppingDistance = enemyFeatures.stoppingDistance;
        color = enemyFeatures.color;
    }
    //Set enemy color
    private void SetColor()
    {
        mainMaterial.color = color;
    }
    //Set AudioSource
    private void SetAudioSource()
    {
        source = GetComponent<AudioSource>();
    }
}
