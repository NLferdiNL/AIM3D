using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    // Could be used to start the destroying process in the editor. Using alive boolean is much easier.
    [SerializeField]
    private int health = 10;

    // Used for the damage method. Could also be used to trigger the destroying process.
    [SerializeField]
    private bool alive = true;

    // Just a debounce to prevent multiple attempts at running the destroying process.
    private bool destroying = false;

    // Need this to access the position and edit it.
    private Transform tf;

    // Need to create it outside loops and make it global that way to not have
    // the loop reset it to 0 preventing the removal.
    private float destroyingStep = 0;

    // Every building is a different size and will require different steps.
    [SerializeField]
    private float toDestroySteps = 24;

    void Start() {
        tf = this.GetComponent<Transform>();
    }

    void FixedUpdate() {
        // This will start the destroying process.
        // Below it also ends it to save up resources.
        if (!alive) {
            if (!destroying) {
                //Reminder: Trigger particle effect.
                InvokeRepeating("destroy", 0, 0.005f);
                destroying = true;
            }
        }

        // If the health is modified without the use of the damage method still destroy the building.
        if (health <= 0) {
            alive = false;
        }

        // Can't have the building go down infinitely, that would only take precious resources.
        // Especially when there's a lot of instances of this class.
        if (destroyingStep >= toDestroySteps) {
            CancelInvoke("destroy");
            Destroy(this.gameObject);
            // Reminder: Summon a destroyed building.
        }
    }

    void destroy() {
        // Add step and go down a step.
        destroyingStep += 0.1f;
        tf.position -= tf.up / 100;
    }

    /// <summary>
    /// Deal damage to me.
    /// </summary>
    /// <param name="dmg">How much damage you deal.</param>
    // To allow outside sources doing damage to this building.
    public void damage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            alive = false;
        }
    }
}
