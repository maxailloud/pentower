using UnityEngine;
using System.Collections;
using System.Linq;

public class HUD : MonoBehaviour
{
	#region HUD elements
	public ButtonScript[] lanes;
	public ButtonScript[] units;
	public TextMesh creditsText;
	public TextMesh incomeText;
	#endregion // HUD elements

	private Player m_player;
	
	void OnDestroy()
	{
		this.lanes = null;
		this.units = null;
		this.creditsText = null;
		this.incomeText = null;
	}

	void Start()
	{
		this.m_player = GameSingleton.Instance.context.currentPlayer;
	}

	void Update()
	{
		HUD_Buttons();
	}

	void LateUpdate()
	{
		// Sanity check
		if (this.m_player == null || this.m_player.tower == null)
		{
			return;
		}

		if (this.creditsText != null)
		{
			this.creditsText.text = string.Format("{0:F0} credits", this.m_player.tower.gold);
		}
		
		if (this.incomeText != null)
		{
			this.incomeText.text = string.Format("{0:F0}", this.m_player.tower.incomePerCycle);
		}
	}

	void HUD_Buttons()
	{		
		// Construct a ray from the current mouse coordinates
		var ray = this.camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		ButtonScript hitButton = null;
		if (Physics.Raycast (ray, out hit))
		{
			// Lanes
			hitButton = this.lanes.FirstOrDefault( (mc) => mc.collider == hit.collider);

			if (hitButton != null)
			{
				// Click
				if (Input.GetButtonDown ("Fire1"))
				{
					hitButton.SetToggled();
					// TODO
				}
				// Mouseover
				else
				{
					if (!hitButton.toggled)
						hitButton.SetMouseOver();
				}
			}
			// Units
			else
			{
				hitButton = this.units.FirstOrDefault( (mc) => mc.collider == hit.collider);
				if (hitButton != null)
				{
					// Click
					if (Input.GetButtonDown ("Fire1"))
					{
						// TODO
					}
					// Mouseover
					else
					{
							hitButton.SetMouseOver();
					}
				}
			}
		}
		
		// Reset other lane buttons
		foreach (var b in this.lanes)
		{
			if (b != hitButton)
			{
				if (hitButton != null && hitButton.toggled)
				{
					b.toggled = false;
					b.SetDefault();
				}
				else
				{
					if (!b.toggled)
					{
						b.SetDefault();
					}
				}
			}
		}
		// Reset other unit button
		foreach (var b in this.units)
		{
			if (b != hitButton)
			{
				b.SetDefault();
			}
		}
	}

//	void OnGUI()
//	{
//		GUI.skin = GameSingleton.Instance.config.defaultSkin;
//
//		GUILayout.BeginHorizontal();
//		GUILayout.FlexibleSpace();
//		GUI.color = Color.red;
//		GUILayout.Label(string.Format("Gold: {0} Income par cycle: {1}", m_player.tower.gold, m_player.tower.incomePerCycle));
//		GUI.color = Color.white;
//		GUILayout.FlexibleSpace();
//		GUILayout.EndHorizontal();
//	}
	
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

