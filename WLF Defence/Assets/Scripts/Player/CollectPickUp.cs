using UnityEngine;
using UnityEngine.UI;

public class CollectPickUp : MonoBehaviour
{
    public Text MyText;
    private int _total;
    private int _counter;

    void Start()
    {
        _total = GameObject.FindGameObjectsWithTag("PickUp").Length;
        MyText.text = string.Format("{0} of {1}", _counter, _total);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            _counter++;
            MyText.text = string.Format("{0} of {1}", _counter, _total);
            other.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").SendMessage("ReceiveXP", 50);
            if (_counter == _total)
            {
                MyText.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("GameController").SendMessage("StopSpawn");
                gameObject.SendMessage("ReceiveXP", 2000);
            }
        }    
    }
}

