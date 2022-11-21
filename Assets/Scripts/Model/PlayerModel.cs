using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class PlayerModel
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _healthMax;
        [SerializeField] private int _healthCurrent;
        [SerializeField] private int _manaMax;
        [SerializeField] private int _manaCurrent;
        
        public float Speed => _speed;
        public int HealthMax => _healthMax;
        public int HealthCurrent => _healthCurrent;
        public int ManaMax => _manaMax;
        public int ManaCurrent => _manaCurrent;

        public PlayerModel(float speed, int healthMax, int healthCurrent, int manaMax, int manaCurrent)
        {
            _speed = speed;
            _healthMax = healthMax;
            _healthCurrent = healthCurrent;
            _manaMax = manaMax;
            _manaCurrent = manaCurrent;
        }

        public void ApplyDamage(int amount)
        {
            _healthCurrent -= amount;
        }
        
        public void ApplyHealing(int healing)
        {
            _healthCurrent += healing;
        }
        
        public PlayerModel Clone()
        {
            return new PlayerModel(Speed, HealthMax, HealthCurrent, ManaMax, ManaCurrent);
        }
    }
}