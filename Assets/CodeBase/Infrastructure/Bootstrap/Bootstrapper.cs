using System;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private IStateMachine stateMachine;

        [Inject]
        public void Construct(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        private void Awake()
        {
            stateMachine.Enter<InitializeState>();
        }
    }
}