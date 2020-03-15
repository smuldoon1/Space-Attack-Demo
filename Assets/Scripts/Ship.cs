using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Ship : MonoBehaviour
{
    public float health;
    public float fireRate;
    public Projectile bullet;

    Vector3 rightPosition;
    Vector3 rightRotation;
    public float selfRightStrength = 1;

    protected bool alive = true;

    public Coroutine firing;

    protected AudioSource audioSource;
    protected Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rigidBody.useGravity = false;
    }

    protected IEnumerator BasicFire()
    {
        Projectile newBullet = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity);
        newBullet.SetVelocity(transform.forward * newBullet.speed);
        newBullet.SetParent(transform);
        audioSource.PlayOneShot(newBullet.shootSound);
        yield return new WaitForSeconds(fireRate);
        firing = null;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(DestroyShip());
        }
    }

    public IEnumerator DestroyShip()
    {
        alive = false;
        yield return new WaitForEndOfFrame();
        rigidBody.drag = 0;
        rigidBody.angularDrag = 0.05f;
        rigidBody.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * 1000f, ForceMode.Acceleration);
        rigidBody.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 1000f, ForceMode.Acceleration);
        if (gameObject.tag == "Player")
        {
            yield return new WaitForSeconds(3f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void SelfRight(float yRotation)
    {
        rightPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        rightRotation = new Vector3(0f, yRotation, 0f);
        transform.position = Vector3.Lerp(transform.position, rightPosition, selfRightStrength * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rightRotation), selfRightStrength * Time.deltaTime);
    }

    public bool IsAlive()
    {
        return alive;
    }
}
