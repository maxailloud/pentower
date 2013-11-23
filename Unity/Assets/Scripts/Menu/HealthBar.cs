using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Unit))]
public class HealthBar : MonoBehaviour
{
    public int maxWidth = 40;// pixels
    public int height = 6; // pixels
    public float offsetX = -0.01f, offsetY = -0.04f;

	public Unit unit;
    static private Color lowHPColor = new Color(1f, 0, 0, 1f);
    static private Color middleHPColor = new Color(1f, 0.8f, 0, 1f);
    static private Color normalHPColor = new Color(0, 1f, 0, 1f);
    private Transform tr = null;

    // Use this for initialization
    void Start()
    {
		unit = GetComponent<Unit>();
        tr = unit.transform; // not to do it inside the main ONGUI loop
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        float ratio = unit.hitPoints / ((float)unit.maxHitPoints);

		Color c;
		if (ratio > 0.5f)
		{
			c = Color.Lerp(middleHPColor, normalHPColor, 2.0f * (ratio - 0.5f));
		}
		else
		{
			c = Color.Lerp (lowHPColor, middleHPColor, 2.0f * ratio);
		}

        Vector3 pos = Camera.main.WorldToViewportPoint(tr.position);

        // if the thing is not in the screen, do not display it
        if (pos.x < 0 || pos.y < 0 || pos.z < 0 || pos.x > Screen.height || pos.y > Screen.width)
        {
            return;
        }

        pos.x += offsetX;
        pos.y += offsetY;

        Rect r = new Rect(pos.x * Screen.width, (1f - pos.y) * Screen.height, ((int)maxWidth * ratio), height); // TODO fine adjustment of halthbar positioning
        HUD.drawRectangle(c, r);
    }
}
