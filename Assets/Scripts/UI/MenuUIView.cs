using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIView : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    
    private Subject<Unit> _restartClicked = new();
    public Observable<Unit> RestartClicked => _restartClicked;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            _restartClicked.OnNext(Unit.Default);
        });
    }
    
    public void ShowMainMenu(bool show)
    {
        gameObject.SetActive(show);
    }
}
