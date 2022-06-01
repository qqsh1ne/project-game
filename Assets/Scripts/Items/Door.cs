using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isOpening;

    private void Update()
    {
        if (!_isOpening)
            animator.SetBool("IsClosed", true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Unit>().CompareTag("Player")) return;
        _isOpening = true;
        animator.SetBool("IsClosed", false);
        
        Invoke(nameof(LaunchBossLevel), 5f);
    }
    
    private void LaunchBossLevel() => SceneManager.LoadScene (sceneName:"Boss Level");
}