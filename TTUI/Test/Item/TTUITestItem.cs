using UnityEngine;
using System.Collections;

public class TTUITestItem : MonoBehaviour {

	void OnClick()
	{
		TTUITestMsg msg = new TTUITestMsg("TestClose");
		TTUICore.MsgMgr.SendMsg(msg);
	}
}
