using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioDetector : Role
{

    #region Attributes

    private bool _isSeeThroughOpen;
    private GameObject _seeThrough;

    #endregion

    #region Overridden methods

    /// <summary>
    /// Method called when the turn of the Captain finishes or ends.
    /// </summary>
    protected override void OnActionStatusChanged()
    {
        ToggleUI();
    }

    public override void PerformRoleAction()
    {
        // setting the turn as not done when it just began
        ToggleTurn();
        Debug.Log("Captain role started\n" + Description);
    }

    protected override void SetDescription()
    {
        Name = "Radio Detector";
        Description =
            "The captain is the central element of the entire crew.\n" +
            "In addition to being responsible for the trajectory taken by the submarine, they must be the link between all other posts."
            ;
    }

    /// <summary>
    /// Method enabling/disabling the UI elements used to perform role actions according to whether the turn is done or not.
    /// </summary>
    protected override void ToggleUI()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.localScale = !IsTurnOver ? Vector3.one : Vector3.zero;
    }


    #endregion

    #region Unity methods

    private void Awake()
    {
        SetDescription();
    }

    // Start is called before the first frame update
    void Start()
    {
        _seeThrough = GameObject.Find("See through");
        _isSeeThroughOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSeeThroughOpen)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleSeeThrough();
            }
        }
    }

    #endregion

    #region Methods

    public void ToggleSeeThrough()
    {
        _isSeeThroughOpen = !_isSeeThroughOpen;
        _seeThrough.transform.localScale = _isSeeThroughOpen ? Vector3.one : Vector3.zero;
    }

    #endregion
}
