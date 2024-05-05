using UnityEngine;
using UnityEngine.SceneManagement;

namespace DATA.Scripts.Core
{
    public class LoadLevel : MonoBehaviour
    {
        public string levelName;
        public void LoadScene()
        {
            SceneManager.LoadScene(levelName);
        }   
    }
}