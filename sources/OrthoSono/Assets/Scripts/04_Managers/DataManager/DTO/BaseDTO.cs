using UnityEngine;
using System.Collections;

using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public abstract class BaseDTO  
{
	public abstract void Parse(IDataReader reader); 
}
