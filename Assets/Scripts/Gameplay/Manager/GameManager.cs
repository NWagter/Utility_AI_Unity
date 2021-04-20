using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private struct SpawnPoint
    {
        public Transform spawnPoint;
        public bool taken;

        public Controller SpawnController(GameObject a_controller)
        {
            var controller = Instantiate(a_controller, spawnPoint.position, Quaternion.identity);
            taken = true;

            return controller.GetComponent<Controller>();
        }
    }

    [SerializeField]
    private List<SpawnPoint> m_spawnPoints;
    [SerializeField]
    private GameObject m_enemyObject;

    private void Awake()
    {
        int spawnId = 0;

        for(int i = 0; i < m_spawnPoints.Count;i++)
        {
            if (!m_spawnPoints[i].taken)
            {
                spawnId++;
                var controller = m_spawnPoints[i].SpawnController(m_enemyObject);
                controller.Setup(i);
                controller.name =  spawnId.ToString() + " Enemy Controller";
            }
        }
    }
}
