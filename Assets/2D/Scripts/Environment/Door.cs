using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2D.Scripts.Environment
{
    [RequireComponent (typeof(Collider2D))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private TMP_Text _interactionText;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            _interactionText.gameObject.SetActive(true);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(1);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            _interactionText.gameObject.SetActive(true);
        }
    }
}