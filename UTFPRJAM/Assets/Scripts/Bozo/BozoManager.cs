using UnityEngine;
using System.Collections;

public class BozoManager : MonoBehaviour
{
    [SerializeField]
    private BozoPowerHealling _healling;

    public GameManager GameManager { get; private set; }

    public void Initialize(GameManager gameManager)
    {
        GameManager = gameManager;
        _healling.Initialize(this);
    }
}
