using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : Ship
{
    public float moveSpeed;
    [Min(0.01f)]
    public float drag;

    public Vector3 velocity;

    public AudioClip rollSound;

    Animator anim;
    Coroutine rolling;

    StandardControls input;

    Vector2 moveAxis;
    bool fireDown;
    float rollAxis;

    new private void Awake()
    {
        base.Awake();
        input = new StandardControls();

        input.Ship.Fire.performed += context => Fire(true);
        input.Ship.Fire.canceled += context => Fire(false);

        input.Ship.Move.performed += context => moveAxis = context.ReadValue<Vector2>();
        input.Ship.Move.canceled += context => moveAxis = Vector2.zero;

        input.Ship.Roll.performed += context => rollAxis = context.ReadValue<float>();
        input.Ship.Roll.canceled += context => rollAxis = 0f;

        input.Ship.Pause.performed += context => Game.Pause();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody.inertiaTensorRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (alive && !Game.isPaused)
        {
            if (firing == null && fireDown)
            {
                firing = StartCoroutine(BasicFire(new Vector3(0f, 0f, 1f)));
            }
        }
    }

    private void LateUpdate()
    {
        if (!IsRolling() && alive && !Game.isPaused)
        {
            if (rollAxis < 0)
            {
                rolling = StartCoroutine(Roll(-1));

            }
            if (rollAxis > 0)
            {
                rolling = StartCoroutine(Roll(1));
            }
        }
    }

    private void FixedUpdate()
    {
        if (alive && !Game.isPaused)
        {
            velocity += new Vector3(moveAxis.x, 0, moveAxis.y).normalized;
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

    void Fire(bool fireEnable)
    {
        fireDown = fireEnable;
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

    private void OnEnable()
    {
        input.Ship.Enable();
    }

    private void OnDisable()
    {
        input.Ship.Disable();
    }
}
