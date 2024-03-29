using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;
using EcsSystems;
using EcsStructs;

public class World : MonoBehaviour
{
    public static LevelData Level;

    public StaticData _stData;
    public LevelData _level;
    public SceneData _scene;

    EcsWorld _world = null;
    EcsSystem _allSys = null, _updSys = null, _fixUpdSys = null;

    void Start()
    {
        if (Level != null)
        {
            _level = Level;
        }

        _world = new EcsWorld();
        var grid = new HexGridData();

        _updSys = new EcsSystem(_world)
            .Add(new CameraMoverSystem())
            .Add(new FirePointMoverSystem())
            .Add(new BallsAreaMoverSystem())

            .Add(new GridCreatorSystem())
            .Add(new BallsLoaderSystem())

            .Add(new UserInputSystem())
            .Add(new AimLineSystem());


        _fixUpdSys = new EcsSystem(_world)
            .Add(new PlayerBallSpawner())
            .Add(new ThrowBallSystem())
            .Add(new ColorSelectorSystem())

            .Add(new BallsColideSystem())
            .Add(new BallSpawnerSystem())
            .Add(new BlopedBubbleSystem())

            .Add(new DestroySystem())
            .Add(new WinSystem())

            .OneFrame<MarkedBallTag>()
            .OneFrame<CheckWinTag>()
            .OneFrame<ButtonUpTag>()
            .OneFrame<ButtonDownTag>();


        _allSys = new EcsSystem(_world)
            .Add(_updSys)
            .Add(_fixUpdSys)
            .Inject(_stData)
            .Inject(_level)
            .Inject(_scene)
            .Inject(_scene.PauseMenuScript)
            .Inject(grid);

        _allSys.Init();
    }

    void Update()
    {
        if (!_scene.PauseMenuScript.isPause)
            _updSys.Upd();
    }

    void FixedUpdate()
    {
        if (!_scene.PauseMenuScript.isPause)
            _fixUpdSys.Upd();
    }
}
