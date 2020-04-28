using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : Ship
{
    /// <summary> The maximum velocity of the ship. </summary>
    public float moveSpeed;

    /// <summary> The constant drag applied to the ship. </summary>
    [Min(0.01f)]
    public float drag;

    /// <summary></summary>
    public Vector3 velocity;

    public AudioClip rollSound;

    Animator anim;
    Coroutine rolling;

    StandardControls input;

    Vector2 moveAxis;
    bool fireDown;
    float rollAxis;

    /// <summary>
    /// Initialise the ship and setup the controls
    /// </summary>
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
        anim.enabled = false;
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

    /// <summary>
    /// Move the ship with a Vector3.
    /// </summary>
    /// <param name="velocity"></param>
    void Move(Vector3 velocity)
    {
        gameObject.transform.Translate(velocity * Time.deltaTime * moveSpeed, Space.World);
    }


    /// <summary>
    /// Roll the ship left or right. Negative values roll left, positive values roll right.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    IEnumerator Roll(float direction)
    {
        anim.enabled = true;
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
        anim.enabled = false;
        rolling = null;
    }

    void Fire(bool fireEnable)
    {
        fireDown = fireEnable;
    }

    /// <summary>
    /// Check if the ship is rolling.
    /// </summary>
    /// <returns></returns>
    public bool IsRolling()
    {
        if (rolling == null)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Called when the ship collides with something.
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// Enable input for the ship.
    /// </summary>
    private void OnEnable()
    {
        input.Ship.Enable();
    }

    /// <summary>
    /// Disable input for the ship.
    /// </summary>
    private void OnDisable()
    {
        input.Ship.Disable();
    }
}
