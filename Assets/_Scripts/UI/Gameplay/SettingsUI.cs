using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class SettingsUI : MonoBehaviour
    {
        public void ActivateSettings()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        
        public void DeactivateSettings()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void ExitGame()
        {
             #if UNITY_EDITOR
                         UnityEditor.EditorApplication.isPlaying = false;
             #else
                 Application.Quit();
             #endif   
        }
    }
}
