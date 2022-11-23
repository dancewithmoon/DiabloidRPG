using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UserInput
{
    public class RaycastTouchInput : MonoBehaviour, IPointerClickHandler, ITouchInput
    {
        private Camera _raycasterCamera;

        public event Action<GameObject, Vector3> OnTouch;

        [Inject]
        private void Construct(Camera raycasterCamera)
        {
            _raycasterCamera = raycasterCamera;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Ray ray = _raycasterCamera.ScreenPointToRay(eventData.position);
            
            if(Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                OnTouch?.Invoke(hit.transform.gameObject, hit.point);
            }
        }
    }
}

