using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    const float PROCEED_CD = 1f;

    Animator anim;
    Text dialogueText;

    bool proceedOnCD = false;
    string[,] dialogueLines = new string[2, 4];

    int currentDialogue = 0;
    int currentLine = 0;

    bool introPlay = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        Time.timeScale = 0.0f;

        dialogueLines[0, 0] = "Halt criminals. Surrender and no harm will come to you.";
        dialogueLines[0, 1] = "You'll never take us alive, Coppers!";
        dialogueLines[0, 2] = "Let's get'em!";
        dialogueLines[0, 3] = "I'm sorry, but you must perish now.";

        dialogueLines[1, 0] = "I told ya Coppers, you couldn't take us alive...";
        dialogueLines[1, 1] = "They got me!";
        dialogueLines[1, 2] = "[SWEET-DEE and RAY-JAY succumb to there wounds and die.]";
        dialogueLines[1, 3] = "You were a danger to the citizens and yourselves. Forgive us.";
    }
	
	// Update is called once per frame
	void Update () {
        if (!proceedOnCD)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(currentLine >= 4)
                {
                    Time.timeScale = 1.0f;
                    this.enabled = false;
                    return;
                }
                proceedOnCD = true;

                if (introPlay)
                    IntroDialogue();

                Invoke("EndProceedCD", PROCEED_CD);

                currentLine++;
            }
        }
	}

    void EndProceedCD()
    {
        proceedOnCD = false;
    }

    void IntroDialogue()
    {

        Debug.Log(currentLine);
        switch (currentLine)
        {
            case 0:
                Debug.Log("testing");
                anim.SetTrigger("cop");
                //dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
            case 1:
                anim.SetTrigger("sweet");
                //dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
            case 2:
                anim.SetTrigger("ray");
                //dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
            case 3:
                anim.SetTrigger("cop");
                //dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
        }
    }

    void DeathDialogue()
    {
        switch (currentLine)
        {
            case 0:
                anim.SetTrigger("sweet");
                dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
            case 1:
                anim.SetTrigger("ray");
                dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
            case 2:
                anim.SetTrigger("ray");
                dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
            case 3:
                anim.SetTrigger("cop");
                dialogueText.text = dialogueLines[currentDialogue, currentLine];
                break;
        }
    }

    public void PlayDeathDialogue()
    {
        currentDialogue = 1;
        proceedOnCD = false;
        this.enabled = true;
    }


}
