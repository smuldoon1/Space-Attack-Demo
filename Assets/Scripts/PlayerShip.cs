using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public float moveSpeed;
    [Min(0.01f)]
    public float drag;

    public Vector3 velocity;

    public AudioClip rollSound;

    Animator anim;
    Coroutine rolling;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody.inertiaTensorRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (alive)
        {
            Vector3 inputVelocity = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
            {
                inputVelocity.x -= 0.5f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVelocity.x += 0.5f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                inputVelocity.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVelocity.z -= 1;
            }

            if (!IsRolling())
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    rolling = StartCoroutine(Roll(-1));
                    inputVelocity.x -= 100;

                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rolling = StartCoroutine(Roll(1));
                    inputVelocity.x += 100;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (firing == null)
                {
                    firing = StartCoroutine(BasicFire());
                }
            }

            velocity += inputVelocity.normalized;
            Move(velocity);
            velocity /= drag;

            SelfRight(0f);
        }
    }

    void Move(Vector3 velocity)
    {
        gameObject.transform.Translate(velocity * Time.deltaTime * moveSpeed, Space.World);
    }

    IEnumerator Roll(float direction)
    {
        if (direction < 0)
        {
            anim.Play("RollLeft");
        }
        else
        {
            anim.Play("RollRight");
        }
        //audioSource.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.7f);
        rolling = null;
    }

    public bool IsRolling()
    {
        if (rolling == null)
        {
            return false;
        }
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (IsRolling())
            {
                collision.gameObject.GetComponent<EnemyShip>().TakeDamage(100);
            }
            else
            {
                TakeDamage(100);
            }
        }
    }
}
