using C_Script.Common.Model.EventCentre;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace C_Script.UI.ScenesChange
{
    public class ButtonFunction :MonoBehaviour
    {
        public void ExitGame()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
        
        public void ReStartGame()
        {
            ScenesEventCentreManager.Instance.Publish(ScenesEventType.ClearRecord);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public void PassScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
