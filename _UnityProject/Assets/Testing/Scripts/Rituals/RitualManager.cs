using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualManager
{
    List<BasicChildrenAI> Children;

    public RitualManager()
    {
        Children = new List<BasicChildrenAI>();
    }

    public void UpdateRituals()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            if (Children[i].Ritual.InitRitual == false && Children[i].Ritual.Started)
            {
                Children[i].Ritual.InitRitual = true;
                Children[i].InitState(BasicChildrenAI.ChildState.Ritual);
            }

            Children[i].Ritual.Condition();

        }
    }

    public void EndDay()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].Ritual.InitRitual = false;

            if (Children[i].Ritual.ConditionComplete)
            {
                Children[i].gameObject.SetActive(false);
            }
        }
    }

    public void RegisterChild(BasicChildrenAI aChild)
    {
        Children.Add(aChild);
    }
}
