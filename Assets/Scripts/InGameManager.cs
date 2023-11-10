using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    static InGameManager _instance;
    public static InGameManager Instance { get => _instance; private set => _instance = value; }
    event Action _updateAction;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        _updateAction?.Invoke();
    }

    public void SetUpdateAction(Action action)
    {
        _updateAction += action;
    }
}
