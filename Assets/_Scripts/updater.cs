using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

public class updater : MonoBehaviour
{

	private bool force = false; // Change if you always wants to be 100% sure that the user is using the latest version (true)
	public string version = "";
	public string url = "";
	public Text txtVersion;
	//string nextUpdate = null;
	string updPath;

	// GUI stuff
	bool showError = false;
	bool reqUpdate = false;
	bool hasUpdate = false;

	// Use this for initialization
	void Start()
	{
#if UNITY_STANDALONE
		StartCoroutine(checkOnline(url));
#else
		updPath = "";
		txtVersion.text = "Version " + version + updPath; 
#endif
	}
	
	IEnumerator checkOnline(string url)
	{
		url += "versionInfo.xml";
		
		try
		{
			// encrypt the PW for better security;
			HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
			
			httpWReq.Method = "POST";
			httpWReq.ContentType = "application/x-www-form-urlencoded";
			
			HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
				Debug.Log("[SYSTEM] Response from WWW-patch: " + responseString);

				if (isLatestVersion(responseString))
				{
					// We have the latest version load next screen or show login...
					Debug.Log("[SYSTEM] Client is up to date...");
					//startgame;
				} else
				{
					hasUpdate = true;
					if (force)
					{
						reqUpdate = true;
					} else
					{
						//startGame();
					}
				}
	
			} else
			{
				// Most likely the url is wrong
				Debug.Log("[SYSTEM] Error while getting version info: " + response.StatusCode);
				if (force)
				{
					showError = true;
				} else
				{
					//startGame();
				}
			}
		} catch (Exception ex)
		{
			Debug.Log("[SYSTEM] Error while getting version info: " + ex.Message);
			// The WWW server is offline
			if (force)
			{
				showError = true;
			} else
			{
				// StartGame
			}
		}

		updGUI();
		yield return new WaitForSeconds(1.0f); // Wait 1 second so we dont overflow anything
		if (hasUpdate)
		{
			if(downloadUpdate())
				txtVersion.text += ", unzip " + updPath + " in the same folder";
		}
	}

	bool downloadUpdate()
	{
		string localFile = updPath;

		if (File.Exists(localFile))
		{
			return true;
		}

		string downloadLink = url + "files/" + updPath;
		WebClient wc = new WebClient();
		wc.DownloadFile(downloadLink, localFile);

		return true;
	}

	public bool isLatestVersion(string rawXML)
	{
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(rawXML);

		string thisId = null;
		string thisVersion = null;

		// check what id our current version is
		XmlNodeList xnList = xml.SelectNodes("/versions/patch");
		foreach (XmlNode xn in xnList)
		{
			string id = xn ["id"].InnerText;
			string versionName = xn ["build"].InnerText;
			if (versionName.Equals(version))
			{
				thisId = id;
				thisVersion = versionName;
				break;
			}
		}

		// if thisId or thisVersion still is null there is an error
		if (thisId == null || thisVersion == null)
		{
			return false;
		} else
		{
			Debug.Log("[SYSTEM] Current version is: " + thisVersion + " (#" + thisId + ")");
		}

		// We now need to check if there are more updates
		foreach (XmlNode xn in xnList)
		{
			string id = xn ["id"].InnerText;
			string versionName = xn ["build"].InnerText;
			if (id.Equals(Convert.ToInt32(thisId) + 1 + ""))
			{
				//nextUpdate = versionName;
				force = Convert.ToBoolean(xn ["force"].InnerText);
#if UNITY_STANDALONE_WIN
				updPath = xn["win-link"].InnerText;
#elif UNITY_STANDALONE_LINUX
				updPath = xn["linux-link"].InnerText;
#elif UNITY_STANDALONE_OSX
				updPath = xn["mac-link"].InnerText;
#endif
				Debug.Log("[SYSTEM] Next update is: " + versionName + " (#" + id + ") which is forces: " + force);
				return false;
			}
		}

		// If we made it this far, then there are no new updates
		return true;
	}

	void updGUI()
	{
		if (showError)
		{
			txtVersion.text = "Error for checking new version";
		} else
		{
			txtVersion.text = "Version: " + version.ToString() + (hasUpdate ? " Update available" : " Up to date");
		}
		if (reqUpdate)
		{

		}


	}

}
