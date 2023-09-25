using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace C_Script.UI.Card
{
    public class CardManager : MonoBehaviour
    {
        [NonSerialized]public int ClickCount;
        public GameObject angelCard;
        public GameObject demonCard;
        private void Awake()
        {
            for (int i = 0; i < 3; ++i)
            {
                GameObject card;
                if (Random.value > 0.5)
                {
                   card = Instantiate(angelCard, transform);
                }
                else
                {
                   card = Instantiate(demonCard, transform);
                }
                card.GetComponent<RectTransform>().anchoredPosition = new Vector3(350*(i-1),0,0);
            }
        }
    }
}
