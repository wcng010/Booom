using System;
using System.Collections;
using C_Script.Common.Model.EventCentre;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


namespace C_Script.SceneControl
{
    public class ScenesController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector scenePassTimeline;

        private void OnEnable()
        {
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.LevelPass,ScenePass);
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.OpenBlackBoard,BlackBoard);
        }

        private void OnDisable()
        {
            ScenesEventCentreManager.Instance.Unsubscribe(ScenesEventType.LevelPass,ScenePass);
            ScenesEventCentreManager.Instance.Unsubscribe(ScenesEventType.OpenBlackBoard,BlackBoard);
        }
        private void ScenePass()
        {
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStop);
            scenePassTimeline.Play();
            StartCoroutine(WaitTimeline(scenePassTimeline));
        }

        private void BlackBoard()
        {
            scenePassTimeline.Play();
        }

        IEnumerator WaitTimeline(PlayableDirector director)
        {
            yield return new WaitUntil(() => director.state == PlayState.Paused);
            var option = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            while (!option.isDone)
            {
                yield return null;
            }
        }
    }
}
