﻿using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Bootstrapper : MonoBehaviourBase {

    private readonly Dictionary<Type, string> _managerPrefabs = new Dictionary<Type, string>()
        {
            {typeof(MainManager), @"Prefabs/Managers/MainManager"},
            {typeof(DollarStore), @"Prefabs/Managers/DollarStore"},
            {typeof(GuiManager), @"Prefabs/Managers/GuiManager"},
            {typeof(CivilianManager), @"Prefabs/Managers/CivilianManager"},
            {typeof(DuckManager), @"Prefabs/Managers/DuckManager"},
        };

    void Awake()
    {
        Init();
    }
    void OnLevelWasLoaded(int level)
    {
        Init();
    }

    void Init()
    {
        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        var root = FindRootByName("MangerRoot");
        foreach (var prefabToSpawn in _managerPrefabs)
        {
            if (!ObjectOfTypeExistsInScene(prefabToSpawn.Key))
            {
                var createdGameObject = Instantiate(Resources.Load(prefabToSpawn.Value, typeof(GameObject)), Vector3.zero, new Quaternion()) as GameObject;
                //Anything created by the bootstrapper should persist between scenes
                if (createdGameObject != null)
                {
                    Debug.Log("Creating: " + createdGameObject.name);
                    createdGameObject.transform.parent = root.transform;
                    DontDestroyOnLoad(createdGameObject);
                }
            }
        }
    }

    GameObject FindRootByName(string rootName)
    {
        var managerRoot = GameObject.Find(rootName) ?? new GameObject();
        managerRoot.name = rootName;
        DontDestroyOnLoad(managerRoot);
        return managerRoot;
    }
}
