using UnityEngine;

namespace Controllers
{
    public interface ITapReceiver
    {
        public void Receive(GameObject target, Vector3 tapPosition);
    }
}