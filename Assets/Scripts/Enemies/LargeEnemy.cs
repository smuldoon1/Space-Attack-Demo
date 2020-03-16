using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : EnemyShip
{
    public override Coroutine Fire()
    {
        return StartCoroutine(BasicFire(new Vector3(-0.5f, 0f, -1f), new Vector3(0.5f, 0f, -1f)));
    }
}
