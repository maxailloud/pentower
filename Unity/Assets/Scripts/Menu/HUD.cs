using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class HUD : MonoBehaviour
{
	private Player m_player;

	void Awake()
	{
		this.m_player = GetComponent<Player>();
	}

	void OnGUI()
	{
		GUI.skin = GameSingleton.Instance.config.defaultSkin;

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUI.color = Color.red;
		GUILayout.Label(string.Format("Gold: {0} Income par cycle: {1}", m_player.tower.gold, m_player.tower.incomePerCycle));
		GUI.color = Color.white;
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}
	
	static public void drawRectangle(Color color, Rect r) {
		Color backup = GUI.color;
		GUI.color = color;
		var unicolor = new Texture2D(1, 1);
		unicolor.SetPixel(0,0, color);
		unicolor.wrapMode = TextureWrapMode.Repeat;
		unicolor.Apply();
		GUI.DrawTexture(r, unicolor);
		GUI.color = backup;
	}
}

