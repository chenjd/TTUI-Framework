using UnityEngine;
using System.Collections;

public class TTUITestItem1 : MonoBehaviour {

	void OnClick()
	{
		TTUITestMsg msg = new TTUITestMsg("TestCallScript");
		TTUICore.MsgMgr.SendMsg(msg);
	}
}
