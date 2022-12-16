using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    [SerializeField] Transform targetParent;
    [SerializeField] Transform start;
    [SerializeField] Transform mirrorPrefab;
    [SerializeField] Transform markPrefab;
    target[] targets;
    target end;

    private void OnEnable()
    {
        targets = targetParent.GetComponentsInChildren<target>();
    }

    public void generate()
    {
        foreach (var tgt in targets)
        {
            tgt.SetEndTrigger(false);
        }

        int index = Random.Range(0, targets.Length);
        end = targets[index];
        end.SetEndTrigger(true);


        //end.gameObject.SetActive(false);

        Vector2Int cur = new Vector2Int(-1, 0);
        Vector2Int dir = Vector2Int.right;

        dfs(cur, dir);
    }

    public bool dfs(Vector2Int pos, Vector2Int dir)
    {
        pos += dir;

        if (pos == end.cord && dir == end.dir)
            return true;

        if (pos.x > 4 || pos.x < 0 || pos.y > 4 || pos.y < 0)
            return false;

        // each mirror only reflect once
        if (MapLocator.instance.ContainsKey(pos))
            return false;

        //Transform mark = Instantiate(markPrefab, null);
        //mark.position = MapLocator.instance.GetCoordinatePosition(pos.x, pos.y);

        int addon = Random.Range(0, 10);
        int[] px = new int[3] { 0, 1, 2 };


        Vector2Int nextDir = Vector2Int.zero;

        for (int i = 0; i < px.Length; i++)
        {
            //random order of the index
            int idx = (px[i] + addon) % 3;
            if (idx == 0)
            {
                // keep forward
                nextDir = dir;
                MapLocator.instance.AddItem(pos, null);
            }
            else if (idx == 1)
            {
                nextDir.x = dir.y;
                nextDir.y = dir.x;
                Transform mirror = Instantiate(mirrorPrefab, null);
                mirror.position = MapLocator.instance.GetCoordinatePosition(pos.x, pos.y);
                mirror.eulerAngles = new Vector3(0f, 0f, 45.0f);
                MapLocator.instance.AddItem(pos, mirror.gameObject);
            }
            else if (idx == 2)
            {
                nextDir.x = -dir.y;
                nextDir.y = -dir.x;
                Transform mirror = Instantiate(mirrorPrefab, null);
                mirror.position = MapLocator.instance.GetCoordinatePosition(pos.x, pos.y);
                mirror.eulerAngles = new Vector3(0f, 0f, 135.0f);
                MapLocator.instance.AddItem(pos, mirror.gameObject);
            }

            if (dfs(pos, nextDir))
            {
                return true;
            }
            MapLocator.instance.RemoveItem(pos);
        }

        pos -= dir;
        //Destroy(mark.gameObject);
        return false;
    }

    private void OnValidate()
    {
        //if (targetParent == null)
        //    targetParent = GameObject.Find("TargetsPanel").transform;

        //targets = targetParent.GetComponentsInChildren<target>();

        //if (targets.Length <= 0)
        //    return;

        ////give index according to child index
        ////up
        //for (int i = 0; i < 5; i++)
        //{
        //    targets[i].dir = new Vector2Int(0, 1);
        //    targets[i].cord = new Vector2Int(i % 5, 4);
        //}

        ////down
        //for (int i = 5; i < 10; i++)
        //{
        //    targets[i].dir = new Vector2Int(0, -1);
        //    targets[i].cord = new Vector2Int(i % 5, 0);
        //}

        ////right
        //for (int i = 10; i < 15; i++)
        //{
        //    targets[i].dir = new Vector2Int(1, 0);
        //    targets[i].cord = new Vector2Int(4, i % 5);
        //}

        //// left
        //for (int i = 15; i < 20; i++)
        //{
        //    targets[i].dir = new Vector2Int(-1, 0);
        //    targets[i].cord = new Vector2Int(0, i % 5);
        //}
    }
}
