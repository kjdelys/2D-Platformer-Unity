using UnityEngine;

public class FloatingGift : MonoBehaviour
{
    public float amplitude = 0.5f; // Amplitude du mouvement zigzag
    public float frequency = 1f; // Fréquence du mouvement zigzag
    public float speed = 2f; // Vitesse à laquelle la lettre monte
    public float disappearHeight = 5f; // Hauteur à laquelle la lettre disparaîtra

    private Vector3 startPosition;
    private float startTime;
    private float initialPhase; // Phase initiale pour randomiser la direction

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
        initialPhase = Random.Range(0, 2 * Mathf.PI); // Randomisation de la phase
    }

    void Update()
    {
        // Mouvement en zigzag avec phase initiale aléatoire
        float x = startPosition.x + amplitude * Mathf.Sin((Time.time - startTime) * frequency + initialPhase);
        float y = startPosition.y + speed * (Time.time - startTime);

        // Mise à jour de la position
        transform.position = new Vector3(x, y, startPosition.z);

        // Disparaître si la hauteur est atteinte
        if (transform.position.y >= startPosition.y + disappearHeight)
        {
            Destroy(gameObject);
        }
    }
}
