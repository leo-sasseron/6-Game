using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScreenControler : MonoBehaviour {
    public void EndCredits()
    {
        SceneManager.LoadScene(0);
    }
}
