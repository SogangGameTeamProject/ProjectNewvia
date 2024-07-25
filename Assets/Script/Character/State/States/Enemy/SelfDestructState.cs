using Newvia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructState : EnemyDeathState
{
    public GameObject explosionPre = null;
    public override void Enter(CharacterInit character)
    {
        base.Enter(character);

        if (explosionPre != null)
        {
            Instantiate(explosionPre, this.transform.position, Quaternion.identity, null);
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
