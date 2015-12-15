using UnityEngine;
using System.Collections;

public class Phase : MonoBehaviour {
    // Blank phase.
    // Grants access to Grow and Shrink functions.
    // Just in case I find a use for them later.

    // Has this phase started?
    [SerializeField]
    protected bool started = false;

    // Fixed growth, this is the default value and will be devided by 100 and then multiplied by attention value.
    [SerializeField]
    protected float fixedGrow = 0.01f;

    // A debug variable, instantly makes the planet grow by the entered value and is then reset to 0.
    [SerializeField]
    protected int growBy;

    // A debug variable, instantly makes the planet shrink by the entered value and is then reset to 0.
    [SerializeField]
    protected int shrinkBy;

    // The planet used for the game. Will be shrunk and etc.
    [SerializeField]
    protected GameObject planet;

    // Max size of the planet. Not something intended to be changed but added to the Editor anyway.
    [SerializeField]
    protected int maxSize = 15;

    // Time the current phase took.
    [SerializeField]
    protected int[] timeTaken = new int[] { 0, 0, 0 };

    // Neurosky input variables.
    [SerializeField]
    protected int meditation;

    [SerializeField]
    protected int attention;
}
