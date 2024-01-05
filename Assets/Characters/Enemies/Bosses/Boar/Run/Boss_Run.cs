using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1f;
    public float attackDelay = 1.5f; // Adjust the delay time as needed

    private Boss boss;
    private Transform player;
    private Rigidbody2D rigidbody;
    private float attackTimer = 0f;
    private bool hasAttacked = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        rigidbody = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            return;

        boss.LookAtPlayer();

        Vector2 target = player.position;
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, speed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);

        if (!hasAttacked && Vector2.Distance(player.position, rigidbody.position) <= attackRange)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackDelay)
            {
                animator.SetTrigger("Attack");
                hasAttacked = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        attackTimer = 0f;
        hasAttacked = false;
    }
}
