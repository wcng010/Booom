using System.Collections;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace C_Script.UI.ScenesChange
{
    public class ButtonFunction :MonoBehaviour
    {
        [SerializeField]private GameObject setFasleObj;
        [SerializeField] private GameObject setActiveObj;
        public void ExitGame()
        {
            GetComponent<Animator>().SetTrigger("Roll");
            Invoke(nameof(Exit),1);
        }

        private void Exit()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
        }
        
        public void Win()=> StartCoroutine(nameof(BackMainMenu));
        public void Escape()=>StartCoroutine(nameof(EscapeGame));
        public void ReStartGame() => StartCoroutine(nameof(ReStart));

        IEnumerator BackMainMenu()
        {
            ScenesEventCentreManager.Instance.Publish(ScenesEventType.OpenBlackBoard);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(0);
        }

        IEnumerator EscapeGame()
        {
            GetComponent<Animator>().SetTrigger("Roll");
            ScenesEventCentreManager.Instance.Publish(ScenesEventType.OpenBlackBoard);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(0);
        }
        IEnumerator ReStart()
        {
            GetComponent<Animator>().SetTrigger("Attack1");
            ScenesEventCentreManager.Instance.Publish(ScenesEventType.OpenBlackBoard);
            ScenesEventCentreManager.Instance.Publish(ScenesEventType.ClearRecord);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public void PassScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void SetActiveFalse()
        {
            transform.parent.gameObject.SetActive(false);
            InputManager.Instance?.OpenInput();
        }

        public void SetActiveObj()
        {
            setActiveObj.SetActive(true);
            InputManager.Instance?.CloseInput();
        }
        
        public void SetFlaseObj()
        {
            setFasleObj.SetActive(false);
            InputManager.Instance?.OpenInput();
        }
    }
}
