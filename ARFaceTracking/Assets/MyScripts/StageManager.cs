using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//씬을 로드하기 위해 선언
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
        public void LoadNextScene(string sceneName)
    {
        //print("게임씬을 로드합니다.");
        SceneManager.LoadScene(sceneName);
        //SceneManager.LoadScene(1);    //지정한 씬의 인덱스
    }
     
}
