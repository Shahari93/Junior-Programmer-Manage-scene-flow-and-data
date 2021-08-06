using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    private ResourcePile resourcePile = null;
    public float resourceMultiplier = 2f;

    // production speed increase during the frame when the productivity unit 
    // comes within range of a Building that is a resource pile
    
    protected override void BuildingInRange()
    {
        if(resourcePile == null)
        {
            ResourcePile pile = m_Target as ResourcePile; // sets the pile variable to m_Target only if m_Target is a ResourcePile type
            if (pile != null)
            {
                resourcePile = pile;
                resourcePile.ProductionSpeed *= resourceMultiplier;
            }
        }
    }

    // check and see if it’s currently working on a resource pile
    private void ResetProductivity()
    {
        if(resourcePile != null)
        {
            resourcePile.ProductionSpeed /= resourceMultiplier;
            resourcePile = null;
        }
    }

    // These methods will run as soon as the user selects a new location for the productivity unit
    // Before moving, it will return the production speed of the current pile back to its original rate, if a productivity pile was currently selected.
    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target); // perform what occurs in the base GoTo method
    }

    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}
