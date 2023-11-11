using UnityEngine;
using UnityEngine.SceneManagement;

public class showVersion : MonoBehaviour
{
    public GUIStyle _style;

    public void Start()
    {
        _style.fontSize = 9;
    }
    private void OnGUI()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            this.enabled = false;
            return;
        }
        string build = "";

#if UNITY_IOS
        build = "BUILD_" + UnityEditor.PlayerSettings.iOS.buildNumber;
#endif

        string bn = "VERSION_" + Application.version + " " + build;
        GUI.Label(new Rect(10, 5, 300, 50), bn, _style);
    }
}
