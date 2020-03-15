using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : EnemyShip
{
    public override Coroutine Fire()
    {
        return StartCoroutine(BasicFire());
    }
}
