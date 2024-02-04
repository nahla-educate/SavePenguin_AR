using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class BtnMenu : MonoBehaviour
{
    public GameObject panelInstruction;
    

    public void ActiveInstruction()
    {
        panelInstruction.SetActive(true);
    }
    public void DesactiveInstruction()
    {
        panelInstruction.SetActive(false);
    }
    public void StartScene()
    {
        SceneManager.LoadScene("Puzzle2D");
    }

    public void RetourScene()
    {
        SceneManager.LoadScene("MEn");
    }

    public void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextScene()
    {
        SceneManager.LoadScene("BatarokoJump");
    }
    public void NextScene3D()
    {
        SceneManager.LoadScene("PenguinScene3D");
    }
    public void NextScene2()
    {
        SceneManager.LoadScene("Coloriage");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quitter l'application dans le build
        Application.Quit();
#endif
    }

}
