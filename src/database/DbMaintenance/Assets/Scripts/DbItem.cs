using System;

public class DbItem
{
    private int ID;
    public string username;
    public int win;
    public float accuracy;
    public float timeCloaked;
    public int doubleJumps;
    public DateTime datePlayed;

	public DbItem(int _id, string _username, int _win, float _accuracy, float _timeCloaked, int _doubleJumps, DateTime dateTime)
	{
        this.ID = _id;
        this.username = _username;
        this.win = _win;
        this.accuracy = _accuracy;
        this.timeCloaked = _timeCloaked;
        this.doubleJumps = _doubleJumps;
        this.datePlayed = dateTime;
    }
}
