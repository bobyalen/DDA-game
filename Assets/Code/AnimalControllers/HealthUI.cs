using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{

    [SerializeField]
    Slider HealthBar;
    [SerializeField]
    Camera camera;
    [SerializeField] Transform healthTransform;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update

    private void Start()
    {
        camera = GameObject.Find("Player").GetComponent<Camera>();
    }

    public void updateBar(float currentHP, float maxHP)
    {
        HealthBar.value = currentHP / maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = healthTransform.position + offset;
    }
}
