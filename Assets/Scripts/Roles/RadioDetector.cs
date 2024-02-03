using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RadioDetector : Role
{

    #region Attributes

    private bool _isSeeThroughOpen;
    private bool _isGridOpen;
    private bool _isDotOpen;
    private GameObject _seeThrough;
    private GameObject _Grid;
    private GameObject _Dot;
    public enum Direction { North, East, South, West, None }
    private Position currentpos = new Position(9,9);

    #endregion

    #region Overridden methods

    /// <summary>
    /// Method called when the turn of the Captain finishes or ends.
    /// </summary>
    protected override void OnActionStatusChanged()
    {
        ToggleUI();
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
        _Grid = GameObject.Find("TileGrid");
        _Dot = GameObject.Find("StartingDot");
        _isSeeThroughOpen = false;
        _isGridOpen = false;
        _isDotOpen = false;
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
        _seeThrough.transform.localScale = _isSeeThroughOpen ? new Vector3((float)1.2,(float)1.7,1) : Vector3.zero;
        _isGridOpen = !_isGridOpen;
        _Grid.transform.localScale = _isGridOpen ? Vector3.one : Vector3.zero;
        _isDotOpen = !_isDotOpen;
        _Dot.transform.localScale = _isDotOpen ? new Vector3(30,30,5) : Vector3.zero;
    }

    #endregion

}
