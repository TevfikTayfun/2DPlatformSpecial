using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool b_IsEnemyCanAttack;
    [HideInInspector] public bool b_IsPlayerSeenByEnemy;

    enum EnemyState { PATROLLING, ATTACKING }
    EnemyState enemyState;

    Animator _animator;
    Rigidbody2D _rb;

    PatrolBetweenPoints patrolBetweenPoints;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        patrolBetweenPoints = gameObject.GetComponent<PatrolBetweenPoints>();
    }

    void Start()
    {
        enemyState = EnemyState.PATROLLING;
    }

    void FixedUpdate()
    {
        AttackOrPatrolControl();

        if(enemyState == EnemyState.PATROLLING)
        {
            patrolBetweenPoints.Patrolling(5);
        }
        if(enemyState == EnemyState.ATTACKING)
        {
            Attack();
        }
    }

    void AttackOrPatrolControl()
    {
        if (b_IsPlayerSeenByEnemy)
        {
            if (b_IsEnemyCanAttack)
            {
                enemyState = EnemyState.ATTACKING;

                // Run the attack animation
                _animator.SetTrigger("WalkingToAttack");

            }
            else
            {
                enemyState = EnemyState.PATROLLING;
            }
        }
        else
        {
            enemyState = EnemyState.PATROLLING;

            // Run the patrol animation again
            _animator.SetTrigger("AttackToWalking");
        }
    }


    void Attack()
    {

    }

    void Die()
    {

    }
}
