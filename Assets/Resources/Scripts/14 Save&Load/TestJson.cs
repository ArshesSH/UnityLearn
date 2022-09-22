using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using LitJson;

public class PlayerInfo
{
    public PlayerInfo( int id, string name, double gold )
    {
        ID = id;
        Name = name;
        Gold = gold;
    }

    public int ID;
    public string Name;
    public double Gold;

}

public class TestJson : MonoBehaviour
{
    #region Public Fields

    public List<PlayerInfo> playerInfoList = new List<PlayerInfo>();


    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        //SavePlayerInfo();   
        LoadPlayerInfo();
    }
    private void Update()
    {
	    
    }
    #endregion


    #region Public Methods

    public void SavePlayerInfo()
    {
        Debug.Log( "SavePlayerInfo() Called" );
        playerInfoList.Add( new PlayerInfo( 1, "이름1", 10001 ) );
        playerInfoList.Add( new PlayerInfo( 2, "이름2", 102001 ) );
        playerInfoList.Add( new PlayerInfo( 3, "이름3", 1301 ) );
        playerInfoList.Add( new PlayerInfo( 4, "이름4", 14001 ) );

        JsonData infoJson = JsonMapper.ToJson( playerInfoList );
        // 덮어쓰기
        File.WriteAllText( Application.dataPath + "/Resources/JsonDatas/PlayerInfoData.json", infoJson.ToString() );

    }

    public void LoadPlayerInfo()
    {
        Debug.Log( "LoadPlayerInfo() Called" );

        if(File.Exists(Application.dataPath + "/Resources/JsonDatas/PlayerInfoData.json" ) )
        {
            string jsonString = File.ReadAllText( Application.dataPath + "/Resources/JsonDatas/PlayerInfoData.json" );

            Debug.Log( jsonString );

            JsonData infoJson = JsonMapper.ToObject( jsonString );

            ParsingJsonPlayerInfo( infoJson );


        }
    }


    #endregion


    #region Private Methods

    void ParsingJsonPlayerInfo( JsonData data )
    {
        Debug.Log( "ParsingJsonPalyerInfo() called" );

        for(int i = 0; i < data.Count; ++i )
        {
            Debug.Log( data[i]["ID"].ToString() );
            Debug.Log( data[i]["Name"] );
            Debug.Log( data[i]["Gold"].ToString() );


            int id = (int)data[i]["ID"];
            Debug.Log( id );
        }

    }

    #endregion
}
