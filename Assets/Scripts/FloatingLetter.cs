using UnityEngine;
using System.Collections.Generic;

public class FloatingLetter : FloatingGift
{

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.AddLetter(randomLetter);
        }
    }
}
