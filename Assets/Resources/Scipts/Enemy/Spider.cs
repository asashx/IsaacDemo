using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyBehaviour
{
    private bool arrived = true;        // 是否到达目标位置

    protected override void PerformMovement()
    {
        animator.SetBool("move", true);
        // 获取玩家的位置
        Vector3 playerPosition = player.position - new Vector3(0f, 0.8f, 0f);

        // 计算目标位置，可以加上一些偏移值
        Vector3 targetPosition = playerPosition + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance < detectRange && arrived == true)
        {
            arrived = false;

            Vector3 moveVector = (targetPosition - transform.position).normalized;

            StartCoroutine(MoveToTarget(targetPosition, moveVector));
        }
        else if (distance >= detectRange && arrived == true)
        {
            arrived = false;

            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

            Vector3 moveVector = (randomOffset - transform.position).normalized;

            StartCoroutine(MoveToTarget(randomOffset, moveVector));
        }
    }

    // 移动到目标位置
    IEnumerator MoveToTarget(Vector3 targetPosition, Vector3 moveVector)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        float startTime = Time.time;

        while (distance > 0.1f && Time.time - startTime < 3f)
        {
            rb.velocity = moveVector * moveSpeed;

            yield return null;

            distance = Vector3.Distance(transform.position, targetPosition);
        }

        rb.velocity = Vector3.zero;

        arrived = true;

        animator.SetBool("move", false);
    }

}
