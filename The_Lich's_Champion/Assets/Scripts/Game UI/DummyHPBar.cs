using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyHPBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject fillArea;
    private Entity playerEntity;

    private int previousHP;
    private int currentHP;
    private int previousMaxHP;
    private int currentMaxHP;

    void Start()
    {
        playerEntity = player.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        // grab hp stats from player entity
        currentHP = playerEntity.Health;
        currentMaxHP = playerEntity.MaxHealth;

        // update the slider value if player hp has changed
        if (currentHP != previousHP || currentMaxHP != previousMaxHP)
        {
            slider.value = (float)playerEntity.Health / (float)playerEntity.MaxHealth;
            if (slider.value <= 0)
            {
                fillArea.gameObject.SetActive(false);
            }
        }
        
        // hold old hp values
        previousHP = currentHP;
        previousMaxHP = currentMaxHP;
    }
}
