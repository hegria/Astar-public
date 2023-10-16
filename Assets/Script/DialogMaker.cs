using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DialogMaker : MonoBehaviour
{
    public List<List<DialogData>> wholetalk;
    void make(int i){
        string a = JsonConvert.SerializeObject(wholetalk);
        File.WriteAllText(Application.persistentDataPath+"/Resource/stage"+i.ToString()+".json",a);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        wholetalk = new List<List<DialogData>>();
        List<DialogData> stage1_start = new List<DialogData>();
        stage1_start.Add(new DialogData(0,"행성 주위가 온통 물이라니 다음 휴가 때 와야겠어!",0));
        stage1_start.Add(new DialogData(1,"매일이 휴가인 프리렌서가 할 말은 아니지만요",0));
        wholetalk.Add(stage1_start);

        List<DialogData> stage2_start = new List<DialogData>();
        
        stage2_start.Add(new DialogData(0,"행성 주위가 온통 물이라니 다음 휴가 때 와야겠어!",0));
        stage2_start.Add(new DialogData(1,"매일이 휴가인 프리렌서가 할 말은 아니지만요",0));
        wholetalk.Add(stage2_start);
        make(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
