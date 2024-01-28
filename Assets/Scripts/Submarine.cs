using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    #region Attributes

    // state of submarine
    private int _health;
    private bool _isSubmerged;
    [SerializeField] private int _maxHealth;
    private int _nbOfTurnsSurfaced;

    // state of the submarine's crew
    private string _name;
    public List<Player> _players;

    // positions of the submarine
    private Captain.Direction _currentCourse;
    private Position _currentPosition;
    private int[,] _trail;


    // testing variables
    private Map _gameMap; // to take from the GameManager or another GameObject

    #endregion

        #region Properties

    /// <summary>
    /// The Direction the Captain has ordered the Submarine to go towards
    /// </summary>
    public Captain.Direction CurrentCourse { get { return _currentCourse; } set { _currentCourse = value; } }

    /// <summary>
    /// Current position of the Submarine on the selected map
    /// </summary>
    public Position CurrentPosition { get => _currentPosition; } // readonly, moving the Submarine should only be executed within the class

    /// <summary>
    /// The amount of health the submarine currently has
    /// </summary>
    public int Health { get { return _health; } set { _health = value; } }

    /// <summary>
    /// Whether the submarine is currently under water or not
    /// </summary>
    public bool IsSubmerged { get { return _isSubmerged; } set { _isSubmerged = value; } }

    /// <summary>
    /// The amount of health the submarine starts the game with
    /// </summary>
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    /// <summary>
    /// The name the players chose to refer to their crew
    /// </summary>
    public string Name { get => _name; set => _name = value; } // readonly, no reason to change the team's name mid-game

    /// <summary>
    /// The players constituting the Submarin's crew
    /// </summary>
    public List<Player> Players { get => _players; } // readonly, there shouldn't be any reason to change the formation of the team mid-game

    /// <summary>
    /// Matrix representing where the submarine has already gone within the game's Map 
    /// </summary>
    public int[,] Trail { get => _trail; }

    /// <summary>
    /// Number of turns during which the submarine has been on the surface
    /// </summary>
    public int TurnsSurfaced { get { return _nbOfTurnsSurfaced; } set { _nbOfTurnsSurfaced = (value >= 0 && value <= 3) ? value: 0; } }

    #endregion

    #region Unity methods

    // Start is called before the first frame update
    void Start()
    {
        //_maxHealth = gameManager.mode == "normal" ? 1 : 4;
        _health             = _maxHealth;
        _isSubmerged        = true;
        _currentCourse      = Captain.Direction.None;

        _players            = new List<Player>(); // take the players chosen from the lobby available right before

        _nbOfTurnsSurfaced  = 0;
        if(_gameMap!=null)
        _trail              = new int[_gameMap.GetMap().GetLength(0), _gameMap.GetMap().GetLength(1)];
    }

    // Update is called once per frame
    void Update()
    {
        if (_nbOfTurnsSurfaced >= 4)
            ToggleSubmersion();

        if (_health <= 0)
        {
            Die();
        }
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Method called when the Captain decides to make their team's submarine surface, as a way to reset the submarine's state (failures, etc.)
    /// </summary>
    public void MakeSurface()
    {
        ToggleSubmersion();
        Debug.Log($"The submarine has made surface at position ({_currentPosition.x}, {_currentPosition.y}).\n" +
            $"The members of {_name} are unable to act during {3 - _nbOfTurnsSurfaced} turns");
    }

    /// <summary>
    /// Updates the position of the submarine of the game's Map according to the Captain's decision
    /// </summary>
    /// <param name="course">The course/direction chosen by the Captain</param>
    public void Move(Captain.Direction course)
    {
        Position newPosition = _currentPosition;

        switch (course)
        {
            case Captain.Direction.North:
                newPosition.x -= 1;
                break;
            case Captain.Direction.South:
                newPosition.x += 1;
                break;
            case Captain.Direction.East:
                newPosition.y += 1;
                break;
            case Captain.Direction.West:
                newPosition.y -= 1;
                break;
        }

        if (IsPathClear(newPosition))
        {
            _trail[_currentPosition.x, _currentPosition.y] = 1; // mark position as past position in history of past positions
            _currentPosition = newPosition; // then update the submarine's position
        }
    }

    /// <summary>
    /// Inflicts damage to the Submarine (1 damage only)
    /// </summary>
    public void TakeDamage()
    {
        if(_health > 0) _health--; // had a parameter but since the game works by inflicting 1 damage only, decided otherwise
        Debug.Log($"The submarine has just taken a damage. It can now only take {_health} more hits");
    }

    #endregion

    #region Private methods

    private void Die()
    {
        Debug.Log("The submarine has been killed");
        // gameManager.IsGameOver = true;
    }
   
    private bool IsPathClear(Position positionToCheck)
    {
        return !((_gameMap.GetMap()[positionToCheck.x, positionToCheck.y] != 0) || (_trail[positionToCheck.x, positionToCheck.y] == 1));
    }

    private void ToggleSubmersion()
    {
        _isSubmerged = !_isSubmerged;
    }

    #endregion
}
