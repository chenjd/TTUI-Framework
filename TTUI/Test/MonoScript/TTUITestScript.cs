using UnityEngine;
using System.Collections;

public class TTUITestScript : MonoBehaviour {
	public UILabel label;

	public void ChangeText(string input)
	{
		this.label.text = input;
	}

}
