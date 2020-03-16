using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShip : Ship
{
    public float forwardSpeed;
    public float horizontalSpeed;

    bool initialized = false;
    float direction = 0;

    Ship target;

    private void Start()
    {
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        target = FindObjectOfType<PlayerShip>();
    }

    private void Update()
    {
        if (alive)
        {
            if (initialized)
            {
                Vector3 velocity = new Vector3(0, 0, -forwardSpeed * Time.deltaTime);

                if (target.IsAlive())
                {
                    // Target the players position
                    //velocity.x = Mathf.Lerp(velocity.x, target.transform.position.x - transform.position.x, chaseSpeed * Time.deltaTime);

                    // Random movement
                    velocity.x = direction * horizontalSpeed;

                    if (firing == null)
                    {
                        firing = Fire();
                    }
                }

                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, transform.position.z), Time.deltaTime * 5);
                if (transform.position.x < 5 && transform.position.x > -5)
                {
                    StartCoroutine(SteerAI());
                    initialized = true;
                }
            }

            SelfRight(180f);
        }
    }

    IEnumerator SteerAI()
    {
        while (alive)
        {
            if (direction < 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            yield return new WaitForSeconds(Random.Range(0.25f, 1.5f));
        }
    }

    public abstract Coroutine Fire();
}
