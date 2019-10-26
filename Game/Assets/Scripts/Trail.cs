using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour
{
    public GameObject prefab;
    public float length = 0.5f;
    public float step = 0.25f;
    public LayerMask layer;

    GameObject[] prefabs;
    Ray2D ray = new Ray2D();
    RaycastHit2D hit = new RaycastHit2D();

    // Use this for initialization
    void Start()
    {
        prefabs = new GameObject[Mathf.FloorToInt(length / step)];

        for (int i = 0; i < prefabs.Length; i++) prefabs[i] = (GameObject)Instantiate(prefab);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition)/* - player.transform.position*/;
        transform.Rotate(0f, mousePosition.x, 0f);

        Debug.DrawLine(transform.position, Vector3.zero, Color.white, 5f, false);


    }


    void FixedUpdate()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        //Debug.Log(Physics2D.Raycast(ray.origin, ray.direction).transform.name);

        int index = 0;
        float distance = 0f;

        /*while (index < prefabs.Length)
        {
            /*RaycastHit2D[]

            Physics2D.Raycast(ray.origin, ray.direction);
            Physics2D.Raycast(ray.origin, ray.direction, FilterMode.Bilinear, results:){ }

            }*/

            /*RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit != null && hit.collider != null) {
                Vector3 inDirection = Vector3.Reflect(transform.right, hit.normal);

                index++;
            }*/

            /*if(hit != null)
            {
                distance += Vector3.Distance(ray.origin, hit.point);

                // nao faco ideia do que esse distance faz
                if (distance < step) {

                    ray.origin = hit.point;
                    ray.direction = Vector3.Reflect(ray.direction, hit.normal);

                } else {


                    Physics.Raycast(ray, out hit, length, layer);
                    prefabs[index].transform.position = ray.origin = ray.origin + ray.direction * Mathf.Abs(step - distance);
                    if (hit.point == ray.origin) ray.direction = Vector3.Reflect(ray.direction, hit.normal);
                    index++;
                    distance = 0f;

                }



            }*/

            //Debug.Log(Physics2D.Raycast(ray.origin, ray.direction).transform.name + "AASFDAFA");

            /*RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }


            if (Physics2D.Raycast(ray, out hit, step, layer))
            {
                Debug.Log(hit.transform.name + " AAAAAAAA");

                distance += Vector3.Distance(ray.origin, hit.point);

                if (distance < step)
                {

                    ray.origin = hit.point;
                    ray.direction = Vector3.Reflect(ray.direction, hit.normal);

                }
                else
                {

                    Physics.Raycast(ray, out hit, length, layer);
                    prefabs[index].transform.position = ray.origin = ray.origin + ray.direction * Mathf.Abs(step - distance);
                    if (hit.point == ray.origin) ray.direction = Vector3.Reflect(ray.direction, hit.normal);
                    index++;
                    distance = 0f;

                }

            }
            else
            {

                Physics.Raycast(ray, out hit, length, layer);
                prefabs[index].transform.position = ray.origin = ray.origin + ray.direction * Mathf.Abs(step - distance);
                if (hit.point == ray.origin) ray.direction = Vector3.Reflect(ray.direction, hit.normal);
                index++;
                distance = 0f;

            }*/

       // }

    }

}