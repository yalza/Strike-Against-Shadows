using UnityEngine;

namespace DATA.Scripts.UI
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] int sceneIndex;
        
        private void Start()
        {
            Invoke(nameof(LoadScene), 3f);
        }
        
        private void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
    }
}
