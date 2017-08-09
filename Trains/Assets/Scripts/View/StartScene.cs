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
    [SerializeField]
    private InputField safeZoneSize;
    [SerializeField]
    private InputField cargoEffect;
    [SerializeField]
    private InputField arrowCooldown;
    [SerializeField]
    private InputField barrierCooldown;
    [SerializeField]
    private InputField ambushCooldown;

    private void Start()
    {
        fieldLength.text = GameSettings.fieldLength.ToString();
        firstLineX.text = GameSettings.firstLineX.ToString();
        lineDeltaX.text = GameSettings.lineDeltaX.ToString();
        cargoEffect.text = GameSettings.cargoEffect.ToString();
        safeZoneSize.text = GameSettings.safeZoneSize.ToString();

        arrowCooldown.text = GameSettings.cooldowns[ActionObjectType.Arrow].ToString();
        barrierCooldown.text = GameSettings.cooldowns[ActionObjectType.Barrier].ToString();
        ambushCooldown.text = GameSettings.cooldowns[ActionObjectType.Ambush].ToString();
    }

    public void LoadNextScene()
    {
        GameSettings.fieldLength = int.Parse(fieldLength.text);
        GameSettings.firstLineX = int.Parse(firstLineX.text);
        GameSettings.lineDeltaX = int.Parse(lineDeltaX.text);
        GameSettings.cargoEffect = int.Parse(cargoEffect.text);
        GameSettings.safeZoneSize = int.Parse(safeZoneSize.text);

        GameSettings.cooldowns[ActionObjectType.Arrow] = int.Parse(arrowCooldown.text);
        GameSettings.cooldowns[ActionObjectType.Barrier] = int.Parse(barrierCooldown.text);
        GameSettings.cooldowns[ActionObjectType.Ambush] = int.Parse(ambushCooldown.text);
        SceneManager.LoadScene(1);
    }

}
