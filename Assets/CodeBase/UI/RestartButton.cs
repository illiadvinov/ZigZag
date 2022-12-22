using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class RestartButton : MonoBehaviour
    {
        private  IStateMachine stateMachine;

        [Inject]
        public void Construct(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Clicked()
        {
            stateMachine.Enter<MenuState>();
        }
    }
}