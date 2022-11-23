using System;
using UnityEngine;

namespace UserInput
{
    public interface ITouchInput
    {
        public event Action<GameObject, Vector3> OnTouch;
    }
}