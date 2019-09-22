using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    public float waitTime = 0;
    public float stopTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.GetChild(0).gameObject != gameObject)
        {
           waitTime += Random.Range(0.2f, .4f);
        }
        else
        {
            stopTime = Random.Range(2f, 4f);
            foreach(Transform child in transform.parent)
            {
                child.GetComponent<ColumnManager>().stopTime = stopTime;
            }
        }
        
        SetInPosition(); 
    }
    private void SetInPosition()
    {
        GameObject YPositions = GameObject.FindGameObjectWithTag("YPos");
        int index = 1;
        foreach(Transform figure in transform)
        {
            float y = YPositions.transform.GetChild(index).transform.position.y;
            figure.transform.localPosition = new Vector3(0, y + 1.45f, 1);
            index++;
        }
    }
    public void StartSpin()
    {
        StartCoroutine(StartSpinCoroutine());
    }
    IEnumerator StartSpinCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        foreach(Transform child in transform)
        {
            child.GetComponent<FiguresLogic>().StartLerp();
        }
        yield return new WaitForSeconds(stopTime);

        foreach (Transform child in transform)
        {
            child.GetComponent<FiguresLogic>().ForcePositionToStop();
        }

        yield return new WaitForSeconds(2);

        GetComponentInParent<Manager>().GetTotalPrizes();
    }

    public List<string> ReturnPositionPrizes()
    {
        GameObject Ypos = GameObject.FindGameObjectWithTag("YPos");
        GameObject number1 = Ypos.transform.GetChild(2).gameObject; //this is the highest one
        GameObject number2 = Ypos.transform.GetChild(3).gameObject; //this is the highest one
        GameObject number3 = Ypos.transform.GetChild(4).gameObject; //this is the highest one

        List<string> correctOnes = new List<string>();
        correctOnes.Insert(0,""); //INITIALIZING TO PREVENT ERRORS
        correctOnes.Insert(0,"");
        correctOnes.Insert(0,"");

        foreach (Transform child in transform)
        {
            float distance = Vector3.Distance(child.position, new Vector3(child.position.x, number1.transform.position.y, 1));
            if(distance <= 1)
            {
                correctOnes[0] = child.GetComponent<SpriteRenderer>().sprite.name;
                continue;
            }
            distance = Vector3.Distance(child.position, new Vector3(child.position.x, number2.transform.position.y, 1));
            if (distance <= 1)
            {
                correctOnes[1] = child.GetComponent<SpriteRenderer>().sprite.name;
                continue;
            }
            distance = Vector3.Distance(child.position, new Vector3(child.position.x, number3.transform.position.y, 1));
            if (distance <= 1)
            {
                correctOnes[2] = child.GetComponent<SpriteRenderer>().sprite.name;
                continue;
            }
        }
        return correctOnes;
    }
}
