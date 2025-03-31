using TMPro;
using UnityEngine;

namespace _3D.Scripts.UI
{
    public class Totem : MonoBehaviour
    {
        [SerializeField] private TMP_Text statisticText;
        [SerializeField] private TMP_Text interactionText;
        [SerializeField] private Teleport _teleportToOpen;

        private EnemyZone _zone;
        private bool _keyGathered;
        private bool _canInteract;

        public void Initialize(EnemyZone zone)
        {
            _zone = zone;
        }

        public void UpdateText(int fedEnemiesCount, int allEnemiesCount)
        {
            statisticText.text = $"Вы накормили {fedEnemiesCount}/{allEnemiesCount} обезьян";
        }

        private void OnTriggerEnter(Collider other)
        {
            statisticText.gameObject.SetActive(true);
            if (!other.CompareTag("Player"))
                return;

            if (_keyGathered)
            {
                interactionText.gameObject.SetActive(false);
            }

            if (_canInteract)
            {
                interactionText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            if (_canInteract && Input.GetKeyDown(KeyCode.E))
            {
                _zone.GatherKey();
                _keyGathered = true;
                interactionText.gameObject.SetActive(false);
                _canInteract = false;
                _teleportToOpen.canEnter = true;
                _teleportToOpen.OperDoor();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            statisticText.gameObject.SetActive(false);
            interactionText.gameObject.SetActive(false);
        }

        public void SetInteractionState(bool state) => _canInteract = state;
    }
}
