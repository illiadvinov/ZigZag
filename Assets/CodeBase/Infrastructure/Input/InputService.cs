using System;
using System.Collections;
using CodeBase.Infrastructure.CoroutineStart;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Infrastructure.Input
{
    public class InputService
    {
        private readonly ICoroutineRunner coroutineRunner;
        public event Action OnClick;
        private Coroutine coroutine;

        [Inject]
        public InputService(ICoroutineRunner coroutineRunner) =>
            this.coroutineRunner = coroutineRunner;

        public void StartInput() =>
            coroutine = coroutineRunner.StartCoroutine(CheckInput());

        public void StopInput()
        {
            if (coroutine != null)
                coroutineRunner.StopCoroutine(coroutine);
        }

        private IEnumerator CheckInput()
        {
            while (true)
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    OnClick?.Invoke();

                yield return null;
            }
        }
    }
}