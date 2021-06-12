using UnityEngine;
using UnityEngine.SceneManagement;

/*Loads a new scene, while also clearing level-specific inventory!*/

public class SceneLoadTrigger : MonoBehaviour
{
    public bool kit = true;
    [SerializeField] string loadSceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (kit)
        {
            if (col.gameObject == NewPlayer.Instance.gameObject)
            {
                GameManager.Instance.hud.loadSceneName = loadSceneName;
                GameManager.Instance.inventory.Clear();
                GameManager.Instance.hud.animator.SetTrigger("coverScreen");
                enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (kit)
            return;
        AbiltityUnlockManager unlockManager = other.GetComponent<AbiltityUnlockManager>();
        if (unlockManager)
            SceneManager.LoadScene(loadSceneName);
    }
}
