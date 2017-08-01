using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private Text fieldLength;
    [SerializeField]
    private Text firstLineX;
    [SerializeField]
    private Text lineDeltaX;

    public void LoadNextScene()
    {
        GameSettings.fieldLength = int.Parse(fieldLength.text);
        GameSettings.firstLineX = int.Parse(firstLineX.text);
        GameSettings.lineDeltaX = int.Parse(lineDeltaX.text);

        SceneManager.LoadScene(1);
    }

}
