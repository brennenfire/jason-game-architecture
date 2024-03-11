using System;
using UnityEngine;

public interface IMet
{
    string NotMetMessage { get; }

    bool Met();
}
