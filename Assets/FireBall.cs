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


    

    // Set the direction of the fireball
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
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("coLLÝDED WÝTH ENEMY");
            damage = baseDamage + Player.main.INT * 5;
            other.GetComponent<stone_enemy_sc>().TakeDamage(damage);
        }
    }

}
