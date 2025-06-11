using UnityEngine;

public class TreeCutable : ToolHit
{
    public override void Hit()
    {
        Destroy(gameObject);
    }
}
