using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;
using EcsSystems;
using EcsStructs;

public class World_ClassicMode : MonoBehaviour
{
    public StaticData _stData;
    public LevelData _level;
    public SceneData _scene;

    EcsWorld _world = null;
    EcsSystem _allSys = null, _updSys = null, _fixUpdSys = null;

    void Start()
    {
        _world = new EcsWorld();
        var grid = new HexGridData();

        ISystem colorSelectorSystem;
        if (_level.IsRandomColors)
            colorSelectorSystem = new ColorSelectorSystem_Random();
        else
            colorSelectorSystem = new ColorSelectorSystem_Chain();


        _updSys = new EcsSystem(_world)
            .Add(new GridCreatorSystem())
            .Add(new BallsLoaderSystem())

            .Add(new CameraMoverSystem())
            .Add(new BallsAreaMoverSystem())
            .Add(new FirePointMoverSystem())

            .Add(new UserInputSystem());


        _fixUpdSys = new EcsSystem(_world)
            .Add(new ThrowBallSystem())
            .Add(colorSelectorSystem)

            .Add(new PlayerBallSpawner())
            .Add(new AimLineSystem())
            .Add(new BallsColideSystem())
            .Add(new BallSpawnerSystem())
            .Add(new BlopedBubbleSystem())

            .Add(new DestroySystem())
            //TODO WinSystem

            .OneFrame<ButtonUpTag>()
            .OneFrame<ButtonDownTag>();


        _allSys = new EcsSystem(_world)
            .Add(_updSys)
            .Add(_fixUpdSys)
            .Inject(_stData)
            .Inject(_level)
            .Inject(_scene)
            .Inject(grid);

        _allSys.Init();
    }

    void Update()
    {
        _updSys.Upd();
    }

    void FixedUpdate()
    {
        _fixUpdSys.Upd();
    }
}
