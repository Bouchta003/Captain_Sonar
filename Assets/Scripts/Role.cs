using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : MonoBehaviour
{
    /// <summary>
    /// This property represents the description of the specific Role
    /// </summary>
    public string Description { get; protected set; }

    protected bool _actionDone;

    /// <summary>
    /// This represents whether the Role has finished their turn
    /// </summary>
    public virtual bool ActionDone {
        get { return _actionDone; } 
        protected set 
        { 
            if (_actionDone != value)
            {
                _actionDone = value;
                OnActionStatusChanged();
            }
        }
    }


    public abstract void PerformRoleAction();

    protected abstract void SetDescription();

    protected abstract void OnActionStatusChanged();
}
