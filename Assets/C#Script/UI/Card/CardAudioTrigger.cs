using C_Script.Manager;
using UnityEngine;

namespace C_Script.UI.Card
{
    public class CardAudioTrigger : MonoBehaviour
    {
        public void CardFlipAudioPlay() => AudioManager.Instance.CardFlipPlay();
    }
}
