using UnityEngine;

public class ComeOut : MonoBehaviour
{
    [SerializeField] private ComingOutEnemy firstEnemy;
    [SerializeField] private ComingOutEnemy secondEnemy;
    [SerializeField] private ComingOutEnemy thirdEnemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Unit>().CompareTag("Player")) return;
        firstEnemy.gameObject.SetActive(true);
        secondEnemy.gameObject.SetActive(true);
        thirdEnemy.gameObject.SetActive(true);
    }
}
