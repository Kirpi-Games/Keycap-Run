using Akali.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Akali.Ui_Materials.Scripts.Components
{
    public class NextButton : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(GoToNextLevel);
        }

        private void OnDisable()
        {
            gameObject.GetComponent<Button>().onClick.RemoveListener(GoToNextLevel);
        }

        public void GoToNextLevel()
        {
            AkaliLevelManager.Instance.levels.GoToNextLevel(true);
        }
    }
}