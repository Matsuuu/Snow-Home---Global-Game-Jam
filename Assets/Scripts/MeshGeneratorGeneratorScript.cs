using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorGeneratorScript : MonoBehaviour
{
    public GameObject meshGenerator;
    public GameObject snowBall;

    public int snowWidth;

    public int snowDepth;
    public int widthGeneratorCount;
    public int depthGeneratorCount;
    
    // Start is called before the first frame update
    void Start()
    {
        Quaternion zeroAngle = Quaternion.Euler(Vector3.zero);
        Vector3 generatorPos = transform.position;
        for (int x = 0; x <= widthGeneratorCount; x++)
        {
            for (int z = 0; z <= depthGeneratorCount; z++)
            {
                GameObject generator = Instantiate(meshGenerator,
                    new Vector3(generatorPos.x + x * snowWidth - x , 0, generatorPos.z + z * snowDepth - z), zeroAngle, transform.parent);
                generator.GetComponent<MeshGenerationScript>().snowBall = snowBall;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
