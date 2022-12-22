using System;
using UnityEngine;

namespace CodeBase.Player
{
    public abstract class Mover
    {
        public abstract void Move(float deltaTime, float speed);
        public abstract void Reset();
    }
}