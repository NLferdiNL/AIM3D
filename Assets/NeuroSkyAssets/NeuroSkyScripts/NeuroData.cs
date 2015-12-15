using UnityEngine;
using System.Collections;

public class NeuroData : MonoBehaviour
{
	public Texture2D[] signalIcons;
	
	private int _indexSignalIcons = 1;
	
    TGCConnectionController _controller;

    private int _poorSignal1;
    private int _attention1;
    private int _meditation1;

    [SerializeField]
    private bool _debugMode = false;

    [SerializeField]
    [Range(0, 100)]
    private int _debugPoorSignal;

    [SerializeField]
    [Range(0,100)]
    private int _debugAttention;

    [SerializeField]
    [Range(0, 100)]
    private int _debugMeditation;

    public int attention {
        get { return _attention1; }
    }
    public int meditation{
        get { return _meditation1; }
    }
	
	private float delta;

    void Start()
    {
		
		_controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
		
		_controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
		_controller.UpdateAttentionEvent += OnUpdateAttention;
		_controller.UpdateMeditationEvent += OnUpdateMeditation;
		
		_controller.UpdateDeltaEvent += OnUpdateDelta;
		
    }
	
	void OnUpdatePoorSignal(int value){
        _poorSignal1 = value;
		if(value < 25){
      		_indexSignalIcons = 0;
		}else if(value >= 25 && value < 51){
      		_indexSignalIcons = 4;
		}else if(value >= 51 && value < 78){
      		_indexSignalIcons = 3;
		}else if(value >= 78 && value < 107){
      		_indexSignalIcons = 2;
		}else if(value >= 107){
      		_indexSignalIcons = 1;
		}
	}
	void OnUpdateAttention(int value){
		_attention1 = value;
	}
	void OnUpdateMeditation(int value){
		_meditation1 = value;
	}
	void OnUpdateDelta(float value){
		delta = value;
	}


    void OnGUI()
    {
		GUILayout.BeginHorizontal();
		
		
        if (GUILayout.Button("Connect"))
        {
            _controller.Connect();
        }
        if (GUILayout.Button("DisConnect"))
        {
            _controller.Disconnect();
			_indexSignalIcons = 1;
        }

        if (_debugMode) { 
            _attention1 = _debugAttention;
            _meditation1 = _debugMeditation;
            _poorSignal1 = _debugPoorSignal; 
        }

		GUILayout.Space(Screen.width-250);
		GUILayout.Label(signalIcons[_indexSignalIcons]);
		
		GUILayout.EndHorizontal();

		
        GUILayout.Label("PoorSignal1:" + _poorSignal1);
        GUILayout.Label("Attention1:" + _attention1);
        GUILayout.Label("Meditation1:" + _meditation1);
		GUILayout.Label("Delta:" + delta);

    }
}
