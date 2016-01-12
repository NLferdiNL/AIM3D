using UnityEngine;
using System.Collections;

// The Grow Phase
// The complete start.
// Gather particles and make a earthly ball.
public class GrowPhase : Phase {

    // To edit meditation and attention variables from the Neurosky input.
    private NeuroData neuroData;

    // The growing particle GameObject will be activated to simulate meteors flying to the growing planetx
    [SerializeField]
    private GameObject particleGroup;

    // The ParticleSystem Components from particleGroup.
    private ParticleSystem[] particles;

    [SerializeField]
    private float particleEmissionRate;

    private void Start() {
        // Need this to obtain the attention and meditation from the Neurosky.  
        neuroData = GameObject.Find("Globals").GetComponent<NeuroData>();
        particles = particleGroup.GetComponentsInChildren<ParticleSystem>();
    }

    // I avoid using the default constructor because I want to activate the class manually.
    // As assets I'll give to it might not be ready until ran.
    public void startUp() {
        started = true;
        InvokeRepeating("timer",0,1);
        print("Growth (1st) phase started!");
        foreach(ParticleSystem particle in particles) {
            // Play all the particle systems.
            particle.Play();
            particleEmissionRate = particle.emissionRate;
        }
    }

    // If ended clean up what's no longer necessary.
    public void stopPhase() {
        CancelInvoke("Timer");
        foreach (ParticleSystem particle in particles) {
            // Play all the particle systems.
            particle.Stop();
        }
        print("Growth (1st) phase ended!");
    }

    // Count time to see how long this phase took.
    void timer() {
        timeTaken[2]++;
        if (timeTaken[2] >= 60) {
            timeTaken[2] = 0;
            timeTaken[1]++;
        }
        if (timeTaken[1] >= 60) {
            timeTaken[1] = 0;
            timeTaken[0]++;
        }
        if (timeTaken[1] >= 1 && timeTaken[2] == 30) {
            fixedGrow *= 2;
        }
    }

    void FixedUpdate() {
        if(started && timeTaken[2] == 0 && timeTaken[1] == 0) {
            // Do not run startUp multiple times.
            // Might cause issues.
            startUp();
        }
        if (started) {
            // The workings behind the two debug variables growBy and shrinkBy.
            if (growBy > 0) {
                grow(growBy);
                growBy = 0;
            } else if (growBy < 0) {
                growBy = 0;
            }

            if (shrinkBy > 0) {
                shrink(shrinkBy);
                shrinkBy = 0;
            } else if (shrinkBy < 0) {
                shrinkBy = 0;
            }

            // Get meditation and attention values
            meditation = neuroData.meditation;
            attention = neuroData.attention;

            // Add the fixed growth.
            grow(fixedGrow / 100 * attention);

            //Edit particle emission based on attention.
            foreach (ParticleSystem particle in particles) {
                particle.emissionRate = (particleEmissionRate / 10) * Mathf.FloorToInt(attention / 10);
                if (particle.emissionRate <= 0) {
                    particle.emissionRate = 1;
                }
            }

            //Do not go behind max size.
            if (planet.transform.localScale.x > maxSize) {
                planet.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                stopPhase();
                started = false;
            }
        }
    }

    public void grow(float size) {
        // Make the planet grow.
        // Basically just add to scale.
        if (planet.transform.localScale.x < maxSize) {
            // My preferred unit of measure is size / 5.
            // So 1 = 0.2 and 5 = 1.
            // Makes it more clear if I do it like this rather than using very small numbers.
            // For example the base growth default is 0.01.
            // If I wanted to use the actual number each time it would be 0.002.
            size /= 5;
            planet.transform.localScale += new Vector3(size, size, size);
            if (planet.transform.localScale.x > maxSize) {
                planet.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            }
        }
    }

    public void shrink(float size) {
        // Mainly a debug function. As I haven't found a use for it yet.
        if (planet.transform.localScale.x > 0.2F) {
            size /= 5;
            planet.transform.localScale -= new Vector3(size, size, size);
            if (planet.transform.localScale.x < 0.2F) {
                planet.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
            }
        }
    }
}
