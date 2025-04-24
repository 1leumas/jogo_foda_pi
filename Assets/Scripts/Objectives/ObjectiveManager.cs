using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [System.Serializable]
    public class Objetivo
    {
        public int id;
        public Transform target;
    }

    public List<Objetivo> objectiveList;

    private Dictionary<int, Transform> objectives;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        objectives = new Dictionary<int, Transform>();

        foreach (var obj in objectiveList)
        {
            if (!objectives.ContainsKey(obj.id))
            {
                objectives.Add(obj.id, obj.target);
            }
        }
    }

    public Transform GetObjetivoPorId(int id)
    {
        objectives.TryGetValue(id, out Transform target);
        return target;
    }
}
