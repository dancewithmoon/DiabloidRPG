using Model;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private GameObject _playerPrefab;
        
        public PlayerModel PlayerModel => _playerModel;
        public GameObject PlayerPrefab => _playerPrefab;
    }
}