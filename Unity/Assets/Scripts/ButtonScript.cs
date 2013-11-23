using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
	public Texture defaultTexture;
	public Texture mouseOverTexture;
	public Texture toggledTexture;

	public bool isToggle;

	public bool toggled;

	public int buttonId;

	void Awake()
	{
		if (isToggle && toggled)
		{
			this.renderer.material.mainTexture = this.toggledTexture;
		}
		else
		{
			this.renderer.material.mainTexture = this.defaultTexture;
		}
	}

	public void SetDefault()
	{
		if (this.defaultTexture != null)
			this.renderer.material.mainTexture = this.defaultTexture;

		Color c = this.renderer.material.color;
		c.a = 0.7f;
		this.renderer.material.color = c;
	}

	public void SetMouseOver()
	{
		if (this.mouseOverTexture != null)
			this.renderer.material.mainTexture = this.mouseOverTexture;
		
		Color c = this.renderer.material.color;
		c.a = 1f;
		this.renderer.material.color = c;
	}

	public void SetToggled()
	{
		toggled = true;
		
		if (this.toggledTexture != null)
			this.renderer.material.mainTexture = this.toggledTexture;
		
		Color c = this.renderer.material.color;
		c.a = 0.8f;
		this.renderer.material.color = c;
	}
}
