using UnityEngine;

public class TType : MonoBehaviour
{
    private void Awake()
    {
        new B();
    }

    public class A
    {
        public void Hello()
        {
            Debug.Log(GetType());
        }
    }

    public class B : A
    {
        public B()
        {
            Hello();
        }
    }
}
