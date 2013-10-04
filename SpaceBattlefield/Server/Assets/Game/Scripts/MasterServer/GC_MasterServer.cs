using UnityEngine;
using System.Collections;
using uLobby;
using uGameDB;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;



public class GC_MasterServer : uLink.MonoBehaviour {
	
	
	void Awake () {
		
		Application.targetFrameRate = 60; // This is to stop the server over-working with nothing to render	
		Application.runInBackground = true; // We want this masterserver to always run, even if it loses focus
		
		Lobby.AddListener(this);
		
		ServerRegistry.OnServerAdded += OnServerAdded;
		ServerRegistry.OnServerRemoved += OnServerRemoved;
		
		AccountManager.OnAccountRegistered += OnAccountRegistered;
		
		AccountManager.OnAccountLoggedIn += OnAccountLoggedIn;
		AccountManager.OnAccountLoggedOut += OnAccoutLogedOut;
		
		Lobby.InitializeLobby(30,7050,"ec2-54-229-34-163.eu-west-1.compute.amazonaws.com",8087);
			
	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//--------------------------------------------- uLobby events --------------------------------------------------------
	
	private void uLobby_OnLobbyInitialized()
	{
		
	}
	
	private void OnServerAdded(ServerInfo server)
	{
		uLink.BitStream dataCopy = server.data;
		
	}
	
	private void OnServerRemoved(ServerInfo server)
	{
		uLink.BitStream dataCopy = server.data;
		
	}
		
	private void OnAccountLoggedIn(Account _Account)
	{
		
	}
	
	private void OnAccoutLogedOut(Account _Account)
	{
		
	}
	
	private void OnAccountRegistered(Account _Account)
	{
		
	}
	
	
	//-----------------------------------------------------------------------------------------------------------------------
}
