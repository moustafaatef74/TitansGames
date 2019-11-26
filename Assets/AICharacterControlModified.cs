using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacterModified))]
    public class AICharacterControlModified : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacterModified character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public float damping = 2.0f;
        private bool dead;
        public bool inRange;
        public int EnemyRangeLimit = 10;
        private bool hit;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacterModified>();
            dead = GetComponent<EnemyPilot>().dead;
            agent.updateRotation = false;
            agent.updatePosition = true;

        }


        private void Update()
        {
            if (!dead)
            {
                if (target != null)
                    agent.SetDestination(target.position);

                if (agent.remainingDistance <= EnemyRangeLimit)
                {
                    var lookPos = target.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        character.Move(agent.desiredVelocity, false, false, false, false);
                        inRange = false;
                    }
                    else
                    {
                        character.Move(Vector3.zero, false, false, true, false);
                        inRange = true;
                    }
                }
                else
                {
                    var lookPos = target.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                    character.Move(Vector3.zero, false, false,false, false);
                }
                if (hit)
                {
                    character.Move(Vector3.zero, false, false, false, true);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }   
    }
}
