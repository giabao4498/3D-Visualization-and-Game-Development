using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    public static CreateBoard board;
    public List<Sprite> club = new List<Sprite>();
    public GameObject prefab;
    public int M, N;
    void Start()
    {
        board = GetComponent<CreateBoard>();
        Vector2 child = prefab.GetComponent<SpriteRenderer>().bounds.size;
        GameObject[,] matrix = new GameObject[M, N];
        float startX = -8;
        float startY = -4;
        GameObject tile;
        Sprite sprite;
        for(int i = 0; i < M; ++i)
        for(int j = 0; j < N; ++j)
        {
            tile = Instantiate(prefab, new Vector3(startX + child.x * i, startY + child.y * j, 0), prefab.transform.rotation);
            matrix[i,j] = tile;
            tile.transform.parent = transform;
            sprite = club[Random.Range(0, club.Count)];
            tile.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}