using UnityEngine;
using System.Collections;

using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SceneDTO : BaseDTO 
{
	public int sceneID = -1;
	public string sceneName = null;
	public string sceneTitle = null;
	public string description = null;
	public string previousSceneName = null; 

	public override void Parse(IDataReader reader)
	{
		this.sceneID = reader.GetInt32(0);
		this.sceneName = reader.GetString(1);
		/*
		this.sceneTitle = reader.GetString(2);
		this.description = reader.GetString(3);
		this.previousSceneName = reader.GetString(4);
		*/
		const string frmt = "ID: {0}; Title: {1}";
		Debug.Log(string.Format(frmt, this.sceneID, this.sceneName));
	}
}
