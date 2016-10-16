using UnityEngine;
using System.Collections;

public class BozoManager : MonoBehaviour
{
    [SerializeField]
    private BozoPowerHealling _healling;
    [SerializeField]
    private BozoPowerCarreta _carreta;
    [SerializeField]
    private BozoPowerStrenght _strenght;
    [SerializeField]
    private BozoPowerShield _shield;

    public GameManager GameManager { get; private set; }

    public void Initialize(GameManager gameManager)
    {
        GameManager = gameManager;
        _healling.Initialize(this);
        _carreta.Initialize(this);
        _strenght.Initialize(this);
        _shield.Initialize(this);
    }
}
