using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : Role
{
    #region To override

    public override void PerformRoleAction()
    {
        ActionDone = false;
        Debug.Log($"{Name} role started\n" + Description);
    }

    protected override void OnActionStatusChanged()
    {
        ToggleUI();
    }

    protected override void SetDescription()
    {
        Name = "Engineer";
        Description =
            $"The {Name} is responsible for reporting any breakdowns in the submarine that appear following orders given by their Captain. \n" +
            $"Some failures neutralize the submarine systems, others can cause damage."
            ;
    }

    #endregion

    #region Unity methods

    // Awake is called when an enabled script instance is being loaded
    void Awake()
    {
        SetDescription();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public methods

    public void ToggleUI()
    {

    }

    #endregion
}
