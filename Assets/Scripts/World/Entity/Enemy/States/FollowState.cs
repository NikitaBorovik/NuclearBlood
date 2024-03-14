using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy.States
{
    public class FollowState : BaseEnemyState
    {
        public FollowState(BaseEnemy baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine) { }

        public override void Enter()
        {
            baseEnemy.Animator.SetBool("MovingRight", false);
            baseEnemy.Animator.SetBool("MovingLeft", true);
        }

        public override void Exit()
        {
            baseEnemy.RigidBody.velocity = Vector2.zero;
        }

        public override void Update()
        {
            if ((baseEnemy.Target.position - baseEnemy.transform.position).magnitude <= baseEnemy.EnemyData.attackRange)
            {
                stateMachine.ChangeState(baseEnemy.AttackState);
            }
            else
            {
                baseEnemy.RigidBody.velocity = (baseEnemy.Target.position - baseEnemy.transform.position).normalized * baseEnemy.EnemyData.speed;
                SetMoveAnimationParams(baseEnemy.RigidBody.velocity.x);
            }
        }

        private void SetMoveAnimationParams(float vx)
        {
            if (vx < 0)
            {
                baseEnemy.Animator.SetBool("MovingRight", false);
                baseEnemy.Animator.SetBool("MovingLeft", true);
            }
            else
            {
                baseEnemy.Animator.SetBool("MovingRight", true);
                baseEnemy.Animator.SetBool("MovingLeft", false);
            }
        }
    }
}
