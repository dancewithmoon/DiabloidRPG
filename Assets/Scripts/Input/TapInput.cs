using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TapInput : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    public SignalBus SignalBus { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000))
        {
            var targetPosition = hit.point;
            SignalBus.Fire(new UserTouchSignal() { touchPosition = targetPosition, touchedObject = hit.transform.gameObject });
        }
    }

    public void Disable()
    {
        enabled = false;
    }
}

