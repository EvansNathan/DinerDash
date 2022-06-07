using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BetweenScenes : MonoBehaviour
{
    public Text baseTipsText;
    public Text baseTipsDS;
    private float baseTips;
    public float baseTipsAnimSpeed;
    public float baseTipsSizeModifier;

    public Text multiplierText;
    public Text multiplierDS;
    public Text multiplierBlue;
    public float multiplierSizeModifier;
    private int multiplier;

    public GameObject stolenGroup;
    public Text stolenText;
    public Text stolenDS;
    public float stolenFallSpeed;
    public float squishSpeed;
    private float stolenTips;

    public Text ordersText;
    public Text ordersDS;
    private int ordersDelivered;

    public Text karenText;
    public Text karenDS;
    private int karensAngered;

    public Text takeHomeText;
    public Text takeHomeDS;
    public GameObject takeHomeGroup;
    private float takeHomeTips;

    public AudioSource sound;

    private void Start()
    {
        baseTips = GameState.Instance.baseTips + GameState.Instance.tipsStolen;
        multiplier = GameState.Instance.multiplier;
        stolenTips = GameState.Instance.tipsStolen;
        ordersDelivered = GameState.Instance.ordersDelivered;
        karensAngered = GameState.Instance.karenAttacks;
        
        SetStartingText();
        TakeHomeCalc();
        
        Invoke("StartAnimations", 2f);
        
    }

    private void SetStartingText()
    {
        baseTipsText.text = baseTipsDS.text = "Tips: $0.00";
        multiplierText.text = multiplierDS.text = "Multiplier: x1";
        stolenText.text = stolenDS.text = "Stolen: -" + stolenTips.ToString("c2");
        ordersText.text = ordersDS.text = "Orders\nDelivered\n0";
        karenText.text = karenDS.text = "Karen\nAttacks\n0";
        takeHomeText.text = takeHomeDS.text = "Take Home: $0.00";
    }

    private void StartAnimations()
    {
        StartCoroutine(BaseTipsAnim());
    }

    private IEnumerator BaseTipsAnim()
    {
        sound.Play();
        var currentBaseTips = 0f;

        while (currentBaseTips < baseTips)
        {
            currentBaseTips += baseTipsAnimSpeed * Time.deltaTime;
            baseTipsText.text = baseTipsDS.text = "Tips: " + currentBaseTips.ToString("c2");
            baseTipsText.rectTransform.localScale += Vector3.one * baseTipsSizeModifier * Time.deltaTime;
            baseTipsDS.rectTransform.localScale += Vector3.one * baseTipsSizeModifier * Time.deltaTime;
            yield return null;
        }
        
        baseTipsText.rectTransform.localScale = Vector3.one;
        baseTipsDS.rectTransform.localScale = Vector3.one;
        StartCoroutine(MultiplierAnim());
    }

    private IEnumerator MultiplierAnim()
    {
        var currentMultiplier = 1;

        while (currentMultiplier < multiplier)
        {
            while (multiplierText.rectTransform.localScale.x < 5)
            {
                multiplierText.rectTransform.localScale += Vector3.one * multiplierSizeModifier * 10 * Time.deltaTime;
                multiplierDS.rectTransform.localScale += Vector3.one * multiplierSizeModifier * 10 * Time.deltaTime;
                yield return null;
            }

            //multiplierBlue.text = "Multiplier";
            multiplierText.rectTransform.localScale = Vector3.one;
            multiplierDS.rectTransform.localScale = Vector3.one + (Vector3.one * currentMultiplier * .1f);
            currentMultiplier += 1;
            multiplierText.text = multiplierDS.text = "Multiplier: x" + currentMultiplier;
            yield return new WaitForSeconds(.1f);
            //multiplierBlue.text = "";
            
            
        }

        multiplierDS.rectTransform.localScale = Vector3.one;
        StartCoroutine(StolenAnim());
    }

    private IEnumerator StolenAnim()
    {
        Debug.Log("COROUTINE");
        var currentScale = 1f;
        
        while (stolenGroup.transform.localPosition.y > 0)
        {
            Vector3 temp = new Vector3(stolenGroup.transform.position.x, stolenGroup.transform.position.y - (stolenFallSpeed * Time.deltaTime), stolenGroup.transform.position.z);
            stolenGroup.transform.position = temp;
            yield return null;
        }

        while (stolenGroup.transform.localScale.y > .5f)
        {
            currentScale -= squishSpeed * Time.deltaTime;
            stolenGroup.transform.localScale = new Vector3(1, currentScale, 1);
            yield return null;
        }
        
        while (stolenGroup.transform.localScale.y < 1f)
        {
            currentScale += squishSpeed * Time.deltaTime;
            stolenGroup.transform.localScale = new Vector3(1, currentScale, 1);
            yield return null;
        }

        StartCoroutine(OrdersDelivered());
    }

    private IEnumerator OrdersDelivered()
    {
        var currentOrdersDelivered = 0;
        var currentKarensAngered = 0;

        while (currentOrdersDelivered < ordersDelivered)
        {
            currentOrdersDelivered += 1;
            ordersText.text = ordersDS.text = "Orders\nDelivered\n" + currentOrdersDelivered;
            yield return new WaitForSeconds(.2f);
        }
        
        while (currentKarensAngered < karensAngered)
        {
            currentKarensAngered += 1;
            karenText.text = karenDS.text = "Karen\nAttacks\n" + currentKarensAngered;
            yield return new WaitForSeconds(.2f);
        }

        StartCoroutine(TakeHomeAnim());
    }

    private IEnumerator TakeHomeAnim()
    {
        var currentScale = 1f;
        takeHomeText.text = takeHomeDS.text = takeHomeTips.ToString("c2");
        
        while (takeHomeGroup.transform.localPosition.y > -315)
        {
            Vector3 temp = new Vector3(takeHomeGroup.transform.position.x, takeHomeGroup.transform.position.y - (stolenFallSpeed * Time.deltaTime), takeHomeGroup.transform.position.z);
            takeHomeGroup.transform.position = temp;
            yield return null;
        }

        while (takeHomeGroup.transform.localScale.y > .5f)
        {
            currentScale -= squishSpeed * Time.deltaTime;
            takeHomeGroup.transform.localScale = new Vector3(1, currentScale, 1);
            yield return null;
        }
        
        while (takeHomeGroup.transform.localScale.y < 1f)
        {
            currentScale += squishSpeed * Time.deltaTime;
            takeHomeGroup.transform.localScale = new Vector3(1, currentScale, 1);
            yield return null;
        }
    }

    private void TakeHomeCalc()
    {
        takeHomeTips = (baseTips * multiplier) - stolenTips;
        GameState.Instance.takeHomeTips = takeHomeTips;
    }

}
