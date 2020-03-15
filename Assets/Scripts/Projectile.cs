using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float persistanceTime = 3f;

    public AudioClip shootSound;

    string firedTag;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        persistanceTime -= Time.deltaTime;
        if (persistanceTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.AddForce(velocity);
    }

    public void SetParent(Transform firedTransform)
    {
        firedTag = firedTransform.tag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ship>() != null)
        {
            if (collision.transform.tag != firedTag)
            {
                collision.gameObject.GetComponent<Ship>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
