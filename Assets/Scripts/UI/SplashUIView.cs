using UnityEngine;

namespace UI
{
    public class SplashUIView : MonoBehaviour
    {
        public void ShowLogo(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}