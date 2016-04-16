using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;



/// <summary>
/// This is the model for URDF interactions which contains actions to interact with the DB
/// </summary>
public class UrdfDb
{
    private string CONN_STRING = "URI=file:../../../db/neptune.db";
    private DbEngine _engine;

    /// <summary>
    /// Constructor for the urdf model using the default connection string
    /// </summary>
    /// <param name="connString">string representation to connect to the sqlite db</param>
    public UrdfDb()
    {
        _engine = new DbEngine(CONN_STRING);
    }

    /// <summary>
    /// Constructor for the urdf model by providing a connection string
    /// </summary>
    /// <param name="connString">string representation to connect to the sqlite db</param>
    public UrdfDb(string connString)
    {
        _engine = new DbEngine(connString);
    }

    /// <summary>
    /// Constructor for the urdf model by providing an existing DbEngine to midigate the number of connections
    /// to the database.
    /// </summary>
    /// <param name="engine">DbEngine with an esitablished connection to a sqlite database</param>
    public UrdfDb(DbEngine engine)
    {
        if (engine.HasConnection())
        {
            _engine = engine;
        }
        else
        {
            throw new Exception("No connection has been established yet.");
        }
    }
    /// <summary>
    /// This will query for all urdfs in the database. 
    /// </summary>
    /// <returns>A list of all urdfs in the database.</returns>
    public List<UrdfItemModel> GetUrdfs()
    {
        List<UrdfItemModel> list = new List<UrdfItemModel>();

        string sql = @"SELECT 
                        `uid`, 
                        `name`, 
                        `modelNumber`, 
                        `internalCost`, 
                        `externalCost`, 
                        `weight`, 
                        `powerUsage`, 
                        `notes`, 
                        `visibility`,
                        `fk_type_id`, 
                        `fk_category_id`, 
                        `usable`, 
                        `urdfFilename`, 
                        `prefabFilename`, 
                        `time` 
                    FROM `tblUrdfs`;";

        SqliteCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;
        SqliteDataReader reader = cmd.ExecuteReader();
        UrdfItemModel item;
        try
        {
            while (reader.Read())
            {
                item = new UrdfItemModel();
                item.uid = reader.GetInt32(0);
                item.name = (!reader.IsDBNull(1) ? reader.GetString(1) : String.Empty);
                item.modelNumber = (!reader.IsDBNull(2) ? reader.GetString(2) : String.Empty);
                item.internalCost = (!reader.IsDBNull(3) ? reader.GetFloat(3) : 0.0f);
                item.externalCost = (!reader.IsDBNull(4) ? reader.GetFloat(4) : 0.0f);
                item.weight = (!reader.IsDBNull(5) ? reader.GetFloat(5) : 0.0f);
                item.powerUsage = (!reader.IsDBNull(6) ? reader.GetFloat(6) : 0.0f);
                item.notes = (!reader.IsDBNull(7) ? reader.GetString(7) : String.Empty);
                item.visibility = (!reader.IsDBNull(8) ? reader.GetInt32(8) : 0);
                item.fk_type_id = (!reader.IsDBNull(9) ? reader.GetInt32(9) : 0);
                item.fk_category_id = (!reader.IsDBNull(10) ? reader.GetInt32(10) : 0);
                item.usable = (!reader.IsDBNull(11) ? reader.GetInt32(11) : 0);
                item.urdfFilename = (!reader.IsDBNull(12) ? reader.GetString(12) : String.Empty);
                item.prefabFilename = (!reader.IsDBNull(13) ? reader.GetString(13) : String.Empty);
                item.time = (!reader.IsDBNull(14) ? reader.GetFloat(14) : 0.0f);

                list.Add(item);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            reader.Close();
            reader = null;
            cmd.Dispose();
            cmd = null;
        }

        return list;
    }


    /// <summary>
    /// This will query for all usable sensors in the database by default unless otherwise specified. 
    /// A sensor is usable if it has an appropriate mesh and data associated to it. 
    /// </summary>
    /// <param name="all">Inclusion of sensors that are not usable</param>
    /// <returns>A list of all sensors/urdfs in the database.</returns>
    public List<UrdfItemModel> GetSensors(bool all = false)
    {
        List<UrdfItemModel> list = new List<UrdfItemModel>();

        string sql = @"SELECT 
                        `uid`, 
                        `name`, 
                        `modelNumber`, 
                        `internalCost`, 
                        `externalCost`, 
                        `weight`, 
                        `powerUsage`, 
                        `notes`, 
                        `visibility`,
                        `fk_type_id`, 
                        `fk_category_id`, 
                        `usable`, 
                        `urdfFilename`, 
                        `prefabFilename`, 
                        `time` 
                    FROM `tblUrdfs`
                    WHERE `fk_category_id` = 1 ";
        
        if(!all)
        {
            sql += "AND `usable` = 1";
        }

        SqliteCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;
        SqliteDataReader reader = cmd.ExecuteReader();
        UrdfItemModel item;
        try
        {
            while(reader.Read())
            {
                item = new UrdfItemModel();
                item.uid = reader.GetInt32(0);
                item.name = (!reader.IsDBNull(1) ? reader.GetString(1) : String.Empty);
                item.modelNumber = (!reader.IsDBNull(2) ?  reader.GetString(2) : String.Empty);
                item.internalCost = (!reader.IsDBNull(3) ?  reader.GetFloat(3) : 0.0f);
                item.externalCost = (!reader.IsDBNull(4) ?  reader.GetFloat(4) : 0.0f);
                item.weight = (!reader.IsDBNull(5) ?  reader.GetFloat(5) : 0.0f);
                item.powerUsage = (!reader.IsDBNull(6) ?  reader.GetFloat(6) : 0.0f);
                item.notes = (!reader.IsDBNull(7) ? reader.GetString(7) : String.Empty);
                item.visibility = (!reader.IsDBNull(8) ? reader.GetInt32(8) : 0);
                item.fk_type_id = (!reader.IsDBNull(9) ? reader.GetInt32(9) : 0);
                item.fk_category_id = (!reader.IsDBNull(10) ?  reader.GetInt32(10) : 0);
                item.usable = (!reader.IsDBNull(11) ?  reader.GetInt32(11) : 0);
                item.urdfFilename = (!reader.IsDBNull(12) ?  reader.GetString(12) : String.Empty);
                item.prefabFilename = (!reader.IsDBNull(13) ?  reader.GetString(13) : String.Empty);
                item.time = (!reader.IsDBNull(14) ? reader.GetFloat(14) : 0.0f);

                list.Add(item);
            }
        } 
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            reader.Close();
            reader = null;
            cmd.Dispose();
            cmd = null;
        }

        return list;
    }

    /// <summary>
    /// This will grab all the urdf types that is known in the database
    /// </summary>
    /// <returns>A list of type of urdf in the database.</returns>
    public List<UrdfTypeModel> GetUrdfTypes()
    {
        List<UrdfTypeModel> list = new List<UrdfTypeModel>();

        string sql = "SELECT `uid`, `name` FROM `tblUrdfType`;";
        SqliteCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;
        SqliteDataReader reader = cmd.ExecuteReader();
        UrdfTypeModel item;
        try
        {
            while (reader.Read())
            {
                item = new UrdfTypeModel();
                item.uid = reader.GetInt32(0);
                item.name = reader.GetString(1);

                list.Add(item);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            reader.Close();
            reader = null;
            cmd.Dispose();
            cmd = null;
        }

        return list;
    }

    /// <summary>
    /// Retrieves the categories the list of categories within the database.
    /// </summary>
    /// <returns>A List of categories found. List may be empty.</returns>
    public List<SensorCategoriesModel> GetSensorCategories()
    {
        List<SensorCategoriesModel> list = new List<SensorCategoriesModel>();

        string sql = "SELECT `uid`, `name` FROM `tblSensorCategories`;";
        IDbCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;
        IDataReader reader = cmd.ExecuteReader();
        SensorCategoriesModel item;
        try
        {
            while (reader.Read())
            {
                item = new SensorCategoriesModel();
                item.uid = reader.GetInt32(0);
                item.name = reader.GetString(1);

                list.Add(item);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            reader.Close();
            reader = null;
            cmd.Dispose();
            cmd = null;
        }

        return list;
    }

    /// <summary>
    /// This will add a given UrdfItemModel into the database with provided details.
    /// </summary>
    /// <param name="item">UrdfItemModel to be added into the database</param>
    /// <returns>The insert id of the newely added id. 0 if it was not added successfully.</returns>
    public int AddSensor(UrdfItemModel item)
    {
        long lastId = 0;
        string sql = @"INSERT INTO `tblUrdfs` 
                        (`name`, `modelNumber`, `internalCost`, `externalCost`, `weight`, `powerUsage`, `fk_type_id`, `fk_category_id`, `usable`, `urdfFilename`, `prefabFilename`, `notes`, `visibility`) 
                    VALUES (@name, @modelNumber, @internalCost, @externalCost, @weight, @powerUsage, @fk_type_id, @fk_category_id, @usable, @urdfFilename, @prefabFilename, @notes, @visibility);";
        SqliteCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;
        
        cmd.Parameters.AddWithValue("@name", item.name);
        cmd.Parameters.AddWithValue("@modelNumber", item.modelNumber);
        cmd.Parameters.AddWithValue("@internalCost", item.internalCost);
        cmd.Parameters.AddWithValue("@externalCost", item.externalCost);
        cmd.Parameters.AddWithValue("@weight", item.weight);
        cmd.Parameters.AddWithValue("@powerUsage", item.powerUsage);
        cmd.Parameters.AddWithValue("@fk_type_id", item.fk_type_id);
        cmd.Parameters.AddWithValue("@fk_category_id", item.fk_category_id);
        cmd.Parameters.AddWithValue("@usable", item.usable);
        cmd.Parameters.AddWithValue("@urdfFilename", item.urdfFilename);
        cmd.Parameters.AddWithValue("@prefabFilename", item.prefabFilename);
        cmd.Parameters.AddWithValue("@notes", item.notes);
        cmd.Parameters.AddWithValue("@visibility", item.visibility);

        try
        {
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT last_insert_rowid();";
            lastId = (long)cmd.ExecuteScalar();
        }
        catch (SqliteException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
        }

        return (int)lastId;
    }

    /// <summary>
    /// Update the given UrdfItemModel in the database with the given values.
    /// </summary>
    /// <param name="item">The UrdfItemModel that is to be updated in the database.</param>
    /// <returns>The number of rows affected by the update. 0 if the id was not found.</returns>
    public int UpdateSensor(UrdfItemModel item)
    {
        string sql = @"UPDATE `tblUrdfs` SET 
                        `name` = @name, 
                        `modelNumber` = @modelNumber, 
                        `internalCost` = @internalCost, 
                        `externalCost` = @externalCost, 
                        `weight` = @weight, 
                        `powerUsage` = @powerUsage, 
                        `fk_type_id` = @fk_type_id, 
                        `fk_category_id` = @fk_category_id, 
                        `time` = @time,
                        `notes` = @notes,
                        `usable` = @usable, 
                        `urdfFilename` = @urdfFilename, 
                        `prefabFilename` = @prefabFilename 
                    WHERE `uid` = @uid;";
        SqliteCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;

        cmd.Parameters.AddWithValue("@uid", item.uid);
        cmd.Parameters.AddWithValue("@name", item.name);
        cmd.Parameters.AddWithValue("@modelNumber", item.modelNumber);
        cmd.Parameters.AddWithValue("@internalCost", item.internalCost);
        cmd.Parameters.AddWithValue("@externalCost", item.externalCost);
        cmd.Parameters.AddWithValue("@weight", item.weight);
        cmd.Parameters.AddWithValue("@time", item.time);
        cmd.Parameters.AddWithValue("@powerUsage", item.powerUsage);
        cmd.Parameters.AddWithValue("@fk_type_id", item.fk_type_id);
        cmd.Parameters.AddWithValue("@fk_category_id", item.fk_category_id);
        cmd.Parameters.AddWithValue("@notes", item.notes);
        cmd.Parameters.AddWithValue("@usable", item.usable);
        cmd.Parameters.AddWithValue("@urdfFilename", item.urdfFilename);
        cmd.Parameters.AddWithValue("@prefabFilename", item.prefabFilename);

        int affectedRows = 0;
        try
        {
            affectedRows = cmd.ExecuteNonQuery();
        }
        catch (SqliteException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
        }

        return affectedRows;
    }

    /// <summary>
    /// This allows for flexibilty of deleting a UrdfItemModel with the object
    /// </summary>
    /// <param name="item">UrdfItemModel that is to be removed in the database</param>
    /// <returns>The number of rows affected. 0 if the id was not found</returns>
    public int DeleteSensor(UrdfItemModel item)
    {
        return DeleteSensor(item.uid);
    }

    /// <summary>
    /// Give the unique id of the sensor this method will remove it from the DB if it exists.
    /// </summary>
    /// <param name="UrdfItemModelId">int representation of the sensor id in the database</param>
    /// <returns>The number of rows there were affected by the delete statement, 0 if the id was not found</returns>
    public int DeleteSensor(int UrdfItemModelId)
    {
        int affectedRows = 0;
        string sql = "DELETE FROM `tblUrdfs` WHERE `uid` = @uid";
        SqliteCommand cmd = _engine.conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@uid", UrdfItemModelId);

        try
        {
            affectedRows = cmd.ExecuteNonQuery();
        }
        catch (SqliteException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
        }
        
        return affectedRows;
    }

}
