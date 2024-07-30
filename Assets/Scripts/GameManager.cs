using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text rabbitText;
    [SerializeField] private TMP_Text wordText;

    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int rabbitCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;
    private bool popRabbit = true;
    private Vector3 playerPosition;

    public GameObject RabbitPrefab;
    public GameObject LevelCompleteDoor;
    public GameObject RabbitSpawnLeftLimit;
    public GameObject RabbitSpawnRightLimit;

    private string[] randomWords = new string[] { "TORO", "GIBBON" };
    private string currentWord = "Default";

    //Level Complete

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;


   
    private int totalCoins = 0;

    public static string GetCurrentWord()
    {
        return instance.currentWord;
    }

    public bool WordCompleted { get; private set; } = false;

    public void AddLetter(char letter)
    {
        char[] wordDisplayArray = wordText.text.ToCharArray(); // Convertissez le texte actuel en tableau de caractères

        // Parcourez chaque caractère dans currentWord pour voir si la lettre ajoutée correspond
        for (int i = 0; i < currentWord.Length; i++)
        {
            if (currentWord[i] == letter && wordDisplayArray[i] == '_') // Vérifiez si la lettre correspond et si la position est encore non complétée
            {
                wordDisplayArray[i] = letter; // Remplacez le tiret bas par la lettre
                break; // Arrêtez la boucle après avoir placé la lettre pour la première fois
            }
        }

        wordText.text = new string(wordDisplayArray); // Convertissez le tableau de caractères de retour en chaîne et mettez à jour l'affichage
        CheckWordCompletion();
    }

    /*private void UpdateWordDisplay()
    {
        wordText.text = currentWord;
    }*/

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        playerPosition = playerController.transform.position;
        PopRabbits();

        FindTotalPickups();
        StartCoroutine(CallPopRabbitPeriodically());

        SetRandomWord();
    }

    private void CheckWordCompletion()
    {
        WordCompleted = (wordText.text.IndexOf('_') == -1); // Vérifie si toutes les positions sont remplies (aucun '_' trouvé)
    }
    
    private void SetRandomWord()
    {
        currentWord = randomWords[Random.Range(0, randomWords.Length)];
        wordText.text = new string('_', currentWord.Length);  // Mettre à jour le texte du mot aléatoire
    }

    private IEnumerator CallPopRabbitPeriodically()
    {
        while (popRabbit) 
        {
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            int randomNumber = UnityEngine.Random.Range(1, 4);
            PopRabbits(randomNumber);
        }
    }

    private void Update()
    {
        if (rabbitCount > 5)
        {
            LevelCompleteDoor.SetActive(true);
        }
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }

    public void IncrementRabbitCount()
    {
        rabbitCount++;
        UpdateGUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
        rabbitText.text = rabbitCount.ToString();
    }

    public void Death()
    {
        if (!isGameOver)
        {
            // Disable Mobile Controls
            UIManager.instance.DisableMobileControls();
            // Initiate screen fade
            UIManager.instance.fadeToBlack = true;

            // Disable the player object
            playerController.gameObject.SetActive(false);

            // Start death coroutine to wait and then respawn the player
            StartCoroutine(DeathCoroutine());

            // Update game state
            isGameOver = true;

            popRabbit = true;

            // Log death message
            Debug.Log("Died");
        }
    }

    public void PopRabbits(int numberOfRabbits = 10)
    {
        for (int i = 0; i < numberOfRabbits; i++)
        {
            PopRabbit();
        }
    }

    public void PopRabbit()
    {
        float xPosition = Random.Range(RabbitSpawnLeftLimit.transform.position.x, RabbitSpawnRightLimit.transform.position.x);
        float yPosition = Random.Range(0f, 5f);

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0);

        Instantiate(RabbitPrefab, spawnPosition, Quaternion.identity);      
    }
 
    public void FindTotalPickups()
    {

        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }
           
        }


      
    }
    public void LevelComplete()
    {
       

        popRabbit = false;
        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "NIVEAU TERMINE";



        levelCompleteCoins.text = "PIECES RAMASSEES : "+ coinCount.ToString() +" / " + totalCoins.ToString();
 
    }
   
    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        playerController.transform.position = playerPosition;

        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Check if the game is still over (in case player respawns earlier)
        if (isGameOver)
        {
            SceneManager.LoadScene(1);

            
        }
    }

}
