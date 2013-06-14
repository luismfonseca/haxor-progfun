using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using Assets.Scripts.Haxor.Util;

namespace Haxor
{
    [Serializable]
	public class HighScore : IComparable
	{
	    public string PlayerName { get; set; }
	
	    public int Score { get; set; }
		
		public int CompareTo(object obj)
	    {
	        var otherScore = (HighScore) obj;
	        if (Score == otherScore.Score)            
	            return 0;            
	
	        if (Score < otherScore.Score)            
	            return 1;            
	
	        return -1;
	    }
	}
}