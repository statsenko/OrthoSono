using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// http://stackoverflow.com/questions/3255697/using-c-sharp-reflection-to-call-a-constructor
using System.Reflection;

using System.Data;
using Mono.Data.Sqlite;
using System.IO;


public class DataManager : MonoBehaviour
{	
	protected static GameObject _go;
	protected static DataManager mInstance = null;
	//private string _dbPath = null;

//	private delegate BaseDTO ParsingDelegate(double x, double y);
	private static Dictionary<string, string [] >  _operations;

	// Response identifiers
	public static string GET_SCENE_DTO = "GET_SCENE_DTO";

	public static DataManager Instance 
	{
		get 
		{
			if (Application.isEditor && !Application.isPlaying)
				return null;
			
			if (mInstance == null)
			{
				_go = new GameObject("_DataManager");
				mInstance = _go.AddComponent(typeof(DataManager)) as DataManager;	
				DontDestroyOnLoad(_go);
				/*
					#if UNITY_IPHONE 
						_dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)  + "/leasson.sqlite";
					#else if UNITY_ANDROID
						_dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)  + "/leasson.sqlite";
					#endif
				*/

				_operations = new Dictionary<string, string[] >
				{
					// request name	// DTO name		// SQL response
					{ GET_SCENE_DTO, new string []	{"SceneDTO", 	"SELECT * FROM category" } },
				};
			}
			return mInstance;
		}
	}
	
	void OnDestroy()
	{
		mInstance = null;
		Destroy(_go);
	}

	public static ArrayList ReadDB(string requestIdentifier)
	{
		ArrayList result = new ArrayList();

		string classDTOname = _operations[requestIdentifier][0];
		string sql = _operations[requestIdentifier][1];

		Type type = Type.GetType(classDTOname);
		MethodInfo method = type.GetMethod("Parse");

		// Подключаемся к нашей базе данных
		string connectionString = "URI=file:" + Application.dataPath + "/DB/leasson.sqlite";
		//connectionString = _dbPath;

		using (IDbConnection dbcon = (IDbConnection)new SqliteConnection(connectionString))
		{
			dbcon.Open();

			// Выбираем нужные нам данные
			using (IDbCommand dbcmd = dbcon.CreateCommand())
			{
				dbcmd.CommandText = sql;
				// Выполняем запрос
				using (IDataReader reader = dbcmd.ExecuteReader())
				{
					// Читаем и выводим результат
					while (reader.Read())
					{
						BaseDTO dto = (BaseDTO) Activator.CreateInstance(type) ;
						IDataReader[] theArray = {reader};
						method.Invoke(dto, theArray);
						result.Add(dto);
					}
				}
			}
			// Закрываем соединение
			dbcon.Close();
		}
		return result;
	}








}
