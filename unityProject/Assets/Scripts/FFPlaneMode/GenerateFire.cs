using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFire : MonoBehaviour
{
    private GameObject firePrefab;

    private Vector3 center;
    private Vector3 size;

    private int nbMinOfFire = 4;
    private int nbMaxOfFire = 8;
    private bool chunckIsLoad = false;

    private List<GameObject> fireList = new List<GameObject>();

    void Start()
    {
        firePrefab = Resources.Load("Fire") as GameObject;
        center = transform.position;
        size = transform.localScale;

        ChangeWaypoint();
    }

    private void FixedUpdate()
    {
        /*
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

            bool grounded = Physics.Raycast(ray, out hit);
            if (grounded && hit.transform.tag.Equals("Ground"))
            {
              Debug.Log("touch");
              //FireSpawn();
              if (!chunckIsLoad) chunckIsLoad = true;
            }
            else if (chunckIsLoad)
            {
              chunckIsLoad = false;
              FireUnspawn();
            }
        */
    }

    public void FireSpawn()
    {
        int nbOfFire = Random.Range(nbMinOfFire, nbMaxOfFire);
        for (int i = 0; i < nbOfFire; ++i)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.z / 2, -size.z / 2));
            GameObject fireCreated = Instantiate(firePrefab, pos, Quaternion.identity);
            fireList.Add(fireCreated);
        }
    }

    public void ChangeWaypoint()
    {
        int nbOfFireDisabled = 0;
        foreach (GameObject fire in fireList)
        {
            if (!fire.activeSelf)
                nbOfFireDisabled++;
        }

        if (nbOfFireDisabled == fireList.Count)
        {
            Debug.Log("All the fire are desactived");

            // Generate a new vector2 position between 1000 and 4000
            Vector3 newPos = new Vector3(Random.Range(1000, 4000), 0.0f, Random.Range(1000, 4000));
            Debug.Log(newPos);
            float altitude = GetAltitude(new Vector2(transform.position.x, transform.position.z));
            Debug.Log(altitude);
        }

    }

  public float GetAltitude(Vector2 pos)
  {
    // Raycast test
    RaycastHit hit;
    if (Physics.Raycast(new Vector3(pos.x, 10000, pos.y), Vector3.down, out hit, 50000))
    {
      float raycastDistance = hit.distance;
      raycastDistance = 10000 - raycastDistance;
      // Debug raycast result and mathematical result
      Debug.Log("Raycast hit at " + hit.point + " with height " + raycastDistance);
      return raycastDistance;
    }

    Debug.LogError("Raycast missed");
    return 0.0f;
  }
}
