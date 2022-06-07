using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Menus_UI
{
    public class UIMgr : MonoBehaviour
    {
        public static UIMgr Instance;

        public Text tipsAmount;
        public Text tipsAmountDS;
        public Text stolenTipsAmount;
        public Text stolenTipsAmountDS;
        public Text dashIndicator;
        public Text dashIndicatorDS;
        public GameObject dashGroup;
        public Text clockText;
        public Text clockTextDS;
        public GameObject tipsAmountGroup;
        public Text tableLine1;
        public Text tableLine2;
        public Text tableLine3;
        public Text interactText;
        public GameObject stolenGroup;

        public Image[] tableHappiness;
        public Sprite happyImage;
        public Sprite sadImage;

        public AudioClip tipSound;

        public Sprite[] foodImages;
        public Sprite transparentImage;

        public int tableLineCount;
    
        public float animationSpeed;
        public float sizeModifier;
        public float locationModifier;
        public float happinessDecay;

        public float currentTips = 0;
        public float desiredTips = 0;
        private Vector3 initalPosition;

        private int hour;
        private int min = 0;
        private string timeOfDay = "PM";

        public bool isWaiting;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
        }

        private void Start()
        {
            hour = GameState.Instance.shiftStart;
            StartCoroutine(HappinessTimer());
            
            GameState.Instance.StartLevel();
        
            tableLine1.text = "";
            tableLine2.text = "";
            tableLine3.text = "";
            tableLineCount = 0;

            currentTips = desiredTips = 0;
            initalPosition = tipsAmount.rectTransform.transform.position;
            tipsAmount.text = tipsAmountDS.text = (currentTips.ToString("c2"));

            StartCoroutine(Clock());

            Debug.Log(dashGroup.transform.position);
        }

        private void Update()
        {
            if (currentTips < desiredTips)
            {
                currentTips += animationSpeed * Time.deltaTime;
                tipsAmountGroup.transform.localScale += Vector3.one * sizeModifier * Time.deltaTime;
                tipsAmountGroup.transform.transform.position += Vector3.one * -locationModifier * Time.deltaTime;
                if (tipsAmountGroup.transform.transform.localPosition.x < 630)
                {
                    tipsAmountGroup.transform.transform.localPosition = new Vector3(630, 330, tipsAmountGroup.transform.transform.localPosition.z);
                }
                if (tipsAmountGroup.transform.localScale.x > 3)
                {
                    tipsAmountGroup.transform.localScale = Vector3.one * 3;
                }
            }
            else
            {
                tipsAmountGroup.transform.localScale = Vector3.one;
                tipsAmountGroup.transform.transform.position = initalPosition;
                currentTips = desiredTips;
            }

            tipsAmount.text = tipsAmountDS.text = (currentTips.ToString("c2"));
        }

        public bool SendTable(string tableName)
        {
            if (tableLineCount == 0)
            {
                tableLine1.text = tableName;
            }
            else if (tableLineCount == 1)
            {
                tableLine2.text = tableName;
            }
            else if (tableLineCount == 2)
            {
                tableLine3.text = tableName;
            }
            else
            {
                Debug.Log("Holding Too Many Tickets");
                return false;
            }

            tableLineCount++;
            return true;
        }

        public void ResetHeldTickets()
        {
            tableLine1.text = "";
            tableLine2.text = "";
            tableLine3.text = "";

            tableLineCount = 0;
        }

        public void SetInteractText(string message)
        {
            interactText.text = message;
        }
    
        private IEnumerator Clock()
        {
            string hourString = hour.ToString();
            string minString;

            if (min == 0)
            {
                minString = "00";
            }
            else
            {
                minString = min.ToString();
            }
        
            clockText.text = clockTextDS.text = hourString + ":" + minString + " " + timeOfDay;
            yield return new WaitForSeconds(5);

            min += 15;

            if (min == 60)
            {
                hour += 1;
                min = 0;
            }

            GameState.Instance.UpdateTime(hour);
            StartCoroutine(Clock());
        }

        private IEnumerator HappinessTimer()
        {
            int index = 0;
        
            foreach (var table in tableHappiness)
            {
                index += 1;
            
                if (table.GetComponent<ImageState>().isActive)
                {
                    table.fillAmount -= happinessDecay * Time.deltaTime;

                    if (table.fillAmount >= .5f && table.sprite == happyImage)
                    {
                        table.color = Color.green;
                        table.GetComponent<ImageState>().happinessModifier = 1f;
                    }
                    else if (table.fillAmount < .5f && table.fillAmount > 0 && table.sprite == happyImage)
                    {
                        table.color = Color.yellow;
                        table.GetComponent<ImageState>().happinessModifier = .75f;
                    }
                    else if (table.fillAmount <= 0 && table.sprite == happyImage)
                    {
                        table.sprite = sadImage;
                        table.fillAmount = 1;
                    }
                    else if (table.fillAmount >= .5f && table.sprite == sadImage)
                    {
                        table.color = new Color32(255, 127, 39, 255);
                        table.GetComponent<ImageState>().happinessModifier = .50f;
                    }
                    else if (table.fillAmount < .5f && table.fillAmount > 0 && table.sprite == sadImage)
                    {
                        table.color = Color.red;
                        table.GetComponent<ImageState>().happinessModifier = 0f;
                    }
                    else if (table.fillAmount <= 0f && table.sprite == sadImage)
                    {
                        GameState.Instance.SpawnKaren(index);
                        table.GetComponent<ImageState>().Deactivate();
                    }
                }
                else
                {
                    table.GetComponent<ImageState>().Deactivate();
                }
            }

            index = 0;
            yield return null;
            StartCoroutine(HappinessTimer());
        }

        public void ActivateHappinessImage(int tableNumber)
        {
            tableHappiness[tableNumber - 1].GetComponent<ImageState>().Activate();
        }
    
        public void DeactivateHappinessImage(int tableNumber)
        {
            tableHappiness[tableNumber - 1].GetComponent<ImageState>().Deactivate();
        }

        public void BoostHappiness(int tableNumber, float boostAmount)
        {
            tableHappiness[tableNumber - 1].fillAmount += boostAmount;
        }

        public float GetHappinessModifier(int tableNumber)
        {
            return tableHappiness[tableNumber - 1].GetComponent<ImageState>().happinessModifier;
        }

        public void StartStolenAnim(float stolenAmount)
        {
            isWaiting = true;
            currentTips = desiredTips;

            if (currentTips < stolenAmount)
            {
                stolenAmount = currentTips;
            }

            GameState.Instance.tipsStolen += stolenAmount;

            stolenTipsAmount.text = stolenTipsAmountDS.text = "-" + stolenAmount.ToString("c2");
            stolenGroup.SetActive(true);

            StartCoroutine(WaitForStolenAnim(stolenAmount));
        }

        private IEnumerator WaitForStolenAnim(float stolenAmount)
        {
            while(isWaiting)
            {
                yield return null;
            }
        
            currentTips = desiredTips -= stolenAmount;
        
            if(currentTips < 0)
            {
                currentTips = desiredTips = 0;
            }

        }

    }
}
