using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCharacters()
    {
        ActionText.instance.startCharacters();
    }

    public void startDialogue()
    {
        ActionText.instance.startDialogue();
    }

    public void endCutscene()
    {
        ActionText.instance.unFreeze();
    }
}
