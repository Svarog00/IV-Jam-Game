using Assets.Code.GUI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class AreaController : MonoBehaviour
    {
        public static event EventHandler<OnEnterAreaEventArgs> OnEnterAreaEventHandler;

        protected PathBuildingManager PathManager;
        protected Area Model;

        [SerializeField] private GameObject _mistVisual;

        private bool _isRevealed;

        [SerializeField] private GameObject _pathFlag;
        //Path flag is just a game object with flag sprite

        private void Start()
        {
            PathManager = FindObjectOfType<PathBuildingManager>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D[] targetObjects = Physics2D.OverlapPointAll(mousePosition);
                foreach(Collider2D targetObject in targetObjects)
                {
                    if(targetObject.gameObject == gameObject)
                    {
                        MarkArea();
                    }
                }
            }
        }

        public void Initialize(Area areaModel)
        {
            Model = Instantiate(areaModel);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && _isRevealed == false)
            {
                RevealArea();
            }

            NotifyInterface();
        }

        public void NotifyInterface()
        {
            OnEnterAreaEventHandler?.Invoke(this, new OnEnterAreaEventArgs { TimeCost = Model.TimeCost, EnergyCost = Model.EnergyCost });
        }

        private void RevealArea()
        {
            _isRevealed = true;
            _mistVisual.SetActive(false);
        }

        private void OnDestroy()
        {
            Model.IsMarked = false;
        }

        private void MarkArea()
        {
            if (_isRevealed)
            {
                Debug.Log("Marked");
                if (Model.IsMarked)
                {
                    Model.IsMarked = false;
                    PathManager.DeletePoint(Model);
                    _pathFlag.SetActive(Model.IsMarked);
                }
                else
                {
                    Model.IsMarked = true;
                    PathManager.AddPoint(Model);
                    _pathFlag.SetActive(Model.IsMarked);
                }
            }
        }
    }
}