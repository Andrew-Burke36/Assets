using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    int a = 1;
    int b = 150;
    int counter = 0;

    string Message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i < 11; i++)
        {
            Message += i + " "; 
        }

        Debug.Log(Message);
          
          while ( a < b )
        {
            a++;
            counter++;
        }

        Debug.Log("It took " + counter + " increments to make the numbers equal");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
