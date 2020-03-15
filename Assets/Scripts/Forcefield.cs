using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Forcefield : MonoBehaviour
{
    public Vector3 forceDirection;
    public float forceMultiplier;

    float forceStrength = 0f;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        forceDirection = forceDirection.normalized;
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            c.GetComponent<PlayerShip>().velocity += forceDirection * 1.5f;
            c.transform.position += forceDirection * forceStrength;
            forceStrength += Time.deltaTime * forceMultiplier;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            forceStrength = 0f;
        }
    }
}
