using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InputValidator : MonoBehaviour
{
    LoginPanel loginPanel;
    SignUpPanel signUpPanel;

    public static bool ValidateTermsToggle(Toggle termsToggle, Text errorText)
    {
        if(termsToggle.isOn)
        {
            return true;
        }
        errorText.text = "You must agree to the terms";
        return false;
    }

    public static bool ValidateEmail(string email, Text errorText)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            errorText.text = "Email field should not be empty";
            return false;
        }

        string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Match result = Regex.Match(email, pattern);
        Regex hasMiniMaxChars = new Regex(@".{6,50}");
        if (!result.Success)
        {
            errorText.text = "Please enter a valid email address";
            return false;
        }

        if (!hasMiniMaxChars.IsMatch(email))
        {
            errorText.text = "Email address should have between 6 and 50 characters";
            return false;
        }
        return true;
    }

    public static bool ValidatePassword(string password, Text errorText)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            errorText.text = "Password field should not be empty";
            return false;
        }

        Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasUpperChar = new Regex(@"[A-Z]+");
        Regex hasMiniMaxChars = new Regex(@".{6,30}");
        Regex hasLowerChar = new Regex(@"[a-z]+");
        Regex hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(password))
        {
            errorText.text = "Password should contain at least one lower case letter";
            return false;
        }
        if (!hasUpperChar.IsMatch(password))
        {
            errorText.text = "Password should contain at least one upper case letter";
            return false;
        }
        if (!hasNumber.IsMatch(password))
        {
            errorText.text = "Password should contain at least one numeric value";
            return false;
        }

        if (!hasSymbols.IsMatch(password))
        {
            errorText.text = "Password should contain at least one special character";
            return false;
        }
        if (!hasMiniMaxChars.IsMatch(password))
        {
            errorText.text = "Passwords should have between 6 and 30 characters";
            return false;
        }
        return true;
    }

    public static bool ValidateRePassword(string password, string rePassword, Text errorText)
    {
        if (password != rePassword)
        {
            errorText.text = "Passwords must match";
            return false;
        }
        return true;
    }

    public static bool ValidateUsername(string username, Text errorText)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            errorText.text = "Username field should not be empty";
            return false;
        }

        string pattern = @"^([a-zA-Z0-9_\-])*$";
        Match result = Regex.Match(username, pattern);
        Regex hasMiniMaxChars = new Regex(@".{6,30}");
        if (!result.Success)
        {
            errorText.text = "Username can not contain special characters";
            return false;
        }

        if (!hasMiniMaxChars.IsMatch(username))
        {
            errorText.text = "Username should have between 6 and 30 characters";
            return false;
        }
        return true;
    }

    void SwitchToSignup()
    {
        loginPanel.ClosePanel();
        signUpPanel.gameObject.SetActive(true);
        signUpPanel.InitPanel();
    }

    void SwitchToLogin()
    {
        signUpPanel.ClosePanel();
        loginPanel.gameObject.SetActive(true);
        loginPanel.InitPanel();
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        loginPanel = GameObject.Find("LoginPanel").GetComponent<LoginPanel>();
        signUpPanel = GameObject.Find("SignUpPanel").GetComponent<SignUpPanel>();

        loginPanel.InitPanel();
        signUpPanel.gameObject.SetActive(false);

        LoginPanel.SwitchToSignupClicked += SwitchToSignup;
        SignUpPanel.SwitchToLoginClicked += SwitchToLogin;
    }
}
