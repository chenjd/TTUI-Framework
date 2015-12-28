using UnityEngine;
using System.Collections;

public class TTUITestMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TTUICore.Create();

		//
		TTUITestFactory uifact = new TTUITestFactory();
		uifact.BuildCommonUIFrame();
		uifact.RegisterUIFrameAssetes();

	}
	
	// Update is called once per frame
	void Update () {
		TTUICore.Update();	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(500, 0, 200, 50), "test"))
		{
			TTUICore.FrameMgr.Active(TTUIFrameID.InitTestFrame);
		}
	}
}
