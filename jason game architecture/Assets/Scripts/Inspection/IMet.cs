﻿using System;
using UnityEngine;

public interface IMet
{
    string NotMetMessage { get; }
    string MetMessage { get; }

    bool Met();
}
