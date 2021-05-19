﻿// Copyright (C) 2016-2021 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

public class CreateDeckButton : MonoBehaviour
{
    [HideInInspector]
    public DeckBuilderScene scene;

    public void OnButtonPressed()
    {
        scene.CreateNewDeck();
    }
}