using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialBarScript : MonoBehaviour
{

    public PlayerAmmoScript ammo;
    public RectTransform trans;
    public Text displayText;
    public Vector3 homePos;
    public Vector3 currentPos;
    public Vector3 targetPos;
    public float delta;

    // Use this for initialization
    void Start()
    {
        trans = gameObject.GetComponent<RectTransform>();
        homePos = trans.localPosition;
        currentPos = homePos;
        delta = trans.rect.width / ammo.max;
        trans.localPosition = GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = trans.localPosition;
        targetPos = GetTarget();
        trans.localPosition = Vector3.MoveTowards(currentPos, targetPos, 1f);
        displayText.text = ammo.current.ToString() + " / " + ammo.max.ToString();
    }

    public Vector3 GetTarget()
    {
        Vector3 offsetPos = new Vector3((ammo.current - ammo.max) * delta, 0f, 0f);
        Vector3 target = homePos + offsetPos;
        return target;
    }

}
