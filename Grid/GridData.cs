using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData : MonoBehaviour
{
    //Singleton
    public static GridData Instance {get; private set;}
    //Fields
    [SerializeField] private TextAsset textAsset;

    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("GridData Singleton already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
    }
    public int[,] ReadMap(int width,int height)
    {
        List<string> lines=GetTextFromFile();
        SetDimensions(lines,width,height);
        int[,] mapData= new int[width,height];
        for(int y=0;y<height;y++)
        {
            for(int x=0;x<width;x++)
            {
                if(lines[y].Length>x)
                {
                    mapData[x,y]=(int)Char.GetNumericValue(lines[y][x]);
                }
            }
        }
        return mapData;

    }
    private void SetDimensions(List<string> textLines,int width,int height)
    {
        height=textLines.Count;
        foreach(string line in textLines)
        {
            if(line.Length>width)
            {
                width=line.Length;
            }
        }
    }

    private List<string> GetTextFromFile(TextAsset textAsset)
    {
        List<string> lines = new List<string>();
        if(textAsset!=null)
        {
            string textData = textAsset.text;
            string[] delimeters= {"\r\n","\n"};
            lines.AddRange(textData.Split(delimeters,System.StringSplitOptions.None));
            lines.Reverse();

        }
        else
        {
            Debug.LogWarning("MAPDATA GetTextFromFile Error: InvalidTextAsset");
        }
        return lines;
    }

    private List<string> GetTextFromFile()
    {
        return GetTextFromFile(textAsset);
    }
}
