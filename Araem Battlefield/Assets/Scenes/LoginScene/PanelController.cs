using System.Text.RegularExpressions;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    LoginPanel loginPanel;
    SignUpPanel signUpPanel;

    

    /*public void ActivateLoginPanel()
    {
        if(signUpPanel.isActiveAndEnabled)
        {
            signUpPanel.ClosePanel();
            signUpPanel.gameObject.SetActive(false);
        }
        
        if(!loginPanel.isActiveAndEnabled)
        {
            loginPanel.gameObject.SetActive(true);
            loginPanel.InitPanel();
        }        
    }

    public void ActivateSignUpPanel()
    {
        if(loginPanel.isActiveAndEnabled)
        {
            loginPanel.ClosePanel();
            loginPanel.gameObject.SetActive(false);
        }
        
        if(!signUpPanel.isActiveAndEnabled)
        {
            signUpPanel.gameObject.SetActive(true);
            signUpPanel.InitPanel();
        }        
    }

    private void Start()
    {
        signUpPanel = GameObject.Find("SignUpPanel").GetComponent<SignUpPanel>();
        loginPanel = GameObject.Find("LoginPanel").GetComponent<LoginPanel>();
        signUpPanel.gameObject.SetActive(false);
        loginPanel.InitPanel();
        //ActivateLoginPanel();
    }*/
}
