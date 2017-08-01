using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private InputField fieldLength;
    [SerializeField]
    private InputField firstLineX;
    [SerializeField]
    private InputField lineDeltaX;

    private void Start()
    {
        fieldLength.text = GameSettings.fieldLength.ToString();
        firstLineX.text = GameSettings.firstLineX.ToString();
        lineDeltaX.text = GameSettings.lineDeltaX.ToString();
    }

    public void LoadNextScene()
    {
        GameSettings.fieldLength = int.Parse(fieldLength.text);
        GameSettings.firstLineX = int.Parse(firstLineX.text);
        GameSettings.lineDeltaX = int.Parse(lineDeltaX.text);

        SceneManager.LoadScene(1);
    }

}
