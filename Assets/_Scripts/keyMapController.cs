using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class keyMapController : MonoBehaviour
{
	public Text txtHelpYellow;
	public Text txtHelpGreen;

	public Toggle toggleQWERTZ;
	public Toggle toggleQWERTY;
	public Toggle toggleAZERTY;
	public Toggle toggleNumLock;

	public Canvas mainUI;
	public GameObject rulesUI;

	enKeyboardLayout keyboardLayout;
	int useNumLock;
	bool isClosing = false;

	public void OnApplicationQuit()
	{
		isClosing = true;
	}

	enum enKeyboardLayout
	{
		QWERTZ,
		QWERTY,
		AZERTY
	}
	// Use this for initialization
	void Start()
	{
		switch ((enKeyboardLayout)PlayerPrefs.GetInt(gVar.prefKeyboard))
		{
			case enKeyboardLayout.AZERTY:
				toggleAZERTY.isOn =true;
				break;
			case enKeyboardLayout.QWERTY:
				toggleQWERTY.isOn =true;
				break;
			case enKeyboardLayout.QWERTZ:
				toggleQWERTZ.isOn =true;
				break;
			default:
				break;
		}

		switch (PlayerPrefs.GetInt(gVar.prefkeyNumLock))
		{
			case 0:
				toggleNumLock.isOn = false;
				break;
			case 1:
				toggleNumLock.isOn = true;
				break;
			default:
				break;
		}
	}

	void setLayout(enKeyboardLayout layout)
	{
		keyboardLayout = layout;
		PlayerPrefs.SetInt(gVar.prefKeyboard, (int)keyboardLayout);
		PlayerPrefs.Save();

		int curPlayer = 0;
		switch (keyboardLayout)
		{
			case enKeyboardLayout.AZERTY:
				gVar.keyAction1[curPlayer] = KeyCode.A;
				gVar.keyUp[curPlayer] = KeyCode.Z;
				gVar.keyLeft[curPlayer] = KeyCode.Q;
				gVar.keyDeposeLightBulb[curPlayer] = KeyCode.W;
				break;
			case enKeyboardLayout.QWERTY:
				gVar.keyAction1[curPlayer] = KeyCode.Q;
				gVar.keyUp[curPlayer] = KeyCode.W;
				gVar.keyLeft[curPlayer] = KeyCode.A;
				gVar.keyDeposeLightBulb[curPlayer] = KeyCode.Z;
				break;
			case enKeyboardLayout.QWERTZ:
				gVar.keyAction1[curPlayer] = KeyCode.Q;
				gVar.keyUp[curPlayer] = KeyCode.W;
				gVar.keyLeft[curPlayer] = KeyCode.A;
				gVar.keyDeposeLightBulb[curPlayer] = KeyCode.Y;
				break;
			default:
				break;
		}

		txtHelpYellow.text = "Move: " + gVar.keyLeft [curPlayer] + ", " + gVar.keyDown [curPlayer] + ", " + gVar.keyRight [curPlayer] + ", " + gVar.keyUp [curPlayer] + "\r\n";
		txtHelpYellow.text += "Release object: " + gVar.keyRelease[curPlayer] + "\r\n";
		txtHelpYellow.text += "Action: " + gVar.keyAction1[curPlayer] + "\r\n";
		txtHelpYellow.text += "Depose light bulb: " + gVar.keyDeposeLightBulb [curPlayer] + "\r\n"; 
		txtHelpYellow.text += "Speed fast/slow: " + gVar.keySpeedSlow[curPlayer];
	}

	void setLayoutNumLock(int value)
	{
		if (isClosing)
			return;
		useNumLock = value;
		PlayerPrefs.SetInt(gVar.prefkeyNumLock, useNumLock);
		PlayerPrefs.Save();

		int curPlayer = 1;
		switch (useNumLock)
		{
			case 0:
				gVar.keyAction1[curPlayer] = KeyCode.U;
				gVar.keyDeposeLightBulb[curPlayer] = KeyCode.J;
				gVar.keyRelease[curPlayer] = KeyCode.O;

				gVar.keyUp[curPlayer] = KeyCode.UpArrow;
				gVar.keyDown[curPlayer] = KeyCode.DownArrow;
				gVar.keyLeft[curPlayer] = KeyCode.LeftArrow;
				gVar.keyRight[curPlayer] = KeyCode.RightArrow;

				gVar.keySpeedSlow[curPlayer] = KeyCode.K;
				break;
			case 1:

				gVar.keyAction1[curPlayer] = KeyCode.Keypad7;
				gVar.keyDeposeLightBulb[curPlayer] = KeyCode.Keypad1;
				gVar.keyRelease[curPlayer] = KeyCode.Keypad9;
				
				gVar.keyUp[curPlayer] = KeyCode.Keypad8;
				gVar.keyDown[curPlayer] = KeyCode.Keypad5;
				gVar.keyLeft[curPlayer] = KeyCode.Keypad4;
				gVar.keyRight[curPlayer] = KeyCode.Keypad6;
				
				gVar.keySpeedSlow[curPlayer] = KeyCode.Keypad2;
				break;
			default:
				break;
		}
		
		txtHelpGreen.text = "Move: " + gVar.keyLeft [curPlayer] + ", " + gVar.keyDown [curPlayer] + "\r\n";
		txtHelpGreen.text += " " + gVar.keyRight [curPlayer] + ", " + gVar.keyUp [curPlayer] + "\r\n";
		txtHelpGreen.text += "Release object: " + gVar.keyRelease[curPlayer] + "\r\n";
		txtHelpGreen.text += "Action: " + gVar.keyAction1[curPlayer] + "\r\n";
		txtHelpGreen.text += "Depose light bulb: " + gVar.keyDeposeLightBulb [curPlayer] + "\r\n"; 
		txtHelpGreen.text += "Speed fast/slow: " + gVar.keySpeedSlow[curPlayer];
	}
	
	public void butQWERTZ(bool value)
	{
		if(value)
			setLayout(enKeyboardLayout.QWERTZ);
	}

	public void butQWERTY(bool value)
	{
		if(value)
			setLayout(enKeyboardLayout.QWERTY);
	}

	public void butAZERTY(bool value)
	{
		if(value)
			setLayout(enKeyboardLayout.AZERTY);
	}

	public void butNumLock(bool value)
	{
		if(toggleNumLock.isOn)
			setLayoutNumLock(1);
		else
			setLayoutNumLock(0);
	}

	public void showRules()
	{
		mainUI.enabled = false;
		rulesUI.SetActive(true);
	}

}
