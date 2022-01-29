using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

public class TreeModule : MonoBehaviour
{
    [SerializeField] private SpriteRenderer branch;
    [SerializeField] private SpriteRenderer leaves;
    [SerializeField] private Light2D treeLight;

    [SerializeField] private int branchNo;
    [SerializeField] private int leavesNo;

    [SerializeField] private bool flipped = false;

    public void Setup(int layer)
    {
        branchNo = Random.Range(0, 2);
        leavesNo = Random.Range(0, 4);

        int flip = Random.Range(0, 2);
        if(flip == 1)
        {
            flipped = true;
        }

        branch.flipX = flipped;
        leaves.flipX = flipped;

        branch.sprite = TreeManager.Instance.GetBranch(branchNo);
        leaves.sprite = TreeManager.Instance.GetLeaf(branchNo, leavesNo);

        branch.color = ColorThemeManager.Instance.GetColorPack().darkTwo;
        leaves.color = ColorThemeManager.Instance.GetColorPack().antaTwo;
        treeLight.color = ColorThemeManager.Instance.GetColorPack().antaTwo;

        leaves.sortingOrder = layer + Constants.playerSpriteOffset;
        branch.sortingOrder = layer + Constants.playerShadowOffset;

        StartCoroutine(FadeIn());
    }

    public void OnTileDie()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn(float duration = 0.8f)
    {
        float timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime / duration;

            branch.color = new Color(branch.color.r, branch.color.g, branch.color.b, Mathf.SmoothStep(0, 1, timer));
            leaves.color = new Color(leaves.color.r, leaves.color.g, leaves.color.b, Mathf.SmoothStep(0, 1, timer));
            treeLight.color = new Color(treeLight.color.r, treeLight.color.g, treeLight.color.b, Mathf.SmoothStep(0, 1, timer));

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FadeOut(float duration = 0.8f)
    {
        float timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime / duration;

            branch.color = new Color(branch.color.r, branch.color.g, branch.color.b, Mathf.SmoothStep(1, 0, timer));
            leaves.color = new Color(leaves.color.r, leaves.color.g, leaves.color.b, Mathf.SmoothStep(1, 0, timer));
            treeLight.color = new Color(treeLight.color.r, treeLight.color.g, treeLight.color.b, Mathf.SmoothStep(1, 0, timer));

            yield return new WaitForEndOfFrame();
        }
    }

}
