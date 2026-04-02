using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ResourceCounter : MonoBehaviour
{
    // public vars will show up in the Unity Inspector
    public float lemons = 0f;
    public float apples = 0f;
    public float money = 0f;

    // counters for automatic generators
    public int num_lemon_trees = 0;
    public int num_lemon_gardens = 0;
    public int num_apple_trees = 0;
    public int num_apple_greenhouses = 0;
    public int num_lemonade_machines = 0;
    public int num_apple_juice_machines = 0;
    public int num_apple_juice_factories = 0;

    // current cost for each generator
    public float lemon_tree_cost = 30f;
    public float lemon_garden_cost = 60f;
    public float apple_tree_cost = 30f;
    public float apple_greenhouse_cost = 60f;
    public float lemonade_machine_cost = 100f;
    public float apple_juice_machine_cost = 100f;
    public float apple_juice_factory_cost = 500f;

    // rates per second
    public float lemonTreeRate = 0f;
    public float lemonadeMachineRate = 0f;
    public float lemonGardenRate = 0f;
    public float appleTreeRate = 0f;
    public float appleGreenhouseRate = 0f;
    public float appleJuiceMachineRate = 0f;
    public float appleJuiceFactoryRate = 0f;

    public TextMeshProUGUI lemonDisplayText;
    public TextMeshProUGUI appleDisplayText;
    public TextMeshProUGUI moneyDisplayText;
    public TextMeshProUGUI lemonTreeDisplayText;
    public TextMeshProUGUI lemonGardenDisplayText;
    public TextMeshProUGUI lemonadeMachineDisplayText;
    public TextMeshProUGUI appleTreeDisplayText;
    public TextMeshProUGUI appleGreenhouseDisplayText;
    public TextMeshProUGUI appleJuiceMachineDisplayText;
    public TextMeshProUGUI appleJuiceFactoryDisplayText;

    public TextMeshProUGUI lemonTreeCostDisplayText;
    public TextMeshProUGUI lemonGardenCostDisplayText;
    public TextMeshProUGUI lemonadeMachineCostDisplayText;
    public TextMeshProUGUI appleTreeCostDisplayText;
    public TextMeshProUGUI appleGreenhouseCostDisplayText;
    public TextMeshProUGUI appleJuiceMachineCostDisplayText;
    public TextMeshProUGUI appleJuiceFactoryCostDisplayText;

    public TextMeshProUGUI fertilizerText;
    public TextMeshProUGUI lemonadeUpgradeText;
    public TextMeshProUGUI seedsText;
    public TextMeshProUGUI crusherText;

    public Button fertilizerButton;
    public Button lemonadeMachineButton;
    public Button seedsButton;
    public Button crusherButton;

    public ParticleSystem lemonClickEffect;
    public ParticleSystem appleClickEffect;
    public ParticleSystem lemonEmitter;
    public ParticleSystem appleEmitter;
    public ParticleSystem leftSmokeEffect;
    public ParticleSystem rightSmokeEffect;
    public GameObject greenhouse;
    public AudioClip shearsClip;
    public GameObject blender;
    public AudioClip blenderClip;
    public GameObject garden;
    public GameObject lemonTree;
    public GameObject appleTree;
    public GameObject lemonadeMachine;
    public GameObject appleFactory;

    public AudioSource appleBuySource;
    public AudioClip appleBuyClip;
    public AudioClip generatorClip;
    public AudioClip powerupClip;
    public AudioClip unlockAppleClip;

    private AudioSource shearsSource;
    private AudioSource blenderSource;
    private AudioSource lemonTreeSource;
    private AudioSource lemonadeMachineSource;
    private AudioSource appleTreeSource;
    private AudioSource appleGreenhouseSource;
    private AudioSource appleFactorySource;
    private AudioSource lemonTreePowerupSource;
    private AudioSource lemonadeMachinePowerupSource;
    private AudioSource appleTreePowerupSource;
    private AudioSource appleJuicePowerupSource;
    


    private Vector3 targetTextScale = new Vector3(1.5f, 1.5f, 1.0f);
    private Color lemon_color;
    private Color apple_color;
    private bool bought_first_apple = false;
    public HapticManager hapticManager;
    
    void Start()
    {
        shearsSource = garden.GetComponent<AudioSource>();
        blenderSource = blender.GetComponent<AudioSource>();
        lemonTreeSource = lemonTree.GetComponent<AudioSource>();
        lemonadeMachineSource = lemonadeMachine.GetComponent<AudioSource>();
        appleTreeSource = appleTree.GetComponent<AudioSource>();
        appleGreenhouseSource = greenhouse.GetComponent<AudioSource>();
        appleFactorySource = appleFactory.GetComponent<AudioSource>();
        lemonTreePowerupSource = fertilizerText.GetComponent<AudioSource>();
        lemonadeMachinePowerupSource = lemonadeUpgradeText.GetComponent<AudioSource>();
        appleTreePowerupSource = seedsText.GetComponent<AudioSource>();
        appleJuicePowerupSource = crusherText.GetComponent<AudioSource>();
        lemon_color = Color.yellow;
        apple_color = Color.red;
        StartCoroutine(resourceEmission(lemonEmitter, "lemon"));
        StartCoroutine(resourceEmission(appleEmitter, "apple"));
    }

    void Update()
    {
        float moneyRate = lemonadeMachineRate + appleJuiceMachineRate + appleJuiceFactoryRate;
        float lemonRate = lemonTreeRate + lemonGardenRate;
        float appleRate = appleTreeRate + appleGreenhouseRate;
        // Euler integration
        lemons += (lemonRate * Time.deltaTime);
        apples += (appleRate * Time.deltaTime);
        money += (moneyRate *Time.deltaTime);

        if (!bought_first_apple && apples > 0) {
            appleBuySource.PlayOneShot(unlockAppleClip);
            bought_first_apple = false;
        }
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
        if (lemonGardenDisplayText != null) 
            lemonGardenDisplayText.text = "Lemon Gardens: " + num_lemon_gardens.ToString();
        if (lemonadeMachineDisplayText != null) 
            lemonadeMachineDisplayText.text = "Lemonade Machines: " +num_lemonade_machines.ToString();
        if (appleTreeDisplayText != null) 
            appleTreeDisplayText.text = "Apple Trees: " +num_apple_trees.ToString();
        if (appleGreenhouseDisplayText != null)
            appleGreenhouseDisplayText.text = "Apple Greenhouses: " + num_apple_greenhouses.ToString();
        if (appleJuiceMachineDisplayText != null) 
            appleJuiceMachineDisplayText.text = "Apple Juice Machines: " +num_apple_juice_machines.ToString();
        if (appleJuiceFactoryDisplayText != null)
            appleJuiceFactoryDisplayText.text = "Apple Juice Factories: " + num_apple_juice_factories.ToString();
        // update generator costs in ui
        if (lemonTreeCostDisplayText != null)
            lemonTreeCostDisplayText.text = "Cost: " + Mathf.FloorToInt(lemon_tree_cost).ToString() + " lemons";
        if (lemonGardenCostDisplayText != null)
            lemonGardenCostDisplayText.text = "Cost: " + Mathf.FloorToInt(lemon_garden_cost).ToString() + " lemons";
        if (lemonadeMachineCostDisplayText != null)
            lemonadeMachineCostDisplayText.text = "Cost: " + Mathf.FloorToInt(lemonade_machine_cost).ToString() + " lemons";
        if (appleTreeCostDisplayText != null)
            appleTreeCostDisplayText.text = "Cost: " + Mathf.FloorToInt(apple_tree_cost).ToString() +" apples";
        if (appleGreenhouseCostDisplayText != null)
            appleGreenhouseCostDisplayText.text = "Cost: " + Mathf.FloorToInt(apple_greenhouse_cost).ToString() +" apples";
        if (appleJuiceMachineCostDisplayText != null) 
            appleJuiceMachineCostDisplayText.text = "Cost: " + Mathf.FloorToInt(apple_juice_machine_cost).ToString() + " apples";
        if (appleJuiceFactoryCostDisplayText != null)
            appleJuiceFactoryCostDisplayText.text = "Cost: " + Mathf.FloorToInt(apple_juice_factory_cost).ToString() + " apples";
    }

    public void PickLemon()
    {
        lemons += 1f;
        lemonClickEffect.Emit(1);
        lemonEmitter.Emit(1);
    }

    IEnumerator resourceEmission(ParticleSystem emitter, string resource) {
        if (resource == "lemon") {
            while (true) {
                lemonEmitter.Emit(Mathf.FloorToInt(lemonTreeRate + lemonGardenRate));
                yield return new WaitForSeconds(1);
            }
        }
        
        if (resource == "apple") {
            while (true) {
                appleEmitter.Emit(Mathf.FloorToInt(appleTreeRate + appleGreenhouseRate));
                yield return new WaitForSeconds(1);
            }
        }
    }

    IEnumerator textColorAnimation(TextMeshProUGUI costTxtObj, Color color) {
        Color orig_color = new Color(costTxtObj.color.r,costTxtObj.color.b,costTxtObj.color.g,costTxtObj.color.a);
        float elapsed = 0f;
        float interpolation_ratio = 0.2f;
        while (elapsed < 1.2f) {
            costTxtObj.color = Color.Lerp(costTxtObj.color,color, interpolation_ratio);
            elapsed+=Time.deltaTime;
            yield return null;
        }
        elapsed = 0f;
        while (elapsed < 1.2f) {
            costTxtObj.color = Color.Lerp(costTxtObj.color, orig_color, interpolation_ratio);
            elapsed+=Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    IEnumerator textMotionEase(TextMeshProUGUI displayTxtObj){
        Vector3 scaleVel =25f*(targetTextScale - displayTxtObj.rectTransform.localScale)*Time.deltaTime;
        float elapsed = 0f;
        while (elapsed < 1.2f){
            displayTxtObj.rectTransform.localScale += scaleVel*Time.deltaTime;
            elapsed+=Time.deltaTime;
            yield return null;
        }
        elapsed = 0f;
        while (elapsed < 1.2f){
            displayTxtObj.rectTransform.localScale -= scaleVel*Time.deltaTime;
            elapsed+=Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    IEnumerator objectMotionEase(GameObject obj) {
        Vector3 targetScale = new Vector3(obj.transform.localScale.x*2.0f, obj.transform.localScale.y, obj.transform.localScale.z);
        Vector3 scaleVel =3f*(targetScale - obj.transform.localScale)*Time.deltaTime;
        float elapsed = 0f;
        while (elapsed < 1.2f){
            obj.transform.localScale += scaleVel*Time.deltaTime;
            elapsed+=Time.deltaTime;
            yield return null;
        }
        // elapsed = 0f;
        // while (elapsed < 1.2f){
        //     obj.transform.localScale -= scaleVel*Time.deltaTime;
        //     elapsed+=Time.deltaTime;
        //     yield return null;
        // }
        yield break;

    }
    public void BuyLemonTree()
    {
        if (lemons >= lemon_tree_cost)
        {
            lemons -= lemon_tree_cost;  // cost of lemon tree
            lemonTreeRate += 2f;  // increase lemon rate by 2 lemons per second
            num_lemon_trees+=1;
            lemon_tree_cost = Mathf.Floor(1.2f*lemon_tree_cost);
            StartCoroutine(textMotionEase(lemonTreeDisplayText));
            StartCoroutine(textColorAnimation(lemonTreeCostDisplayText, lemon_color));

            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyLemonGarden() {
        if (lemons >= lemon_garden_cost) {
            lemons -= lemon_garden_cost;
            lemonGardenRate += 5f;
            num_lemon_gardens+=1;
            lemon_garden_cost = Mathf.Floor(1.5f*lemon_garden_cost);
            StartCoroutine(textMotionEase(lemonGardenDisplayText));
            StartCoroutine(textColorAnimation(lemonGardenCostDisplayText, lemon_color));
            shearsSource.clip = shearsClip;
            shearsSource.PlayOneShot(generatorClip);
            shearsSource.PlayOneShot(shearsClip);

            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyLemonadeMachine() {
        if (lemons >= lemonade_machine_cost) {
            lemons -= lemonade_machine_cost;
            lemonadeMachineRate +=1f;
            num_lemonade_machines+=1;
            lemonade_machine_cost = Mathf.Floor(1.5f*lemonade_machine_cost);
            StartCoroutine(textMotionEase(lemonadeMachineDisplayText));
            StartCoroutine(textColorAnimation(lemonadeMachineCostDisplayText, lemon_color));
            lemonadeMachineSource.PlayOneShot(generatorClip);
            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyApple()
    {
        if (money >= 1f)
        {
            money -= 1f;
            apples += 1f;
        }

        appleEmitter.Emit(1);

        if(appleBuySource != null && appleBuyClip != null)
        {
            appleBuySource.PlayOneShot(appleBuyClip);
        }

        if (hapticManager != null)
        {
                hapticManager.PlayAppleHaptic();
        }
    }
    public void BuyAppleTree(){
        if (apples >= apple_tree_cost) {
            apples -= apple_tree_cost;
            appleTreeRate += 3f;
            num_apple_trees+=1;
            apple_tree_cost = Mathf.Floor(1.2f*apple_tree_cost);
            StartCoroutine(textMotionEase(appleTreeDisplayText));
            StartCoroutine(textColorAnimation(appleTreeCostDisplayText, apple_color));
            appleTreeSource.PlayOneShot(generatorClip);
            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyAppleGreenhouse() {
        if (apples >= apple_greenhouse_cost) {
            apples -= apple_greenhouse_cost;
            appleGreenhouseRate += 8f;
            num_apple_greenhouses+=1;
            apple_greenhouse_cost = Mathf.Floor(1.5f*apple_greenhouse_cost);
            StartCoroutine(textMotionEase(appleGreenhouseDisplayText));      
            StartCoroutine(objectMotionEase(greenhouse));
            StartCoroutine(textColorAnimation(appleGreenhouseCostDisplayText, apple_color));
            appleGreenhouseSource.PlayOneShot(generatorClip);
            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyAppleJuiceMachine() {
        if (apples >= apple_juice_machine_cost) {
            apples -= apple_juice_machine_cost;
            appleJuiceMachineRate += 15f;
            num_apple_juice_machines+=1;
            apple_juice_machine_cost = Mathf.Floor(1.5f*apple_juice_machine_cost);
            StartCoroutine(textMotionEase(appleJuiceMachineDisplayText));
            StartCoroutine(textColorAnimation(appleJuiceMachineCostDisplayText, apple_color));
            blenderSource.clip = blenderClip;
            blenderSource.PlayOneShot(generatorClip);
            blenderSource.PlayOneShot(blenderClip);
            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyAppleJuiceFactory() {
        if (apples >= apple_juice_factory_cost) {
            apples -= apple_juice_factory_cost;
            appleJuiceFactoryRate += 100f;
            num_apple_juice_factories+=1;
            apple_juice_factory_cost = Mathf.Floor(1.9f*apple_juice_factory_cost);
            StartCoroutine(textMotionEase(appleJuiceFactoryDisplayText));
            StartCoroutine(textColorAnimation(appleJuiceFactoryCostDisplayText, apple_color));
            leftSmokeEffect.Emit(100);
            rightSmokeEffect.Emit(100);
            
            appleFactorySource.PlayOneShot(generatorClip);
            if (hapticManager != null)
            {
                hapticManager.PlayGeneratorHaptic();
            }
        }
    }
    public void BuyFertilizer() {
        //Upgrade for lemon trees
        if (lemons >= 100) {
            lemons -= 100;
            lemonTreeRate *= 1.2f;
            //disable ability to buy upgrade after 1st purchase
            if (fertilizerButton != null) fertilizerButton.interactable = false;
            appleTreePowerupSource.PlayOneShot(powerupClip);
            if (hapticManager != null)
            {
                hapticManager.PlayPowerupHaptic();
            }
        }
    }
    public void BuyEfficientLemonadeMachines() {
        //upgrade for lemonade machines
        if (lemons >= 200){
            lemons -= 200;
            lemonadeMachineRate *= 1.15f;
            if (lemonadeMachineButton != null) lemonadeMachineButton.interactable = false;
            lemonadeMachinePowerupSource.PlayOneShot(powerupClip);
            if (hapticManager != null)
            {
                hapticManager.PlayPowerupHaptic();
            }
        }
        
    }
    public void BuyImprovedSeeds() {
        //upgrade for apple trees
        if (apples >= 50) {
            apples -= 50;
            appleTreeRate *= 1.1f;
            if (seedsButton != null) seedsButton.interactable = false;
            appleTreePowerupSource.PlayOneShot(powerupClip);
            if (hapticManager != null)
            {
                hapticManager.PlayPowerupHaptic();
            }
        }
    }
    public void BuyStrongerCrushers() {
        //upgrade for apple juice machines
        if (apples >= 200) {
            apples -= 200;
            appleJuiceMachineRate *= 1.2f;
            if (crusherButton != null) crusherButton.interactable = false;
            appleJuicePowerupSource.PlayOneShot(powerupClip);
            if (hapticManager != null)
            {
                hapticManager.PlayPowerupHaptic();
            }
        }
    }
}
