using Assets.Code.GUI;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class AreaController : MonoBehaviour
    {
        protected PathBuildingManager PathManager;
        protected ResourcesUI Interface;
        protected Area Model;

        [SerializeField] private GameObject _mistVisual;

        private bool _isRevealed;

        [SerializeField] private GameObject _pathFlag;
        //Path flag is just a game object with flag sprite

        private void Start()
        {
            Interface = FindObjectOfType<ResourcesUI>();
            PathManager = FindObjectOfType<PathBuildingManager>();
        }

        public void Initialize(Area areaModel)
        {
            Model = Instantiate(areaModel);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && _isRevealed == false)
            {
                RevealArea();
            }
            Interface.SetValues(Model.TimeCost, Model.EnergyCost);
        }

        private void RevealArea()
        {
            _isRevealed = true;
            _mistVisual.SetActive(false);
        }

        private void OnMouseDown()
        {
            MarkArea();
        }

        private void MarkArea()
        {
            if (_isRevealed)
            {
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