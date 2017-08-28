using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

abstract public class DB{

    private MySqlConnection conn;

    private MySqlConnection SetConn()
    {
        if(conn == null)
        {
            try
            {
                conn = new MySqlConnection("server=127.0.0.1;uid=admin;pwd=admin;database=hospitalwatts;");
            }
            catch (MySqlException e)
            {
                Debug.Log(e);
            }
        }

        return this.conn;
    }

    public MySqlConnection GetConn()
    {
        return this.SetConn();
    }


}
