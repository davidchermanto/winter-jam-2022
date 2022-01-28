using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance;

    [SerializeField] private List<Sprite> branches;

    [SerializeField] private List<Sprite> leafBranchZero;
    [SerializeField] private List<Sprite> leafBranchOne;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetBranch(int index)
    {
        if(index == 0)
        {
            return branches[0];
        }
        else
        {
            return branches[1];
        }
    }

    public Sprite GetLeaf(int branchIndex, int leafIndex)
    {
        if(branchIndex == 0)
        {
            return leafBranchZero[leafIndex];
        }
        else
        {
            return leafBranchOne[leafIndex];
        }
    }


}
