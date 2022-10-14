using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class SaveInformation
{
    public string name;
    public float posX;
    public float posY;
    public float posZ;
    
}

public class Save_Load : MonoBehaviour
{
    #region Public Fields
    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        CSV_Load();
    }
    private void Update()
    {
        // Load
        if ( Input.GetKeyDown( KeyCode.A ) )
        {
            if ( PlayerPrefs.HasKey( "ID" ) )
            {
                string getID = PlayerPrefs.GetString( "ID" );
                Debug.Log( string.Format( "ID:{0}", getID ) );
            }
            else
            {
                Debug.Log( "저장된 ID 없음" );
            }
        }

        if ( Input.GetKeyDown( KeyCode.B ) )
        {
            string setID = "PlayerID";
            PlayerPrefs.SetString( "ID", setID );
            Debug.Log( "Saved" );
        }

        if ( Input.GetKeyDown( KeyCode.C ) )
        {
            PlayerPrefs.SetInt( "Score", 33 );
            PlayerPrefs.SetFloat( "Exp", 44.4f );

            int getScore = 0;
            if ( PlayerPrefs.HasKey( "Score" ) )
            {
                getScore = PlayerPrefs.GetInt( "Score" );
            }


            float getExp = 0;
            if ( PlayerPrefs.HasKey( "Exp" ) )
            {
                getExp = PlayerPrefs.GetFloat( "Exp" );
            }

            Debug.Log( getScore.ToString() );
            Debug.Log( getExp.ToString() );
        }

        if ( Input.GetKeyDown( KeyCode.D ) )
        {

            int getScore = PlayerPrefs.GetInt( "Score", 100 );
            float getExp = PlayerPrefs.GetFloat( "Exp", 100.0f );
            string getName = PlayerPrefs.GetString( "Name", "NONE" );

            Debug.Log( getScore.ToString() );
            Debug.Log( getExp.ToString() );
            Debug.Log( getName );
        }

        if ( Input.GetKeyDown( KeyCode.F ) )
        {
            PlayerPrefs.DeleteKey( "ID" );
            PlayerPrefs.DeleteKey( "Score" );
            PlayerPrefs.DeleteKey( "Exp" );

            PlayerPrefs.DeleteAll();
        }


        if ( Input.GetKeyDown( KeyCode.S ) )
        {
            SaveAsBinary();
        }
        if ( Input.GetKeyDown( KeyCode.T ) )
        {
            LoadBinary();
        }
    }
    #endregion


    #region Public Methods
    #endregion


    #region Private Methods

    void SaveAsBinary()
    {
        SaveInformation info = new SaveInformation();
        info.name = "inha";
        info.posX = 0.0f;
        info.posY = 2.0f;
        info.posZ = 4.3f;

        Debug.Log( info );

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream();
        formatter.Serialize( memStream, info );
        byte[] bytes = memStream.GetBuffer();
        String memStr = Convert.ToBase64String( bytes );

        Debug.Log( memStr );

        PlayerPrefs.SetString( "SaveInformation", memStr );
    }

    void LoadBinary()
    {
        string getInfos = PlayerPrefs.GetString( "SaveInformation", "None" );
        Debug.Log( "Load: " + getInfos );
        byte[] bytes = Convert.FromBase64String( getInfos );
        MemoryStream memStream = new MemoryStream( bytes );
        BinaryFormatter formatter = new BinaryFormatter(  );
        SaveInformation informs = formatter.Deserialize( memStream ) as SaveInformation;


        Debug.Log( informs.name );
        Debug.Log( informs.posX );
        Debug.Log( informs.posY );
        Debug.Log( informs.posZ );
    }

    void CSV_Load()
    {
        List<Dictionary<string, object>> data = CSVReader.Read( "DataSheets/ItemSheet" );
        for(var i = 0; i < data.Count; ++i )
        {
            print( $"ID: {data[i]["ID"]}" );
            print( $"Name: {data[i]["Name"]}" );
            print( $"Power: {data[i]["Power"]}" );
            print( $"Description: {data[i]["Description"]}" );
        }
    }

    #endregion
}
