using System.Collections;
using Model;
using UnityEngine;
using Zenject;

namespace Behaviours.PlayerBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private Rigidbody _playerBody;

        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        private void Awake()
        {
            _playerBody = GetComponent<Rigidbody>();   
        }

        public void StartMovement(Vector3 target)
        {
            StopMovement();
        
            target.y = transform.position.y;
            transform.LookAt(target);
            StartCoroutine(MoveTo(target));
        }

        public void StopMovement()
        {
            StopAllCoroutines();
        }

        private IEnumerator MoveTo(Vector3 target)
        {
            while (Vector2.Distance(VectorUtils.VectorXZtoXY(transform.position), VectorUtils.VectorXZtoXY(target)) > 0.1f)
            {
                Vector3 nextPosition = Vector3.MoveTowards(transform.position, target, _playerModel.Speed);
                _playerBody.MovePosition(nextPosition);
                yield return null;
            }
        }
    }
}