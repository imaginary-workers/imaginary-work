using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneChanger : MonoBehaviour
    {
        private void ChangeScene() 
        {
            SceneManager.LoadScene("VictoryScreen");
        }

    }
}
