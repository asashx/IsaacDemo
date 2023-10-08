using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clotty : EnemyBehaviour
{
    public GameObject attackPrefab;
    public GameObject tearPrefab;
    private Transform eyesPos;

    protected override void Start()
    {
        base.Start();

        eyesPos = transform.Find("Eyes");
    }

    protected override void Update()
    {
        base.Update();

        InvokeRepeating("MoveToRandomTarget", 0f, moveInterval);
    }

    private void MoveToRandomTarget()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        Vector3 moveVector = (randomOffset - transform.position).normalized;
        StartCoroutine(MoveToTarget(randomOffset, moveVector));
    }

    IEnumerator MoveToTarget(Vector3 targetPosition, Vector3 moveVector)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        float startTime = Time.time;

        while (Time.time - startTime < moveInterval)
        {
            rb.velocity = moveVector * moveSpeed;

            yield return null;

            distance = Vector3.Distance(transform.position, targetPosition);
        }

        rb.velocity = Vector3.zero;
    }

    protected override void Attack()
    {
        Shoot(Vector2.up);
        Shoot(Vector2.left);
        Shoot(Vector2.right);
        Shoot(Vector2.down);
        AttackEffect();
    }

    private void Shoot(Vector2 direction)
    {
        GameObject tear = ObjectPool.Instance.GetObject(tearPrefab);
        tear.transform.position = eyesPos.position;

        tear.GetComponent<BloodTears>().SetSpeed(direction);

        if (direction == Vector2.down)
        {
            tear.GetComponent<SpriteRenderer>().sortingLayerName = "AbovePlayer";
        }
    }

    private void AttackEffect()
    {
        GameObject attack = ObjectPool.Instance.GetObject(attackPrefab);
        attack.transform.position = eyesPos.position;
    }
}
