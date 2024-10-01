using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHPBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    private TargetDummy playerEntity;
    private float originalScale = 1.0f;
    void Start()
    {
        playerEntity = player.GetComponent<TargetDummy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            ((float)playerEntity.Health / (float)playerEntity.MaxHealth) * originalScale,
            transform.localScale.y,
            transform.localScale.z);
    }
}
