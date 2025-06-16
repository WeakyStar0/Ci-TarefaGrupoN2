using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class FinalSceneManagerM : MonoBehaviour
{
 [SerializeField]
 private TMP_Text answersText;
 public void StartM()
 {
 answersText.text = GameManager.GetSeconds().ToString();
 }
 public void TestAgain()
 {
 GameManager.Reset();
 SceneManager.LoadScene("MainScene");
 }
}