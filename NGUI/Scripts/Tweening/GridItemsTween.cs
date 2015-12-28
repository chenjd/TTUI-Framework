using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridItemsTween : MonoBehaviour
{
    private int time = 0;
    private int childIndex = -1;
    private Transform gridTran;
    private Vector3 firstPos;
    public void MoveGridChildren()
    {
        gridTran = GetComponent<UIGrid>().transform;
        if (gridTran.childCount > 0)
        {
            Transform firstTrans = gridTran.GetChild(0);
            firstPos = firstTrans.localPosition;
            Vector3 myPos = new Vector3(firstPos.x, firstPos.y + 40, firstPos.z);

            for (int i = 0; i < gridTran.childCount; i++)
            {
                Transform child = gridTran.GetChild(i).transform;
                child.localPosition = myPos;
                child.gameObject.SetActive(false);
            }
            ItemsMove();
        }
    }
    private void ItemsMove()
    {
        childIndex++;

        Transform curChild = gridTran.GetChild(childIndex);
        curChild.gameObject.SetActive(true);
        TweenPosition tp1 = TweenPosition.Begin(curChild.gameObject, 0.2f, firstPos);
        if (childIndex < gridTran.childCount - 1)
        {
            StartCoroutine(DelayCallFun.DoDelay(() => { ItemsRowMove(); }, 0.2f));
        }
    }
    private void ItemsRowMove()
    {
        for (int idx = 0; idx < childIndex + 1; idx++)
        {
            Transform oneChild = gridTran.GetChild(idx);
            TweenPosition tp2 = TweenPosition.Begin(oneChild.gameObject, 0.2f, new Vector3(oneChild.localPosition.x + gridTran.gameObject.GetComponent<UIGrid>().cellWidth, oneChild.localPosition.y, oneChild.localPosition.z));
        }
        StartCoroutine(DelayCallFun.DoDelay(() => { ItemsMove(); }, 0.2f));
    }
}

public class DelayCallFun : MonoBehaviour
{
    public static IEnumerator DoDelay(System.Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}
