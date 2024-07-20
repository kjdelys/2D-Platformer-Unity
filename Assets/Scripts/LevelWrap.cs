using UnityEngine;

public class LevelWrap : MonoBehaviour
{
    public GameObject player; // Référence au joueur
    public Transform leftDuplicate; // Référence au duplicata gauche du niveau
    public Transform rightDuplicate; // Référence au duplicata droit du niveau

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        float rightHalfWidth = rightDuplicate.position.x / 2;
        float leftHalfWidth = leftDuplicate.position.x / 2;

        // Si le joueur se déplace au-delà de la moitié du duplicata droit
        if (playerPosition.x > rightHalfWidth)
        {
            player.transform.position = new Vector3(leftHalfWidth, playerPosition.y, playerPosition.z);
        }
        // Si le joueur se déplace au-delà de la moitié du duplicata gauche
        else if (playerPosition.x < leftHalfWidth)
        {
            player.transform.position = new Vector3(rightHalfWidth, playerPosition.y, playerPosition.z);
        }
    }
}
