using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TutorialPopups : MonoBehaviour
{

    public ResourceCounter rc;
    public GameObject popupPanel;
    public TMP_Text popupText;

    public GameObject lemonTrophy;
    public GameObject appleTrophy;
    public GameObject moneyTrophy;

    public float popupTime = 4f;
    private Vector3 originalScale;

    private float timer = 0f;
    private bool showing = false;

    private bool startShown = false;
    private bool applesUnlockedShown = false;
    private bool lemonTreeShown = false;
    private bool lemonGardenShown = false;
    private bool lemonadeMachineShown = false;
    private bool appleTreeShown = false;
    private bool appleGreenhouseShown = false;
    private bool appleJuiceMachineShown = false;
    private bool appleJuiceFactoryShown = false;

    private bool fertilizerShown = false;
    private bool efficientMachinesShown = false;
    private bool betterSeedsShown = false;
    private bool strongerCrushersShown = false;

    private bool lemonTrophyShown = false;
    private bool appleTrophyShown = false;
    private bool moneyTrophyShown = false;
    
    public AudioSource unlockSoundSource;
    public AudioClip unlockSoundClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (popupPanel != null)
        {
            originalScale = popupPanel.transform.localScale; 
            popupPanel.SetActive(false);
            popupPanel.transform.localScale = Vector3.zero;
        }

        ShowPopup("Grab some lemons.\nCollect lemons to unlock new items.");
        startShown = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (rc == null) return;

        if (showing)
        {
            timer -= Time.deltaTime;

            if(timer <= 0f)
            {
                HidePopup();
            }

            return;
        }

        if (!lemonTreeShown && rc.lemons >= rc.lemon_tree_cost && rc.num_lemon_trees == 0)
        {
            ShowPopup("Lemon Trees unlocked.\nThey automatically produce lemons.\nRate: 2 lemons/sec\nCost: 30 lemons.");
            lemonTreeShown = true;
            return;
        }

        if (!lemonGardenShown && rc.lemons >= rc.lemon_garden_cost && rc.num_lemon_gardens == 0)
        {
            ShowPopup("Lemon Gardens unlocked.\nThey produce lemons faster than Lemon Trees.\nRate: 5 lemons/sec\nCost: 60 lemons.");
            lemonGardenShown = true;
            return;
        }

        if (!lemonadeMachineShown && rc.lemons >= rc.lemonade_machine_cost && rc.num_lemonade_machines == 0)
        {
            ShowPopup("Lemonade Machines unlocked.\nThey generate money over time.\nRate: 1 money/sec\nCost: 100 lemons.");
            lemonadeMachineShown = true;
            return;
        }

        if (!applesUnlockedShown && rc.money >= 1f)
        {
            ShowPopup("Grab some apples.\nUse money to buy apples and unlock new items.");
            applesUnlockedShown = true;
            return;
        }

        if (!appleTreeShown && rc.apples >= rc.apple_tree_cost && rc.num_apple_trees == 0)
        {
            ShowPopup("Apple Trees unlocked.\nThey automatically produce apples.\nRate: 3 apples/sec\nCost: 30 apples.");
            appleTreeShown = true;
            return;
        }

        if (!appleGreenhouseShown && rc.apples >= rc.apple_greenhouse_cost && rc.num_apple_greenhouses == 0)
        {
            ShowPopup("Apple Greenhouses unlocked.\nThey produce apples faster than Apple Trees.\nRate: 8 apples/sec\nCost: 60 apples.");
            appleGreenhouseShown = true;
            return;
        }

        if (!appleJuiceMachineShown && rc.apples >= rc.apple_juice_machine_cost && rc.num_apple_juice_machines == 0)
        {
            ShowPopup("Apple Juice Machines unlocked.\nThey generate money from apples.\nRate: 15 money/sec\nCost: 100 apples.");
            appleJuiceMachineShown = true;
            return;
        }

        if (!appleJuiceFactoryShown && rc.apples >= rc.apple_juice_factory_cost && rc.num_apple_juice_factories == 0)
        {
            ShowPopup("Apple Juice Factories unlocked.\nThey generate large amounts of money.\nRate: 100 money/sec\nCost: 500 apples.");
            appleJuiceFactoryShown = true;
            return;
        }

        if (!fertilizerShown && rc.lemons >= 100f && rc.fertilizerButton != null && rc.fertilizerButton.interactable)
        {
            ShowPopup("Upgrade unlocked: Lemon Fertilizer\nBoosts Lemon Tree production by 20%.\nCost: 100 lemons.");
            fertilizerShown = true;
            return;
        }

        if (!efficientMachinesShown && rc.lemons >= 200f && rc.lemonadeMachineButton != null && rc.lemonadeMachineButton.interactable)
        {
            ShowPopup("Upgrade unlocked: Efficient Lemonade Machines\nBoosts Lemonade Machine production by 15%.\nCost: 200 lemons.");
            efficientMachinesShown = true;
            return;
        }

        if (!betterSeedsShown && rc.apples >= 50f && rc.seedsButton != null && rc.seedsButton.interactable)
        {
            ShowPopup("Upgrade unlocked: Better Apple Seeds\nBoosts Apple Tree production by 10%.\nCost: 50 apples.");
            betterSeedsShown = true;
            return;
        }

        if (!strongerCrushersShown && rc.apples >= 200f && rc.crusherButton != null && rc.crusherButton.interactable)
        {
            ShowPopup("Upgrade unlocked: Stronger Crushers\nBoosts Apple Juice Machine production by 20%.\nCost: 200 apples.");
            strongerCrushersShown = true;
            return;
        }

        if (!lemonTrophyShown && rc.lemons >= 2000f)
        {
            ShowPopup("Achievement unlocked: Lemonaire\nAwarded for obtaining 2000 lemons!");
            lemonTrophy.SetActive(true);
            lemonTrophyShown = true;
            return;
        }

        if (!appleTrophyShown && rc.apples >= 1500f)
        {
            ShowPopup("Achievement unlocked: Applionaire\nAwarded for obtaining 1500 apples!");
            appleTrophy.SetActive(true);
            appleTrophyShown = true;
            return;
        }

        if (!moneyTrophyShown && rc.money >= 5000f)
        {
            ShowPopup("Achievement unlocked: Rich\nAwarded for obtaining 5000 money!");
            moneyTrophy.SetActive(true);
            moneyTrophyShown = true;
            return;
        }
    }

void ShowPopup(string msg)
    {
        /*if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }*/

        if (popupText != null)
        {
            popupText.text = msg;
        }

        showing = true;
        timer = popupTime;

        StopAllCoroutines(); // Prevents animations from fighting each other
        StartCoroutine(AnimateIn());

        /*if(unlockSoundSource != null && unlockSoundClip != null)
        {
            unlockSoundSource.PlayOneShot(unlockSoundClip);
        }*/
    }

    void HidePopup()
    {
        /*if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }

        showing = false;*/

        StartCoroutine(AnimateOut());
    }

    IEnumerator AnimateIn()
    {
        popupPanel.SetActive(true);
        float t = 0;
        float duration = 0.6f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float percent = t / duration;

            float s;
            if (percent < 0.7f)
            {
                s = Mathf.Lerp(0f, 1.4f, percent / 0.7f);
            }
            else
            {
                // go from 1.1 back down to 1.0
                s = Mathf.Lerp(1.4f, 1.0f, (percent - 0.7f) / 0.3f);
            }

            popupPanel.transform.localScale = originalScale * s;
            yield return null;
        }

        // ensure it ends at exactly the right size
        popupPanel.transform.localScale = originalScale;
    }

    IEnumerator AnimateOut()
    {
        float t = 0;
        float duration = 0.3f;
        Vector3 startScale = popupPanel.transform.localScale;

        while (t < duration)
        {
            t += Time.deltaTime;
            popupPanel.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t / duration);
            yield return null;
        }

        popupPanel.SetActive(false);
        showing = false;
    }
}