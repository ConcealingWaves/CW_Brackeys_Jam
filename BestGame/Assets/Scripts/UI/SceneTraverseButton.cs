using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTraverseButton : MonoBehaviour
{
    [SerializeField] private int to;
    public void Goto()
    {
        if (SceneTraverser.instance == null)
        {
            GameObject traverser = new GameObject();
            traverser.AddComponent<SceneTraverser>();
        }

        SceneTraverser.instance.GoToScene(to);
    }
}
