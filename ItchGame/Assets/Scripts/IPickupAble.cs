﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupAble
{
    void PickupItem();

    void UseItem(GameObject obj);
}