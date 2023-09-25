using System.Collections.Generic;
using C_Script.Common.Model.EventCentre;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace C_Script.UI.Card
{
    public abstract class Card:MonoBehaviour
    {
        [SerializeField] protected List<Sprite> cardSprite = new List<Sprite>();
        [SerializeField]private GameObject cardImage;
        protected Animator MyAnimator;
        protected CardManager CardManager;
        protected Image MyImage;
        protected Button MyButton;
        protected int Value;
        protected static readonly int Flip = Animator.StringToHash("Flip");
        

        protected virtual void Awake()
        {
            MyImage = GetComponent<Image>();
            MyButton = GetComponent<Button>();
            MyAnimator = GetComponent<Animator>();
            CardManager = GetComponentInParent<CardManager>();
        }

        public abstract void OnButtonClick();
        protected virtual void CollectSprite()
        {
            int length = GameManager.Instance.cardRecord.AngleCard.Count;
            for (int i = 0; i < length; ++i)
            {
                GameObject obj = Instantiate(cardImage, transform.parent.parent);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-922.6f + 76.8f*i,-487.4f, 0);
                obj.GetComponent<Image>().sprite = GameManager.Instance.cardRecord.AngleCard[i];
            }
            
            length = GameManager.Instance.cardRecord.DemonCard.Count;
            for (int i = 0; i < length; ++i)
            {
                GameObject obj = Instantiate(cardImage, transform.parent.parent);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-922.6f + 76.8f*i,-380.6f, 0);
                obj.GetComponent<Image>().sprite = GameManager.Instance.cardRecord.DemonCard[i];
            }
        }
        
        protected abstract void Function1();

        protected abstract void Function2();

        protected abstract void Function3();

        protected abstract void Function4();

        protected abstract void Function5();

        protected abstract void Function6();
        
        protected abstract void Function7();
        
    }
}