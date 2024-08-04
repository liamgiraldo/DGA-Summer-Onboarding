using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject spike1;
    [SerializeField] GameObject spike2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spike1.active && !spike2.active)
            {
                spike1.SetActive(false);
                spike2.SetActive(true);
            }
            else if (!spike1.active && spike2.active)
            {
                spike1.SetActive(true);
                spike2.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
