using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.World.Entity.Enemy.States
{
    public class MeleeAttackState : BaseEnemyState
    {
        public MeleeAttackState(BaseEnemy baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Attacking");
            baseEnemy.RigidBody.mass *= 1000;
            baseEnemy.StartCoroutine(MakeAttack((baseEnemy.Target.position - baseEnemy.transform.position).normalized));
        }

        public override void Exit()
        {
            baseEnemy.RigidBody.mass /= 1000;
        }

        public override void Update()
        {
            
        }
        private IEnumerator MakeAttack(Vector3 direction)
        {
            yield return new WaitForSeconds(1f); //Temporary
            stateMachine.ChangeState(baseEnemy.FollowState);
        }
    }
}
