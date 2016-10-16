using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform _batmanPosition;
    [SerializeField]
    private int _clawnQuantity = 5;
    [SerializeField]
    private ClawnManager _clawPrefab;
    [SerializeField]
    private BozoManager _bozoManager;
    [SerializeField]
    private BatmanManager _batmanManager;

    private Timer _timer { get { return GetComponent<Timer>(); } }

    private List<ClawnManager> _clawns = new List<ClawnManager>();
    public List<ClawnManager> Claws { get { return _clawns; } }

    private bool _gameEnded, _batmanDiedLaunch;

    private void Start()
    {
        _timer.StartCounting();
        _timer.onEnd.AddListener(EndTime);
        _bozoManager.Initialize(this);
        _batmanManager.Initialize(this);
        for (int i = 0; i < _clawnQuantity; i++)
        {
            _clawns.Add(AddClawn(Vector3.zero));
        }
    }

    private void LateUpdate()
    {
        if (_gameEnded) return;
        //if (_batmanPosition != null)
        //{
        //    for (int i = 0; i < _clawns.Count; i++)
        //        _clawns[i].BatmanPosition = _batmanPosition.position;
        //}

        if (_clawns.Count <= 0)
        {
            Debug.Log("Batman Wins");
            SceneManager.LoadScene("sceneEndGameWin");
        }
    }

    private bool ClawnsOnScreen()
    {
        for (int i = 0; i < _clawns.Count; i++)
        {
            if (_clawns[i].IsOnScreen)
                return true;
        }
        return false;
    }

    public ClawnManager AddClawn(Vector3 position)
    {
        ClawnManager clawn = Instantiate(_clawPrefab, position + Vector3.back, Quaternion.identity) as ClawnManager;
        clawn.Initialize(this);
        clawn.BatmanRef = _batmanManager.transform;
        return clawn;
    }

    public void ClawDied(ClawnManager clawn)
    {
        _clawns.Remove(clawn);
    }

    private void EndTime(float time)
    {
        if (_clawns.Count > 0)
        {
            if (ClawnsOnScreen())
            {
                Debug.Log("Batman Loses");
                SceneManager.LoadScene("sceneEndGameLose");
            }
            else
            {
                Debug.Log("Batman Wins - Clawns is out of sight");
                SceneManager.LoadScene("sceneEndGameWin");
            }
        }
    }

    public void BatmanDied()
    {
        if (_batmanDiedLaunch) return;
        _batmanDiedLaunch = true;
        Invoke("EndGame", 1);
    }

    private void EndGame()
    {
        _gameEnded = true;
        _timer.Stop();
        _bozoManager.gameObject.SetActive(false);
        _batmanManager.gameObject.SetActive(false);
        for (int i = 0; i < _clawns.Count; i++)
        {
            Destroy(_clawns[i].gameObject);
        }
        _clawns = new List<ClawnManager>();

        SceneManager.LoadScene("sceneEndGameLose");
    }
}
