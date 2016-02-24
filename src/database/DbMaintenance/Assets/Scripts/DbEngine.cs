using System;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class DbEngine : IDisposable
{
    private IDbConnection dbconn;

    public DbEngine(string connString)
	{
        dbconn = (IDbConnection)new SqliteConnection(connString);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
    }

    public void insertUserInfo(string username, int win, float accuracy, float timeCloaked, int doubleJump)
    {

        if (win >= 1)
        {
            win = 1;
        }
        else
        {
            win = 0;
        }
        username.Replace('\'', '\0');
        username.Replace('\"', '\0');
        username.Replace(';', '\0');
        string SQL = string.Format("INSERT INTO `tblUsers` (`username`, `win`, `accuracy`, `timeCloaked`, `doubleJumps`) VALUES ('{0}', {1}, {2}, {3}, {4})", username, win, accuracy, timeCloaked, doubleJump);
        Debug.WriteLine(SQL);
        IDbCommand dbCmd = dbconn.CreateCommand();
        dbCmd.CommandText = SQL;

        IDataReader reader = dbCmd.ExecuteReader();
        reader.Dispose();
        reader = null;
        dbCmd.Dispose();
        dbCmd = null;

    }

    public List<DbItem> GetAllUsers()
    {
        string SQL = "SELECT `ID`, `username`, `win`, `accuracy`, `timeCloaked`, `doubleJumps`, `datePlayed` FROM `tblUsers` WHERE 1;";
        List<DbItem> results = new List<DbItem>();
        DbItem item; // = new DbItem();
        int ID = 0;
        string username = "";
        int win = 0;
        float accuracy = 0.0f;
        float timeCloaked = 0.0f;
        int doubleJumps = 0;
        DateTime theTime;
        IDbCommand dbCmd = dbconn.CreateCommand();
        dbCmd.CommandText = SQL;
        IDataReader reader = dbCmd.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                ID = reader.GetInt32(0);
                username = reader.GetString(1);
                win = reader.GetInt32(2);
                accuracy = reader.GetFloat(3);
                timeCloaked = reader.GetFloat(4);
                doubleJumps = reader.GetInt32(5);
                theTime = reader.GetDateTime(6);
            
                item = new DbItem(ID, username, win, accuracy, timeCloaked, doubleJumps, theTime);
                results.Add(item);
            }
        }
        catch (Exception e )
        {
            Debug.WriteLine(e.Message);
        }


        reader.Close();
        reader = null;
        dbCmd.Dispose();
        dbCmd = null;

        return results;
    }

    public void Dispose()
    {
        dbconn.Dispose();
    }
    
}
