using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatTarget : MonoBehaviour, ICombatTarget
{
    private List<IProcessHit> _processHits = new List<IProcessHit>();
    public void ProcessHit()
    {
        if (_processHits.Count == 0)
            _processHits = GetComponents<MonoBehaviour>().OfType<IProcessHit>().ToList();

        foreach (var processHit in _processHits)
        {
            processHit.ProcessHit();
        }
    }
}