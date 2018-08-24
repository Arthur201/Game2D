﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {



	public static GM instance = null;



	public float yMinLive = -10f;
	public Transform spawnPoint;
	
	public GameObject playerPrefab;

	public float maxTime = 120f; 

	bool timerOn = true;

	float timeLeft;

	public UI ui;

	GameData data = new GameData();

	PlayerCtrl player;

	public float timeToRespawn = 2f;

    void Awake(){
		if(instance == null){
			instance = this;
		}
		
	}



	void Start () {
		if(player == null){
			RespawnPlayer();
		}
		timeLeft = maxTime;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(player == null){
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if(obj != null){
				player = obj.GetComponent<PlayerCtrl>();
			}
		}
		UpdateTimer();
		DisplayHudData();
	}

	void UpdateTimer(){
		if(timerOn){
			timeLeft = timeLeft - Time.deltaTime;
			if(timeLeft <=0f){
				timeLeft = 0;
				ExpirePlayer();
			}
		}
	}

	void DisplayHudData(){
		ui.hud.txtcoinCount.text = "x " + data.coinCount;
		ui.hud.txtTimer.text = "Timer: " + timeLeft.ToString("F1");

	}
	public void IncrementCoinCount() {
		data.coinCount++;
	}

	public void RespawnPlayer(){
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

	}

	public void KillPlayer(){
		if(player != null){
			Destroy(player.gameObject);
			Invoke("RespawnPlayer", timeToRespawn);
		}
	}

		public void ExpirePlayer(){
		if(player != null){
			Destroy(player.gameObject);
			
		}
		 GameOver();
		}

		void GameOver() {
		timerOn = false;
		ui.gameOver.txtcoinCount.text = "Coins: " + data.coinCount;
		ui.gameOver.txtTimer.text = "Timer: " + timeLeft.ToString("F1");
		ui.gameOver.gameOverPanel.SetActive(true);
		}
	}

	









