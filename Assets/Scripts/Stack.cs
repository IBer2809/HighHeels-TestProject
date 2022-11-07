using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    
    [SerializeField] private GameManager _gameManager;


    [SerializeField] private Transform _leftParent;
    [SerializeField] private Transform _rightParent;
    [SerializeField] private Transform _leftPrevious;
    [SerializeField] private Transform _rightPrevious;

    [SerializeField] private int _stackOffset;

    public List<Transform> LeftHeelsOnPlayer;
    public List<Transform> RightHeelsOnPlayer;

    public int lastActiveIndexForLeftStack;
    public int lastActiveIndexForRightStack;

    [SerializeField] private List<GameObject> _leftHeelPool;
    [SerializeField] private List<GameObject> _rightHeelPool;
    [SerializeField] private GameObject _leftHeelPrefab;
    [SerializeField] private GameObject _rightHeelPrefab;
    [SerializeField] private int _shoeCount;

    [SerializeField] private GameObject _instantiateLeftHeel;
    [SerializeField] private GameObject _instantiateRightHeel;






    void Start()
    {

        lastActiveIndexForLeftStack = -1;
        lastActiveIndexForRightStack = -1;

        _leftHeelPool = new List<GameObject>();
        _rightHeelPool = new List<GameObject>();

        RightHeelsOnPlayer = new List<Transform>();
        LeftHeelsOnPlayer = new List<Transform>();


        GameObject leftShoe;
        GameObject rightShoe;

        for (int i = 0; i < _shoeCount; i++)
        {
            leftShoe = Instantiate(_leftHeelPrefab);
            rightShoe = Instantiate(_rightHeelPrefab);

            leftShoe.transform.parent = _leftPrevious.transform;
            leftShoe.transform.localScale = new Vector3(1, 1, 1);
            leftShoe.transform.rotation = _leftPrevious.rotation;
            leftShoe.transform.localPosition = new Vector3(_leftPrevious.position.x, _leftPrevious.position.y, _leftPrevious.position.z + _stackOffset);
            leftShoe.SetActive(false);

            LeftHeelsOnPlayer.Add(leftShoe.transform);

            rightShoe.transform.parent = _rightPrevious.transform;
            rightShoe.transform.localScale = new Vector3(1, 1, 1);
            rightShoe.transform.rotation = _rightPrevious.rotation;
            rightShoe.transform.localPosition = new Vector3(_rightPrevious.position.x, _rightPrevious.position.y, _rightPrevious.position.z + _stackOffset);
            rightShoe.SetActive(false);
            
            RightHeelsOnPlayer.Add(rightShoe.transform);

            _leftPrevious = leftShoe.transform;
            _rightPrevious = rightShoe.transform;

            _leftHeelPool.Add(leftShoe);
            _rightHeelPool.Add(rightShoe);
        }



    }

    public void RemoveHeels(int count)
    {
        List<Transform> activeRightHeels = new List<Transform>();
        List<Transform> activeLeftHeels = new List<Transform>();

        for (int i = 0; i < LeftHeelsOnPlayer.Count; i++)
        {
            if (LeftHeelsOnPlayer[i].gameObject.activeInHierarchy)
                activeLeftHeels.Add(LeftHeelsOnPlayer[i]);
        }
        for (int i = 0; i < RightHeelsOnPlayer.Count; i++)
        {
            if (RightHeelsOnPlayer[i].gameObject.activeInHierarchy)
                activeRightHeels.Add(RightHeelsOnPlayer[i]);
        }

        if(_gameManager.CurrentState == GameManager.GameState.Play)
        {
            if (activeLeftHeels.Count >= count && activeRightHeels.Count >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    LeftHeelsOnPlayer[lastActiveIndexForLeftStack].gameObject.SetActive(false);
                    Instantiate(_instantiateLeftHeel, LeftHeelsOnPlayer[lastActiveIndexForLeftStack].position, Quaternion.identity);
                    RightHeelsOnPlayer[lastActiveIndexForRightStack].gameObject.SetActive(false);
                    Instantiate(_instantiateRightHeel, RightHeelsOnPlayer[lastActiveIndexForRightStack].position, Quaternion.identity);
                    lastActiveIndexForLeftStack--;
                    lastActiveIndexForRightStack--;
                }
            }
            else
            {
                _gameManager.CurrentState = GameManager.GameState.Loose;
                for (int i = 0; i < RightHeelsOnPlayer.Count; i++)
                {
                    if (RightHeelsOnPlayer[i].gameObject.activeInHierarchy)
                    {
                        RightHeelsOnPlayer[i].GetComponent<BoxCollider>().enabled = false;
                    }
                }
                for (int i = 0; i < LeftHeelsOnPlayer.Count; i++)
                {
                    if (LeftHeelsOnPlayer[i].gameObject.activeInHierarchy)
                    {
                        LeftHeelsOnPlayer[i].GetComponent<BoxCollider>().enabled = false;
                    }
                }
            }
        }

        else if(_gameManager.CurrentState == GameManager.GameState.PreWin)
        {
            if (activeLeftHeels.Count >= count && activeRightHeels.Count >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    LeftHeelsOnPlayer[lastActiveIndexForLeftStack].gameObject.SetActive(false);
                    RightHeelsOnPlayer[lastActiveIndexForRightStack].gameObject.SetActive(false);
                    lastActiveIndexForLeftStack--;
                    lastActiveIndexForRightStack--;
                }
            }
            else
            {
                _gameManager.CurrentState = GameManager.GameState.WinOnStairs;
            }
        }

        
        




    }
}
