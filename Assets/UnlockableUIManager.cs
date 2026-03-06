using UnityEngine;
using UnityEngine.UI;

public class UnlockableUIManager : MonoBehaviour
{
    public ResourceCounter rc;

    public GameObject lemonTreeLock;
    public GameObject lemonGardenLock;
    public GameObject lemonadeMachineLock;

    public GameObject appleTreeLock;
    public GameObject appleGreenHouseLock;
    public GameObject appleJuiceMachineLock;
    public GameObject appleJuiceFactoryLock;

    public GameObject appleLock;

    public GameObject lemonFertilizerLock;
    public GameObject efficientLemonadeLock;
    public GameObject appleSeedsLock;
    public GameObject strongerCrushersLock;

    void Update()
    {
        if (rc == null) return;

        // Lemon side
        if (lemonTreeLock != null && rc.lemons >= rc.lemon_tree_cost)
            lemonTreeLock.SetActive(false);

        if (lemonGardenLock != null && rc.lemons >= rc.lemon_garden_cost)
            lemonGardenLock.SetActive(false);

        if (lemonadeMachineLock != null && rc.lemons >= rc.lemonade_machine_cost)
            lemonadeMachineLock.SetActive(false);

        // Apple unlock from money
        if (appleLock != null && rc.money >= 1f)
            appleLock.SetActive(false);

        // Apple side
        if (appleTreeLock != null && rc.apples >= rc.apple_tree_cost)
            appleTreeLock.SetActive(false);

        if (appleGreenHouseLock != null && rc.apples >= rc.apple_greenhouse_cost)
            appleGreenHouseLock.SetActive(false);

        if (appleJuiceMachineLock != null && rc.apples >= rc.apple_juice_machine_cost)
            appleJuiceMachineLock.SetActive(false);

        if (appleJuiceFactoryLock != null && rc.apples >= rc.apple_juice_factory_cost)
            appleJuiceFactoryLock.SetActive(false);

        // Upgrades
        if (lemonFertilizerLock != null && rc.lemons >= 100f)
            lemonFertilizerLock.SetActive(false);

        if (efficientLemonadeLock != null && rc.lemons >= 200f)
            efficientLemonadeLock.SetActive(false);

        if (appleSeedsLock != null && rc.apples >= 50f)
            appleSeedsLock.SetActive(false);

        if (strongerCrushersLock != null && rc.apples >= 200f)
            strongerCrushersLock.SetActive(false);
    }
}