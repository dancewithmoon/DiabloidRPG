using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject(Id = "playerHealthCurrent")]
    private int HealthCurrent { get; set; }

    [Inject(Id = "playerHealthMax")]
    private int HealthMax { get; set; }

    [Inject(Id = "playerMana")]
    private int Mana { get; set; }

    [Inject]
    private SignalBus SignalBus { get; set; }

    private void Start()
    {
        SignalBus.Fire(new UpdatePlayerHealthSignal() { healthCurrent = HealthCurrent, healthMax = HealthMax });
    }

    public void ApplyDamage(ApplyPlayerDamageSignal signal)
    {
        HealthCurrent -= signal.damage;
        SignalBus.Fire(new UpdatePlayerHealthSignal() { healthCurrent = HealthCurrent, healthMax = HealthMax });
        if(HealthCurrent <= 0)
        {
            SignalBus.Fire(new PlayerDiedSignal());
        }
    }

    public void ApplyHealing(int healing)
    {
        HealthCurrent += healing;
    }
}
