using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScriptPos : MonoBehaviour
{
    private Transform target;
    private GameObject player;

    // Moving variables
    public float speed = 3;
    public float stoppingDistance = 3;
    public bool notStuck = true;

    // Health variables
    public int maxHealth = 30;
    public int currentHealth;

    // Attack related variables
    public int damageDealt = 10;
    private float nextAttackTime = 0f;
    public float attackRate = 1f;
    public float attackRange = 3.5f;

    public bool addScore;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Follow player and prevent overlapping with player
        if (notStuck)
        {
            FollowPlayer();
        }


        // Deal damage
        if (Vector3.Distance(transform.position, target.position) <= attackRange && Time.time >= nextAttackTime)
        {
            player.GetComponent<PlayerController>().TakeDamage(damageDealt);
            nextAttackTime = Time.time + 1f / attackRate;
        }

        // Destroy enemy if out of bounds
        
    }

    // Take damage from bullet hits and destroy enemy when health reaches 0
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            addScore = true;
            addScore = false;
            notStuck = false;
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}
