using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;

public class DbEngine
{
    public SqliteConnection conn = null;
    private string errorMessage;

    /// <summary>
    /// Constructor for a given connection string provided.
    /// </summary>
    /// <param name="connString">The string to connection to the database file</param>
    public DbEngine(string connString)
    {
        if (!init(connString))
        {
            // TODO: Handle invalid init
        }
    }

    /// <summary>
    /// Always call this function before using to ensure that we're connected to the DB.
    /// </summary>
    /// <param name="connString">The string to connection to the database file</param>
    /// <returns>true if it has a success connection, false if there isn't</returns>
    protected bool init(string connString)
    {
        bool result = true;

        try
        {
            if (conn == null)
            {
                conn = new SqliteConnection(connString);
            }
            else
            {
                conn.Close();
                conn.ConnectionString = connString;
            }

            conn.Open();
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            result = false;
        }
        return result;
    }

    /// <summary>
    /// This will tell us if we ran init already and the connection was successfulliy opened
    /// </summary>
    /// <returns>true if init has been called before and connection state is open, false if it hasn't been initialized</returns>
    public bool HasConnection()
    {
        // check if the dbconn object has been initiated
        // if it has been initiated then is check the connection state 
        return ((conn != null) && (conn.State == ConnectionState.Open)); 
    }

    /// <summary>
    /// If there's an error that occurs we can get the error message this way.
    /// </summary>
    /// <returns>return the error message if any</returns>
    public string GetError()
    {
        return errorMessage;
    }
    
    public void insert(string table, Dictionary<string, string> valueList)
    {
        string keyValues = "";
        string values = "";

        string SQL = string.Format("INSERT INTO `{0}` ({1}) VALUES ({3})", table, keyValues, values);
        Debug.WriteLine(SQL);
        SqliteCommand dbCmd = conn.CreateCommand();
        
        dbCmd.CommandText = SQL;
        dbCmd.Prepare();
        dbCmd.ExecuteNonQuery();

        dbCmd.Dispose();
        dbCmd = null;
    }
}
