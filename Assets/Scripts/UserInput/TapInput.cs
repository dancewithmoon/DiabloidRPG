using System;
using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UserInput
{
    public class TapInput : MonoBehaviour, IPointerClickHandler
    {
        private Camera _mainCamera;
        private ITapReceiver _tapReceiver;

        [Inject]
        private void Construct(ITapReceiver tapReceiver)
        {
            _mainCamera = Camera.main;
            _tapReceiver = tapReceiver;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Ray ray = _mainCamera.ScreenPointToRay(eventData.position);
            
            if(Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                _tapReceiver.Receive(hit.transform.gameObject, hit.point);
            }
        }
    }
}

