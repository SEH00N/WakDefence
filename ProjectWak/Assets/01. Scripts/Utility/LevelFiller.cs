using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelFiller : MonoBehaviour
{
	[SerializeField] GameObject levelPrefab = null;
    [SerializeField] float distance = 1.5f;
    [SerializeField] float delay = 0.025f;
    [SerializeField] float size = 2f;
    [SerializeField] int stackSize = 5000;

    private readonly Vector3[] direction = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private int stack = 0;

    [ContextMenu("Fill")]
    public void StartFill()
    {
        stack = 0;
        Fill(gameObject);
    }

    private async void Fill(GameObject current = null)
    {
        if(current == null)
            return;

        for(int i = 0; i < 4; ++i)
        {
            Debug.DrawLine(current.transform.position, current.transform.position + direction[i] * distance, Color.red, 5f);
            int prev = Interlocked.Add(ref stack, 1);
            if(prev > stackSize)
                return;
            
            await Task.Delay((int)(delay * 1000));

            if(Physics.Raycast(current.transform.position, direction[i], distance))
                continue;
            
            GameObject next = PrefabUtility.InstantiatePrefab(levelPrefab, current.transform.parent) as GameObject;
            next.transform.position = current.transform.position + direction[i] * size;
            Fill(next);
        }
    }
}
