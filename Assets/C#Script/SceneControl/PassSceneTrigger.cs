using System;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;


namespace C_Script.SceneControl
{
    public class PassSceneTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (String.Compare(col.gameObject.tag, "Player", StringComparison.Ordinal) == 0/*&&GameManager.Instance.enemyNum==0*/)
            {
                ScenesEventCentreManager.Instance.Publish(ScenesEventType.Loop);
                if (PlayerPrefs.HasKey("LoopCount"))
                {
                    int count = PlayerPrefs.GetInt("LoopCount");
                    PlayerPrefs.SetInt("LoopCount",count+1);
                }
                else
                {
                    PlayerPrefs.SetInt("LoopCount",0);
                }
            }
        }
    }
}
