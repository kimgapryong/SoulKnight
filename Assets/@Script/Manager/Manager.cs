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
    private SkillManager _skill = new SkillManager();
    public static SkillManager Skill { get { return Instance._skill; } }
    private MonsterManager _monster = new MonsterManager();
    public static MonsterManager Monster { get { return Instance._monster; } }

    private static CameraController _cam;
    public static CameraController Camera { get { CameraInit(); return _cam; } private set { _cam = value; } }     

    private RandomManager _random = new RandomManager();    
    public static RandomManager Random { get { return Instance._random; } }
    private ItemManager _item = new ItemManager();
    public static ItemManager Item { get { return Instance._item; } }
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

    public static GameObject _pos;
    public static GameObject Pos { get { PosInit(); return _pos; } }
    public static void PosInit()
    {
        if (_pos != null)
            return;

        _pos = Resources.Instantiate("CharacterPos");
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
