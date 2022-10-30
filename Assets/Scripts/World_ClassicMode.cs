using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;

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
        var grid = new HexGrid(11, 500);

        _updSys = new EcsSystem(_world);
        _fixUpdSys = new EcsSystem(_world);

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
