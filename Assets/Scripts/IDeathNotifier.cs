using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeathNotifier
{
    void SubscribeListener(IListener l);
}
