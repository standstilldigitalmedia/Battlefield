using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void ok()
    {

    }

    private void Start()
    {
        string[] questionArray = { "<sprite=0><sprite=6>0  <sprite=0><sprite=7>0  <sprite=0><sprite=8>0     <sprite=0><sprite=9>0   <sprite=0><sprite=10>0   \nIn order to begin your long study of the gentle Daglaresh, you will need to obtain fragments of their DNA and encode the fragments into Ahroi, or “<i>Cipher Runes</i>.” Unfortunately, this process will cost the Daglaresh its life. But their lives will not be given in vain. By arranging these Ahori in a specific pattern, true knowledge of the Daglaresh can be unlocked.\n\nOnce true knowledge has been unlocked, you may continue your long study by encoding DNA into Onar, or “<i>Sacred Runes</i>.” The Onar you collect can be used to breathe new life into beings called mainsprings. By arranging Onar in different patterns, mainsprings can be created with enhanced attributes.\n\nMainsprings are created in vessels called aurelia. By providing the aurelia with Onar, you are essentially programming the mainspring's DNA. However, you haven't unlocked true knowledge yet and certainly don't have any Onar. So, you will need to use the aurelia without Onar in order to create a base mainspring. You will then use these base mainsprings to collect your very first Ahroi.\n\n<sprite=0><sprite=1><b>Open your collection</b>" };
        GameObject modalPanel = Instantiate(Resources.Load("QuestPanel"), GameObject.Find("Canvas").transform) as GameObject;
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails { sentences = questionArray, title = "YOUR FIRST MAINSPRING" };            
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Ok", action = modalPanel.GetComponent<ModalPanel>().DisplayNextSentence };        
        modalPanel.GetComponent<ModalPanel>().NewChoice(modalPanelDetails);
    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
