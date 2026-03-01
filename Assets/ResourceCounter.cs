using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour
{
    // public vars will show up in the Unity Inspector
    public float lemons = 0f;
    public float apples = 0f;
    public float money = 0f;

    // rates per second
    public float lemonRate = 0f;
    public float appleRate = 0f;

    public TextMeshProUGUI lemonDisplayText;
    public TextMeshProUGUI appleDisplayText;

    void Start()
    {
        
    }

    void Update()
    {
        // Euler integration
        lemons += (lemonRate * Time.deltaTime);
        apples += (appleRate * Time.deltaTime);

        // update the UI
        if (lemonDisplayText != null)
            lemonDisplayText.text = "Lemons: " + Mathf.FloorToInt(lemons).ToString();
        if (appleDisplayText != null)
            appleDisplayText.text = "Apples: " + Mathf.FloorToInt(apples).ToString();
    }

    public void PickLemon()
    {
        lemons += 1f;
    }

    public void BuyLemonTree()
    {
        if (lemons >= 30f)
        {
            lemons -= 30f;  // cost of lemon tree
            lemonRate += 5f;  // increase lemon rate by 5 lemons per second
        }
    }

    public void BuyApple()
    {
        if (money >= 1f)
        {
            money -= 1f;
            apples += 1f;
        }
    }
}
