using UnityEngine;
using System.Collections.Generic;

public class FloatingGift : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f; 
    public float speed = 2f; 
    public float disappearHeight = 5f; 

    private Vector3 startPosition;
    private float startTime;
    private float initialPhase;

    private char randomLetter;

    private SpriteRenderer spriteRenderer;

    // Listes pour l'éditeur
    [SerializeField] private List<char> keys = new List<char>();
    [SerializeField] private List<Sprite> values = new List<Sprite>();

    // Dictionnaire interne
    private Dictionary<char, Sprite> letterSprites = new Dictionary<char, Sprite>();

    void InitializeDictionary()
    {
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            letterSprites[keys[i]] = values[i];
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeDictionary();
        startPosition = transform.position;
        startTime = Time.time;
        initialPhase = Random.Range(0, 2 * Mathf.PI); 
        SetRandomLetter();

        SetupSprite(randomLetter);
    }

    void Update()
    {

        float x = startPosition.x + amplitude * Mathf.Sin((Time.time - startTime) * frequency + initialPhase);
        float y = startPosition.y + speed * (Time.time - startTime);

        transform.position = new Vector3(x, y, startPosition.z);

        if (transform.position.y >= startPosition.y + disappearHeight)
        {
            Destroy(gameObject);
        }
    }

    void SetupSprite(char letter)
    {
        if (letterSprites.ContainsKey(letter))
        {
            spriteRenderer.sprite = letterSprites[letter];
        }
        else
        {
            Debug.LogError("Sprite not found for letter: " + letter);
        }
    }

    private void SetRandomLetter()
    {
        string currentWord = GameManager.GetCurrentWord(); 
        if (!string.IsNullOrEmpty(currentWord))
        {
            randomLetter = currentWord[Random.Range(0, currentWord.Length)];
        }
    }
}
