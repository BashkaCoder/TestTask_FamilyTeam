using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3D.Scripts
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private EnemyZone _zone;
        [SerializeField] private TMP_Text _teleportText;
        [SerializeField] private Teleport _correspondingTeleport;
        [SerializeField] private Sprite _openDoorSprite;
        [SerializeField] private Sprite _closedDoorSprite;
        public Transform spawnPoint;
        public bool canEnter;
        public bool shouldEnableZoneView;
        public bool shouldCloseDoor;

        private const string CanEnterText = "Нажмите E, чтобы войти";
        private string _cannotEnterText = "Сперва помолись алтарю";

        private void Start()
        {
            if (canEnter) GetComponent<SpriteRenderer>().sprite = _openDoorSprite;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _teleportText.transform.parent.gameObject.SetActive(true);
            _teleportText.text = canEnter ? CanEnterText : _cannotEnterText;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            if (canEnter && Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = _correspondingTeleport.spawnPoint.position;
                ZoneView.Instance.gameObject.SetActive(shouldEnableZoneView);
                if (_zone != null)
                {
                    _zone.UpdateZoneView();
                }

                if (shouldCloseDoor)
                {
                    _correspondingTeleport.CloseDoor();
                }

                if (EnemyZoneManager.AllZonesCleared())
                {
                    SceneManager.LoadScene(2);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _teleportText.transform.parent.gameObject.SetActive(false);
        }

        public void OperDoor() => GetComponent<SpriteRenderer>().sprite = _openDoorSprite;

        public void CloseDoor()
        {
        
            GetComponent<SpriteRenderer>().sprite = _closedDoorSprite;
            _correspondingTeleport.canEnter = false;
            canEnter = false;
            _cannotEnterText = "Вы накормили всех обезьян";
            _correspondingTeleport._cannotEnterText = _cannotEnterText = "Эти обезьяны накормлены";
            ZoneView.Instance.gameObject.SetActive(false);
        }
    }
}
