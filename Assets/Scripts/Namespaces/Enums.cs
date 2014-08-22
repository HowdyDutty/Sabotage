
namespace Sabotage
{
	namespace Enums
	{
		/////////////////// Gameboard Manager ////////////////////////////  
		enum rotation : int
		{
			RIGHT 		= 0,
			LEFT  		= 180,
			LOWER_LEFT  = 240,
			LOWER_RIGHT = 300,
			UPPER_LEFT  = 120,
			UPPER_RIGHT = 60
		};

		/////////////////// Pick Up Items ////////////////////////////
		public enum PickUpType : int
		{
			GAMEBOARD_ITEM  = 0,
			GOD_ACTION		= 1	
		};

		/////////////////// Player Manager ////////////////////////////
		enum Player : int 
		{ 
			ONE = 0, 
			TWO = 1
		};
	
		public enum PlayerType : int
		{
			GAMEBOARD = 0,
			GOD 	  = 1
		};
	
	}
}
