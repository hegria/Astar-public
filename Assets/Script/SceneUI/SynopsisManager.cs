using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

public class SynopsisManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<string> dialog;
    public Image image;

    public Sprite[] sprties;

    int[] indeies = {0,1,2,4,5,6};
    int imgidx = 0;

    public TextMeshProUGUI texts;

    int strindex;
    string nowtalk;
    int lines;

    IEnumerator nextimg(){
        imgidx++;
        for (int i = 60; i >= 0; i--)
        {
            image.color = new Color(i / 60f, i / 60f, i / 60f, 1);
            yield return null;
        }
        if(imgidx-1>=sprties.Length){
            yield break;
        }
        yield return new WaitForSeconds(1.5f);
        image.sprite = sprties[imgidx - 1];
        for (int i = 0; i < 60+1;i++){
            image.color = new Color(i / 60f, i / 60f, i / 60f, 1);
            yield return null;
        }
    }

    public AudioSource audioSource;
    IEnumerator startimg(){
      
        for (int i = 0; i < 60+1;i++){
            image.color = new Color(i / 60f, i / 60f, i / 60f, 1);
            yield return null;
        }
    }
    void Start()
    {
        UIsound.uIsound.stop();
        string temp = (Resources.Load("synopsis") as TextAsset).text;
        dialog = JsonConvert.DeserializeObject<List<string>>(temp);
        lines = 0;
        strindex = 0;
        nowtalk = null;
        StartCoroutine(startimg());
        StartCoroutine(says());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void skipbtn(){
        UIsound.uIsound.Clicked();
        skip();
    }
    public void skip(){
        SceneManager.LoadScene(2);
    }
    IEnumerator says(){
        if(lines==0&&strindex == 0){
            yield return new WaitForSeconds(0.5f);
        }
        if(dialog[lines][strindex].Equals('<')){
            while(!dialog[lines][strindex].Equals('>')){
                nowtalk += dialog[lines][strindex++];
            }
            nowtalk += dialog[lines][strindex++];
        }
        nowtalk += dialog[lines][strindex];
        texts.SetText(nowtalk);
        strindex++;
        if(strindex == dialog[lines].Length){
            if(lines == indeies[imgidx]){
                yield return new WaitForSeconds(1.5f);
                if(imgidx < 5){
                    StartCoroutine(nextimg());
                    yield return new WaitForSeconds(0.8f);
                    texts.SetText("");
                }
                yield return new WaitForSeconds(1.7f);
            }else{
                yield return new WaitForSeconds(1.5f);
            }
            lines++;
            if(lines == dialog.Count){
                skip();
                yield break;
            }
            strindex = 0;
            nowtalk = null;
        }else{
            if(dialog[lines][strindex].Equals(' ')){
                
                yield return new WaitForSeconds(1f/16);//16
            }else{
            audioSource.Play();
            yield return new WaitForSeconds(1f/8);//8
            }
        }
        StartCoroutine(says());
    }
}
