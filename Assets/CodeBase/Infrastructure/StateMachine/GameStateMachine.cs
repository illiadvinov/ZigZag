using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private Dictionary<Type, IState> states;
        private IState currentState;

        [Inject]
        public void Construct(
            [Inject(Id = "InitializeState")] IState initialize,
            [Inject(Id = "GamePlayState")] IState gamePlay,
            [Inject(Id = "FinalState")] IState final,
            [Inject(Id = "MenuState")] IState menu)
        {
            states = new Dictionary<Type, IState>
            {
                [typeof(InitializeState)] = initialize,
                [typeof(GamePlayState)] = gamePlay,
                [typeof(FinalState)] = final,
                [typeof(MenuState)] = menu
            };
        }

        public void Enter<TState>() where TState : IState
        {
            currentState?.Exit();
            IState state = states[typeof(TState)];
            currentState = state;
            state.Enter();
        }
    }
}