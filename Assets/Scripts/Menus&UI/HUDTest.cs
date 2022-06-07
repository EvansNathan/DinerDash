using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUDTest : MonoBehaviour
    {

        public Text tipsAmount;
        public Text interactText;
        public Image progressBar;
        public float animationSpeed;
        public float sizeModifier;
        public float locationModifier;
        public float fillSpeed;

        private float currentTips = 0;
        private float desiredTips = 0;
        private Vector3 initalPosition;

        private void Start()
        {
            currentTips = desiredTips = 0;
            initalPosition = tipsAmount.rectTransform.transform.position;
            tipsAmount.text = (currentTips.ToString("c2"));
        }

        void Update()
        {
            if(Input.GetKeyUp(KeyCode.C))
            {
                desiredTips += 1;
                Debug.Log("ADD TIPS");
            }

            if (currentTips < desiredTips)
            {
                currentTips += animationSpeed * Time.deltaTime;
                tipsAmount.rectTransform.localScale += Vector3.one * sizeModifier * Time.deltaTime;
                tipsAmount.rectTransform.transform.position += Vector3.one * -locationModifier * Time.deltaTime;
                Debug.Log(currentTips);
            }
            else
            {
                tipsAmount.rectTransform.localScale = Vector3.one;
                tipsAmount.rectTransform.transform.position = initalPosition;
                currentTips = desiredTips;
            }

            tipsAmount.text = (currentTips.ToString("c2"));
            progressBar.fillAmount -= fillSpeed * Time.deltaTime;
        }

        public void setInteractText(string message)
        {
            
        }
    }
}

