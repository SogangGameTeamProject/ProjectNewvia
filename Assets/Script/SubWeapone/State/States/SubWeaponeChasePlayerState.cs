using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public class SubWeaponeChasePlayerState : SubWeaponeStateBase
    {
        private NavMeshAgent agent;


        public override void Enter(SubWeapone subWeapone)
        {
            base.Enter(subWeapone);
            if (agent == null)
                agent = subWeapone.GetComponent<NavMeshAgent>();
        }
        public override void Exit()
        {
            agent.velocity = Vector3.zero;
        }

        public override void StateUpdate()
        {
            //플레이어 추적
            if (agent && _subWeapone._playerController)
            {
                agent.SetDestination(_subWeapone._playerController._subWeaponMountingLocation.position);
            }
            else
                _subWeapone.StateTransition(SubWeaponeStateType.Idle);
        }
    }
}