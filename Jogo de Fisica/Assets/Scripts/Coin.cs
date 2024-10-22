using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform stackedOn;
    [SerializeField] private Transform stackPivot;
    [SerializeField] float dropForce = 5f;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.AddForce(Vector3.up * dropForce + Vector3.right * .2f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && stackedOn == null)
        {
            CoinStack coinStack = other.gameObject.GetComponent<CoinStack>();
            coinStack.Push(this);
        }
    }

    private void GoToPosition()
    {
        transform.position = StackedOn.position;
    }

    public void Stack(Transform pivot)
    {
        stackedOn = pivot;
        transform.SetParent(stackedOn);
        GoToPosition();

        rigidbody.isKinematic = true;
    }

    public void Collect()
    {
        GameManagement.instance.ChangeMoney(1);

        Debug.Log("coin collected");

        Destroy(gameObject);
    }

    public Transform StackedOn => stackedOn;
    public Transform StackPivot => stackPivot;
}
