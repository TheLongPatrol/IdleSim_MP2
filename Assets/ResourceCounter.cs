using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    // public vars will show up in the Unity Inspector
    public float lemons = 0f;
    public float apples = 0f;
    public float money = 0f;

    // counters for automatic generators
    public int num_lemon_trees = 0;
    public int num_apple_trees = 0;
    public int num_lemonade_machines = 0;
    public int num_apple_juice_machines = 0;

    // current cost for each generator
    public float lemon_tree_cost = 30f;
    public float apple_tree_cost = 30f;
    public float lemonade_machine_cost = 100f;
    public float apple_juice_machine_cost = 100f;

    // rates per second
    public float lemonRate = 0f;
    public float lemonadeMachineRate = 0f;
    public float appleRate = 0f;
    public float appleJuiceMachineRate = 0f;

    public TextMeshProUGUI lemonDisplayText;
    public TextMeshProUGUI appleDisplayText;
    public TextMeshProUGUI moneyDisplayText;
    public TextMeshProUGUI lemonTreeDisplayText;
    public TextMeshProUGUI lemonadeMachineDisplayText;
    public TextMeshProUGUI appleTreeDisplayText;
    public TextMeshProUGUI appleJuiceMachineDisplayText;
    public TextMeshProUGUI lemonTreeCostDisplayText;
    public TextMeshProUGUI lemonadeMachineCostDisplayText;
    public TextMeshProUGUI appleTreeCostDisplayText;
    public TextMeshProUGUI appleJuiceMachineCostDisplayText;

    public Button fertilizerButton;
    public Button lemonadeMachineButton;
    public Button seedsButton;
    public Button crusherButton;

    void Start()
    {
        
    }

    void Update()
    {
        float moneyRate = lemonadeMachineRate+appleJuiceMachineRate;
        // Euler integration
        lemons += (lemonRate * Time.deltaTime);
        apples += (appleRate * Time.deltaTime);
        money += (moneyRate *Time.deltaTime);

        // update the UI
        if (lemonDisplayText != null)
            lemonDisplayText.text = "Lemons: " + Mathf.FloorToInt(lemons).ToString();
        if (appleDisplayText != null)
            appleDisplayText.text = "Apples: " + Mathf.FloorToInt(apples).ToString();
        if (moneyDisplayText != null)
            moneyDisplayText.text = "Money: " + Mathf.FloorToInt(money).ToString();
        // update generator amounts ui
        if (lemonTreeDisplayText != null) 
            lemonTreeDisplayText.text = "Lemon Trees: " + num_lemon_trees.ToString();
        if (lemonadeMachineDisplayText != null) 
            lemonadeMachineDisplayText.text = "Lemonade Machines: " +num_lemonade_machines.ToString();
        if (appleTreeDisplayText != null) 
            appleTreeDisplayText.text = "Apple Trees: " +num_apple_trees.ToString();
        if (appleJuiceMachineDisplayText != null) 
            appleJuiceMachineDisplayText.text = "Apple Juice Machines: " +num_apple_juice_machines.ToString();
        // update generator costs in ui
        if (lemonTreeCostDisplayText != null)
            lemonTreeCostDisplayText.text = "Cost: " + Mathf.FloorToInt(lemon_tree_cost).ToString() + " lemons";
        if (lemonadeMachineCostDisplayText != null)
            lemonadeMachineCostDisplayText.text = "Cost: " + Mathf.FloorToInt(lemonade_machine_cost).ToString() + " lemons";
        if (appleTreeCostDisplayText != null)
            appleTreeCostDisplayText.text = "Cost: " + Mathf.FloorToInt(apple_tree_cost).ToString() +" apples";
        if (appleJuiceMachineCostDisplayText != null) 
            appleJuiceMachineCostDisplayText.text = "Cost: " + Mathf.FloorToInt(apple_juice_machine_cost).ToString() + " apples";
    }

    public void PickLemon()
    {
        lemons += 1f;
    }

    public void BuyLemonTree()
    {
        if (lemons >= lemon_tree_cost)
        {
            lemons -= lemon_tree_cost;  // cost of lemon tree
            lemonRate += 5f;  // increase lemon rate by 5 lemons per second
            num_lemon_trees+=1;
            lemon_tree_cost = Mathf.Floor(1.2f*lemon_tree_cost);
        }
    }
    public void BuyLemonadeMachine() {
        if (lemons >= lemonade_machine_cost) {
            lemons -= lemonade_machine_cost;
            lemonadeMachineRate +=1f;
            num_lemonade_machines+=1;
            lemonade_machine_cost = Mathf.Floor(1.5f*lemonade_machine_cost);
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
    public void BuyAppleTree(){
        if (apples >= apple_tree_cost) {
            apples -= apple_tree_cost;
            appleRate += 5f;
            num_apple_trees+=1;
            apple_tree_cost = Mathf.Floor(1.5f*apple_tree_cost);
        }
    }
    public void BuyAppleJuiceMachine() {
        if (apples >= apple_juice_machine_cost) {
            apples -= apple_juice_machine_cost;
            appleJuiceMachineRate += 50f;
            num_apple_juice_machines+=1;
            apple_juice_machine_cost = Mathf.Floor(1.5f*apple_juice_machine_cost);
        }
    }
    public void BuyFertilizer() {
        //Upgrade for lemon trees
        if (lemons >= 100) {
            lemons -= 100;
            lemonRate *= 1.2f;
            //disable ability to buy upgrade after 1st purchase
            if (fertilizerButton != null) fertilizerButton.interactable = false;
        }
    }
    public void BuyEfficientLemonadeMachines() {
        //upgrade for lemonade machines
        if (lemons >= 200){
            lemons -= 200;
            lemonadeMachineRate *= 1.15f;
            if (lemonadeMachineButton != null) lemonadeMachineButton.interactable = false;
        }
        
    }
    public void BuyImprovedSeeds() {
        //upgrade for apple trees
        if (apples >= 50) {
            apples -= 50;
            appleRate *= 1.1f;
            if (seedsButton != null) seedsButton.interactable = false;
        }
    }
    public void BuyStrongerCrushers() {
        //upgrade for apple juice machines
        if (apples >= 200) {
            apples -= 200;
            appleJuiceMachineRate *= 1.2f;
            if (crusherButton != null) crusherButton.interactable = false;
        }
    }
}
