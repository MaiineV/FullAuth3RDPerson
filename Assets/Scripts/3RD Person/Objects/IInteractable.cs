using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public void StartInteractable(Player player);
    public void StopInteractable();
}
