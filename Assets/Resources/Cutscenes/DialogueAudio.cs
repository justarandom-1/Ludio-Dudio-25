using UnityEngine;

public class DialogueAudio : MonoBehaviour
{
    protected AudioClip clip;
    protected AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int talking = 0;



    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void setClip(AudioClip a)
    {
        clip = a;
    }

    public void EndDialogue()
    {
        talking = 0;
    }

    public void Speak()
    {
        if (clip == null)
            return;
        if (talking == 0)
            audioSource.PlayOneShot(clip, LevelManager.instance.dialogueVol);

        talking = (talking + 1) % 3;
    }
}
