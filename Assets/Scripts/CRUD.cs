using UnityEngine;
using System;
using MySql.Data.MySqlClient;

public class CRUD : DB {

    private MySqlCommand query;

    public MySqlCommand insert(int pontuacao)
    {
        try
        {
            this.query = new MySqlCommand("INSERT into record (nome,pontuacao) VALUES ('a', '" +pontuacao+"')", this.GetConn());

            this.GetConn().Open();
            this.query.ExecuteNonQuery();
            this.GetConn().Close();

        }
        catch(MySqlException e)
        {
            Debug.Log(e);
        }

        return this.query;
    }

    public int[] Select()
    {
        int[] data = new int[5]; int i = 0;
        try
        {
           
            this.query = new MySqlCommand("SELECT pontuacao FROM record order by pontuacao ASC LIMIT 4 ", this.GetConn());
            this.GetConn().Open();
            MySqlDataReader myReader = this.query.ExecuteReader();
            while (myReader.Read())
            {
                data[i] = myReader.GetInt32(0); i++;
            }
            myReader.Close();
            this.GetConn().Close();

        }
        catch (MySqlException e)
        {
            Debug.Log(e);
        }

        return data;
    }

}
