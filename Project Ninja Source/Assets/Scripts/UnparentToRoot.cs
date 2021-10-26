using UnityEngine;

public class UnparentToRoot : MonoBehaviour
{
    private void Awake() => transform.SetParent(transform.root);
}
