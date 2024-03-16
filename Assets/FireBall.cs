using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 5f;
    public float maxDistance = 30f;

    private float timeAfterCreated = 0f;
    private float maxTime = 5f;
    private Vector3 direction;
    private Vector3 initialPosition;

    [SerializeField]public float baseDamage = 15;
    public float damage;
    public bool damageApplied;

    private void Start()
    {
        damage = baseDamage + Player.main.INT * 5;
    }

    public void SetDirection(Vector3 newDirection)
    {
        newDirection.y = 0;
        newDirection.Normalize();
        direction = newDirection;
        initialPosition = transform.position;
    }

    void Update()
    {
       
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        Extinguish();
    }
    public void Extinguish()
    {
        timeAfterCreated += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, initialPosition);

        if (distance > maxDistance ||  timeAfterCreated >= maxTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!damageApplied && other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(damage);
            damageApplied = true;
        }
    }
}
