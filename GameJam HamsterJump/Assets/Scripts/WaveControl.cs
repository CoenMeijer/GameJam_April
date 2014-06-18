using UnityEngine;
using System.Collections;

public class WaveControl : MonoBehaviour {

    
	// -- Properties -- //
	public bool deadtime = false;
	private float deadtimer = 4;
	// 
	// Bullet
	public GameObject bulletSpawn;

	// Timers
	public float startTimer;

    // Next Wave
    public bool startNextWave = false;

	// -- Vars -- //

    // Bullet Spawners
    private BulletControl[] TopSpawners;
    private BulletControl[] BottomSpawners;
    private BulletControl[] RightSpawners;
    private BulletControl[] LeftSpawners;

	// Wave Started
	private bool _started = false;

    // Current wave
    private int _cWave = 1;

	// Timers
	private float _startTimerLeft;

	public void Start()
    {
        // Load all bullet spawners
        TopSpawners     = GameObject.Find("BulletSpawners/Top").GetComponentsInChildren<BulletControl>();
        BottomSpawners  = GameObject.Find("BulletSpawners/Bottom").GetComponentsInChildren<BulletControl>();
        RightSpawners   = GameObject.Find("BulletSpawners/Right").GetComponentsInChildren<BulletControl>();
        LeftSpawners    = GameObject.Find("BulletSpawners/Left").GetComponentsInChildren<BulletControl>();

		_startTimerLeft = startTimer;
	}

	public void Update () 
	{
		if(deadtime){
			deadtimer -= Time.deltaTime;
			if(deadtimer <= 0){
				Application.LoadLevel(0);
			}
		}

        if (startNextWave)
        {
            NextWave(3);
			startNextWave = false;
        }

		if(!_started)
		{
			_startTimerLeft -= Time.deltaTime;
			if(_startTimerLeft <= 0)
			{
                CreateWave(_cWave);
                _started = true;
			}
		}
	}

	private void CreateWave(int waveNumber)
	{
        if(waveNumber == 1)
        {
            starSpawners(RightSpawners, 4, -3, 2, (i) => { return i * 0.1f; });
            starSpawners(LeftSpawners, 4, -3, 2, (i) => { return i * 0.1f; });
        }
        else if (waveNumber == 2)
        {
            starSpawners(TopSpawners, 0, -1, 1, (i) => { return i * 0.1f; });
            starSpawners(BottomSpawners, 0, -1, 1, (i) => { return i * 0.1f; });
        }
        else if (waveNumber == 3)
        {
            starSpawners(TopSpawners, 0, -1, 3, (i) => { return i * 0.1f; });
            starSpawners(LeftSpawners, 0, -1, 3, (i) => { return i * 0.1f; });
        }
	}

    // The blueprint of the startSpawners func-param
    private delegate float TimeCalcFunc(int index);

    private void starSpawners(BulletControl[] side, int start, int amount, int skip, TimeCalcFunc calc)
    {
        // Validate
        if (start < 0) start = 0;
        if (amount < 0) amount = side.Length + amount;
        if (amount > side.Length) amount = side.Length;
        if (skip < 0) skip = 0;

        // Skip amount index
        int cSkipInd = -1;

        // Loop
        for (int i = start; i < amount; i++)
        {
            cSkipInd++;
            if (cSkipInd == skip)
            {
                cSkipInd = -1;
                side[i].delayLeft = calc(i);
                side[i].enabled = true;
            }
        }
    }

    private void stopSpawners(BulletControl[][] sides)
    {
        foreach (BulletControl[] cSide in sides)
        {
            foreach (BulletControl cItem in cSide)
            {
                cItem.enabled = false;
            }
        }
    }

    public void NextWave(int waitTime)
    {
        _cWave++;
        stopSpawners(new BulletControl[][] { TopSpawners, BottomSpawners, RightSpawners, LeftSpawners });
        _startTimerLeft = waitTime;
        _started = false;
    }
}
