using CodeBase.Infrastructure.StateMachine.States;

namespace CodeBase.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : IState;
    }
}