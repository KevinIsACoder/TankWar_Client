using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class DBAccess 
{
	private SqliteConnection dbConnection;  //负责打开和关闭
	private SqliteCommand sqliteCommand;
	private static Dictionary<string, DBAccess> DbTable = new Dictionary<string, DBAccess>();
	
	public DBAccess(string path)
	{
		OpenDB(path);
	}
	public static DBAccess CreateDB(string path, string type)
	{
		if(!DbTable.ContainsKey(type) || DbTable[type] == null || DbTable[type].dbConnection == null)
		{
			DBAccess dBAccess = new DBAccess(path);
			DbTable[type] = dBAccess;
		}
		return DbTable[type];
	}
    public void OpenDB(string datapath)
	{
		try
		{
			dbConnection = new SqliteConnection(datapath);
			dbConnection.Open();

			dbConnection.DefaultTimeout = 1;
			#if UNITY_IPHONE
			iPhone.SetNoBackupFlag(datapath);
			#endif
		}
		catch(System.Exception ex)
		{
			Debug.LogError("DB Access Error" + ex);
		}
	}
	public void CloseSqlConnection()
	{
		if(sqliteCommand != null)
			sqliteCommand.Dispose();
		if(dbConnection != null)
			dbConnection.Close();
		dbConnection = null;
	}
	public SqliteTransaction BeginTransaction()
	{
		return dbConnection.BeginTransaction();
	}
	public SqliteDataReader ExecuteQuery(string sqlQuery)
	{
		sqliteCommand = new SqliteCommand(dbConnection);
		sqliteCommand.CommandText = sqlQuery;
		return sqliteCommand.ExecuteReader();
	}
	public int ExecuteNonQuery(string sqlQuery, SqliteParameter[] parameters)   //有参数的查询
	{
		int result = 1;
		sqliteCommand = new SqliteCommand(sqlQuery);
		try
		{
			sqliteCommand.CommandText = sqlQuery;
			if(parameters != null)
			{
				sqliteCommand.Parameters.AddRange(parameters);
			}
			result = sqliteCommand.ExecuteNonQuery();
		}
		catch(System.Exception ex)
		{
			result = 0;
			Debug.LogError(ex);
		}
		return result;
	}
	public int ReplaceSpecific(string tableName, string[] cols, string[] values)
	{
		if(cols.Length != values.Length)
			throw new System.Exception("Cols.Length != Values.Length!");
		SqliteParameter[] parameters = new SqliteParameter[cols.Length];
		string[] Values = new string[values.Length];
		for(int i = 0; i < cols.Length; ++i)
		{
			cols[i] = "@" + cols[i];
			parameters[i] = new SqliteParameter(cols[i], values[i]);
		}
		string colsString = string.Join(", ", cols);
		string valuesString = string.Join(", ", Values);
		string query = "REPLEACE INTO" + tableName + "(" + colsString + ") VALUES (" + valuesString + ")";
		return ExecuteNonQuery(query, parameters);
	}

	public int UpdateData(string tableName, string[] cols, string[] values, string condition)
	{
		if(cols.Length != values.Length)
			throw new System.Exception("Cols.length != Values.Length");
		SqliteParameter[] parameter = new SqliteParameter[cols.Length];
		for(int i = 0; i < cols.Length; ++i)
		{
			cols[i] = "@" + cols[i];
			parameter[i] = new SqliteParameter(cols[i], values[i]);
		}
		string query = "UPDATE" + tableName + " SET " + string.Join(", ", cols);
		if(!string.IsNullOrEmpty(condition))
		   query = query + "WHERE" + condition;
		return ExecuteNonQuery(query, parameter);
	}
    public int InsertData(string tableName, string[] cols, string[] values)
	{
        if(cols.Length != values.Length)
			throw  new System.Exception("Cols.Length != Values.Length");
		SqliteParameter[] parameters = new SqliteParameter[cols.Length];
		string ColString = string.Join(", ", cols);
		string[] ValueSympols = new string[values.Length];
		for(int i = 0; i < values.Length; ++i)
		{
			ValueSympols[i] = "@" + cols[i];
			parameters[i] = new SqliteParameter(cols[i], values[i]);
		}
		string query = "INSERT INTO" + tableName + "(" + ColString + ") Values " + ValueSympols;
		return ExecuteNonQuery(query, parameters);
	}
	public void CreateTable(string tableName, string[] col, string[] coltype)  //创建表
	{
        if(col.Length != coltype.Length)
			throw new System.Exception("Create Table: Cols.Length != ColType.Length");
		string CommandText = "CREATE TABLE" + tableName;
		for(int i = 0; i < col.Length; ++i)
		{
			CommandText += "(" + col[i] + " " + coltype[i] + ")";
		}
		ExecuteQuery(CommandText);
	}
}
