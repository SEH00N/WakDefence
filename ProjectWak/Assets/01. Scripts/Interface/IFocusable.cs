using UnityEngine;

public interface IFocusable
{
	public GameObject CurrentObject { get; }

    public void OnSelected();
    public void OnInteracted();

    public void OnFocusBegin();
    public void OnFocusEnd();
}
