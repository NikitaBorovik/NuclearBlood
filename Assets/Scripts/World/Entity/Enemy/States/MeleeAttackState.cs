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
            baseEnemy.Animator.SetBool("IsAttacking", true);
            baseEnemy.RigidBody.mass *= 1000;
            //int index = Random.Range(0, baseEnemy.EnemyData.attackSounds.Count);
            //if (baseEnemy.EnemyData.attackSounds.Count > 0)
            //    baseEnemy.AudioSource.PlayOneShot(baseEnemy.EnemyData.attackSounds[index]);

            baseEnemy.StartCoroutine(MakeAttack((baseEnemy.Target.position - baseEnemy.transform.position).normalized));
        }

        public override void Exit()
        {
            baseEnemy.Animator.SetBool("IsAttacking", false);
            baseEnemy.RigidBody.mass /= 1000;
        }

        public override void Update()
        {
            
        }
        private IEnumerator MakeAttack(Vector3 direction)
        {
            yield return new WaitForSeconds(baseEnemy.EnemyData.timeBetweenAttacks);

            MeleeEnemy meleeEnemy = (MeleeEnemy)baseEnemy;
            meleeEnemy.Attack.SetActive(true);
            meleeEnemy.Attack.transform.right = direction;
            yield return new WaitForSeconds(0.1f);

            meleeEnemy.Attack.SetActive(false);
            stateMachine.ChangeState(baseEnemy.FollowState);
        }
    }
}
