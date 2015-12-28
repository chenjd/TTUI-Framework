using UnityEngine;
using System.Collections;

public class TTUITestFrame : TTUIFrame {

    public TTUITestFrame (int id)
        : base(id)
    {
    }

    public override void Active()
    {
        base.Active();
    }

    public override void DeActive(bool bForce = false)
    {
        base.DeActive(bForce);
    }

	public override void RegisterHandlers(TTUIMsgManager mgr)
    {
        base.RegisterHandlers(mgr);
		mgr.RegisterHandler(TTUIHelper.StringHash("TestClose"), new TTUIMsgManager.TTUIMsgHandler(this.Close));
		mgr.RegisterHandler(TTUIHelper.StringHash("TestCallScript"), new TTUIMsgManager.TTUIMsgHandler(this.CallScriptFunc));
    }

	public override void UnRegisterHandlers(TTUIMsgManager mgr)
    {
		mgr.UnRegisterHandler(TTUIHelper.StringHash("TestClose"), new TTUIMsgManager.TTUIMsgHandler(this.Close));
		mgr.UnRegisterHandler(TTUIHelper.StringHash("TestCallScript"), new TTUIMsgManager.TTUIMsgHandler(this.CallScriptFunc));
        base.UnRegisterHandlers(mgr);
    }

	public void CallScriptFunc(TTUIMessage msg)
    {
		base.CallMBFunction("ChangeText", "消息的ID为：" + msg.ID);
    }

	public void Close(TTUIMessage msg)
    {
		Debug.LogError("click");
		TTUICore.FrameMgr.DeActive(TTUIFrameID.InitTestFrame);
    }
}
