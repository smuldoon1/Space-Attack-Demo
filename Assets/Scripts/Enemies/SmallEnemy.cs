using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : EnemyShip
{
    public override Coroutine Fire()
    {
        return StartCoroutine(BasicFire(new Vector3(0f, 0f, -1f)));
    }
}
