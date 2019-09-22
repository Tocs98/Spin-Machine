using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    Button SpinButton;

    //COMBO WINS
    List<int> campanas = new List<int>();
    List<int> berenjenas = new List<int>();
    List<int> cerezas = new List<int>();
    List<int> sandias = new List<int>();
    List<int> naranjas = new List<int>();
    List<int> uvas = new List<int>();
    List<int> limon = new List<int>();


    bool isChecking; 


    private void Start()
    {
        campanas.Add(0); // 0 or 1 ITEM
        campanas.Add(50); //2 ITEMS
        campanas.Add(75); //3 ITEMS
        campanas.Add(100); //4 ITEMS OR MORE
        
        berenjenas.Add(0);
        berenjenas.Add(10);
        berenjenas.Add(20);
        berenjenas.Add(40);
        
        cerezas.Add(0);
        cerezas.Add(2);
        cerezas.Add(5);
        cerezas.Add(10);
        
        sandias.Add(0);
        sandias.Add(20);
        sandias.Add(30);
        sandias.Add(60);
        
        naranjas.Add(0);
        naranjas.Add(10);
        naranjas.Add(15);
        naranjas.Add(30);
        
        uvas.Add(0);
        uvas.Add(10);
        uvas.Add(20);
        uvas.Add(40);
        
        limon.Add(0);
        limon.Add(5);
        limon.Add(10);
        limon.Add(20);


        SpinButton = GameObject.FindGameObjectWithTag("ButtonSpin").GetComponent<Button>();   
    }

    public void StartSpin()
    {
        SpinButton.interactable = false;
        foreach (Transform child in transform)
        {
            child.GetComponent<ColumnManager>().StartSpin();
        }
    }

    public void ReEnableSpin()
    {
        SpinButton.interactable = true;
    }

    public void GetTotalPrizes()
    { //CHILDS 2, 3 AND 4 are the ones who can have a price
        //first row
        if (isChecking) return;
        else isChecking = true;

        Debug.ClearDeveloperConsole();

        GameObject Ypos = GameObject.FindGameObjectWithTag("YPos").gameObject;
        GameObject number1 = Ypos.transform.GetChild(2).gameObject; //this is the lowest one
        GameObject number2 = Ypos.transform.GetChild(3).gameObject;
        GameObject number3 = Ypos.transform.GetChild(4).gameObject; //this is the highest one

        //GETTING AL THE NAMES OF THE SPRITES THAT ARE IN THE SELECTED POSITION
        List<string> column1 = transform.GetChild(0).GetComponent<ColumnManager>().ReturnPositionPrizes();
        List<string> column2 = transform.GetChild(1).GetComponent<ColumnManager>().ReturnPositionPrizes();
        List<string> column3 = transform.GetChild(2).GetComponent<ColumnManager>().ReturnPositionPrizes();
        List<string> column4 = transform.GetChild(3).GetComponent<ColumnManager>().ReturnPositionPrizes();
        List<string> column5 = transform.GetChild(4).GetComponent<ColumnManager>().ReturnPositionPrizes();

        //FIRST, CHECK HORIZONTAL

        //check first line, the lowest one
        List<string> resultLowestLine = new List<string>();
        resultLowestLine.Add(column1[0]); 
        resultLowestLine.Add(column2[0]);
        resultLowestLine.Add(column3[0]);
        resultLowestLine.Add(column4[0]);
        resultLowestLine.Add(column5[0]);
        Debug.LogError("----HORIZONTAL----");
        Debug.LogError("--LOWEST LINE RESULT--");
        CheckItems(resultLowestLine);

        List<string> resultMiddleLine = new List<string>();
        resultMiddleLine.Add(column1[1]);
        resultMiddleLine.Add(column2[1]);
        resultMiddleLine.Add(column3[1]);
        resultMiddleLine.Add(column4[1]);
        resultMiddleLine.Add(column5[1]);

        Debug.LogError("--MIDDLE LINE RESULT--");
        CheckItems(resultMiddleLine);

        List<string> resultHighestLine = new List<string>();
        resultHighestLine.Add(column1[2]);
        resultHighestLine.Add(column2[2]);
        resultHighestLine.Add(column3[2]);
        resultHighestLine.Add(column4[2]);
        resultHighestLine.Add(column5[2]);

        Debug.LogError("--HIGHEST LINE RESULT--");
        CheckItems(resultHighestLine);


        Debug.LogError("----VERTICAL COLUMNS----");
        Debug.LogError("--NUMBER 1--");
        CheckItems(column1);
        Debug.LogError("--NUMBER 2--");
        CheckItems(column2);
        Debug.LogError("--NUMBER 3--");
        CheckItems(column3);
        Debug.LogError("--NUMBER 4--");
        CheckItems(column4);
        Debug.LogError("--NUMBER 5--");
        CheckItems(column5);
        
        ReEnableSpin();
        Invoke("SetActiveResult", 1);
    }

    private void CheckItems(List<string> items)
    {
        int c = 0;
        int b = 0;
        int ce = 0;
        int s = 0;
        int n = 0;
        int u = 0;
        int l = 0;
        for(int i = 0; i < items.Count; i++)
        {
            string item = items[i];

            if (i == 0 || items[i - 1] != item) continue;

            switch (item)
            {
                case "1": //campana
                    c++;
                    if (c > 4) c = 4;
                    break;
                case "4": //berenjena
                    b++;
                    if (b > 4) b = 4;
                    break;
                case "7": //cerezas
                    ce++;
                    if (ce > 4) ce = 4;
                    break;
                case "2": //sandia
                    s++;
                    if (s > 4) s = 4;
                    break;
                case "5": //naranja
                    n++;
                    if (n > 4) n = 4;
                    break;
                case "3": //uva
                    u++;
                    if (u > 4) u = 4;
                    break;
                case "6": //limon
                    l++;
                    if (l > 4) l = 4;
                    break;
            }
        }

        if(c > 0)
        {
            Debug.LogError(campanas[c]);
        }
        if (b > 0)
        {
            Debug.LogError(berenjenas[b]);
        }
        if (ce > 0)
        {
            Debug.LogError(cerezas[ce]);
        }
        if (s > 0)
        {
            Debug.LogError(sandias[s]);
        }
        if (n > 0)
        {
            Debug.LogError(naranjas[n]);
        }
        if (u > 0)
        {
            Debug.LogError(uvas[u]);
        }
        if (l > 0)
        {
            Debug.LogError(limon[l]);
        }
    }

    private void SetActiveResult()
    {
        isChecking = false;
    }
}
