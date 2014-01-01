using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DoSth());
    }

    IEnumerator DoSth()
    {
        AudioSource[] audioSourceArray = this.gameObject.GetComponents<AudioSource>();
        audioSourceArray[1].Play();

        yield return new WaitForSeconds(3);
        Debug.Log(audioSourceArray[0].volume);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
