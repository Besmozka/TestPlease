using System;
using UnityEngine;
using UnityEngine.UI;
using R3;
using VContainer;

namespace UI
{
    public class LoadUIView : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        private LoadState _loadState;
        private IDisposable _subscription;

        [Inject]
        public void Construct(LoadState loadState)
        {
            _loadState = loadState;

            _subscription = _loadState.Progress
                .Subscribe(FillProgressBar);
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }

        public void Show(bool show)
        {
            progressBar.gameObject.SetActive(show);
        }

        private void FillProgressBar(float progress)
        {
            Debug.Log($"progress {progress}");
            progressBar.fillAmount = progress;
        }
    }
}