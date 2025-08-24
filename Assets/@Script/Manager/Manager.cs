using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    public static Manager Instance {  get { Init(); return _instance; } }

    private CharacterManager _character = new CharacterManager();
    public static CharacterManager Character { get { return Instance._character; } }

    private ResourcesManager _resources = new ResourcesManager();
    public static ResourcesManager Resources { get { return Instance._resources; } }

    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    private SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    private static CameraController _cam;
    public static CameraController Camera { get { CameraInit(); return _cam; } private set { _cam = value; } }     
    public static void CameraInit()
    {
        if(_cam != null)
            return;

        GameObject cam = Resources.Instantiate("Main Camera");
        Camera = cam.GetOrAddComponent<CameraController>();
    }
    public void SetCamera(CameraController cam)
    {
        Camera = cam;
    }

    public static PlayerController Player { get; private set; }
    public void SetPlayer(PlayerController player)
    {
        Player = player;
    }
    private static void Init()
    {
        if (_instance != null)
            return;

        GameObject go = GameObject.Find("@Manager");
        if(go == null)
        {
            go = new GameObject("@Manager");
            go.AddComponent<Manager>();
        }
        _instance = go.GetComponent<Manager>();
        DontDestroyOnLoad(go);
    }
}
