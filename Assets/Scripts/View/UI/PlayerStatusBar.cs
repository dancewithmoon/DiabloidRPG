using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusBar : MonoBehaviour
{
    [SerializeField] private HealthPresenter healthPresenter;
    public HealthPresenter HealthPresenter => healthPresenter;
}
