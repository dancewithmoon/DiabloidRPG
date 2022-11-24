using Model;
using UnityEngine;
using Zenject;

public class ManualTestScript : MonoBehaviour
{
    [Inject] public PlayerModel PlayerModel { get; set; }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerModel.ApplyDamage(25);
        }   
    }
}
